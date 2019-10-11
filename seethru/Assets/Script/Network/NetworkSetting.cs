using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MrsServer = System.IntPtr;
using MrsConnection = System.IntPtr;
using MrsCipher = System.IntPtr;

public class NetworkSetting : Mrs
{
    protected static String g_ArgConnectionType;
    protected static String g_ArgIsKeyExchange;
    protected static String g_ArgIsEncryptRecords;
    protected static String g_ArgWriteDataLen;
    protected static String g_ArgWriteCount;
    protected static String g_ArgConnections;
    protected static String g_ArgServerAddr;
    protected static String g_ArgServerPort;
    protected static String g_ArgTimeoutMsec;
    protected static String g_ArgIsValidRecord;
    protected static String g_ArgConnectionPath;

    protected static bool g_IsKeyExchange;
    protected static bool g_IsEncryptRecords;
    protected static UInt16 g_RecordOptions;
    protected static UInt32 g_WriteDataLen;
    protected static UInt32 g_Connections;
    protected static UInt32 g_ReadCount;
    protected static String g_Header;
    protected static mrs.Connect g_Connect;
    protected static bool g_IsValidRecord;

    protected static byte[] sampledata;
    protected static MrsConnection g_nowconnect;

    NetworkSettingData netsettings;

    bool try2connect = false;
    int trytime;

    static NetworkSetting()
    {
#if UNITY_WEBGL
        g_ArgConnectionType   = "3";
        g_ArgIsKeyExchange    = "0";
#else
        g_ArgConnectionType = "1";
        g_ArgIsKeyExchange = "1";
#endif
        g_ArgIsEncryptRecords = "1";
        g_ArgWriteDataLen = "1024";
        g_ArgWriteCount = "10";
        g_ArgConnections = "1";
        g_ArgServerAddr = "127.0.0.1";
#if UNITY_WEBGL
        g_ArgServerPort       = "22223";
#else
        g_ArgServerPort = "22222";
#endif
        g_ArgTimeoutMsec = "500";
        g_ArgIsValidRecord = "1";
        g_ArgConnectionPath = "/";
    }


    void Awake()
    {
        gameObject.AddComponent<mrs.ScreenLogger>();
        
        g_Connect = new mrs.Connect();
    }

    private void OnGUI()
    {
        if (!try2connect)
        {
            if (netsettings != null) { netsettings = null; } 
            GUILayout.Space(30);

            GUILayout.BeginHorizontal();
            GUILayout.Label("ServerAddr:");
            g_ArgServerAddr = GUILayout.TextField(g_ArgServerAddr);
            GUILayout.Space(30);
            GUILayout.Label("ServerPort:");
            g_ArgServerPort = GUILayout.TextField(g_ArgServerPort);
            GUILayout.Space(30);
            GUILayout.Label("TimeoutMsec:");
            g_ArgTimeoutMsec = GUILayout.TextField(g_ArgTimeoutMsec);
            GUILayout.EndHorizontal();
            GUILayout.Space(60);

            if (GUILayout.Button("Start Game", GUILayout.Width(300), GUILayout.Height(80)))
            {
                MrsClient client = gameObject.GetComponent<MrsClient>();
                netsettings = new NetworkSettingData();
                try2connect = true;
                trytime = Int32.Parse(g_ArgTimeoutMsec)+500;
                client.StartEchoClient();
                //mrs.Utility.LoadScene("SampleScene");
            }
        }
        else
        {
            GUILayout.TextArea("Connecting ...");
        }
    }

    void End()
    {
        mrs_finalize();
    }

    void OnDestroy()
    {
        End();
    }

    // Update is called once per frame
    void Update()
    {
        if (try2connect)
        {
            if (trytime > 0) { trytime--; }
            else { try2connect = false; }
        }
        mrs_update();
    }


    // 鍵交換した時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsKeyExchangeCallback))]
    private static void on_key_exchange(MrsConnection connection, IntPtr connection_data)
    {
        MRS_LOG_DEBUG("on_key_exchange");
        
    }

    // ソケット接続時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsConnectCallback))]
    private static void on_connect(MrsConnection connection, IntPtr connection_data)
    {
        MRS_LOG_DEBUG("on_connect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version(MRS_VERSION_KEY), mrs_connection_get_remote_version(connection, MRS_VERSION_KEY));
        //g_paytype = 0x01;
        if (g_IsKeyExchange)
        {
            mrs_set_cipher(connection, mrs_cipher_create(MrsCipherType.ECDH));
            mrs_key_exchange(connection, on_key_exchange);
        }
        else
        {
            //write_echo_all( connection );
        }
    }

    // フォールバック接続時に呼ばれる
    private static void on_fallback_connect(MrsConnection connection, mrs.Connect.Request request)
    {
        MRS_LOG_DEBUG("on_fallback_connect connection_type=" + request.ConnectionType + " addr=" + request.Addr + " port=" + request.Port + " timeout_msec=" + request.TimeoutMsec);
        mrs_set_connect_callback(connection, on_connect);
        mrs_set_disconnect_callback(connection, on_disconnect);
        mrs_set_error_callback(connection, on_error);

        mrs_set_read_record_callback(connection, on_read_record);
        mrs_connection_set_path(connection, g_ArgConnectionPath);
    }

    // ソケット切断時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsDisconnectCallback))]
    private static void on_disconnect(MrsConnection connection, IntPtr connection_data)
    {
        MRS_LOG_DEBUG("on_disconnect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version(MRS_VERSION_KEY), mrs_connection_get_remote_version(connection, MRS_VERSION_KEY));
    }

    // ソケットにエラーが発生した時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsErrorCallback))]
    private static void on_error(MrsConnection connection, IntPtr connection_data, MrsConnectionError status)
    {
        switch (status)
        {
            case MrsConnectionError.CONNECT_ERROR:
            case MrsConnectionError.CONNECT_TIMEOUT:
                {
                    MrsConnection client = g_Connect.FallbackConnect(connection);
                    if (MrsConnection.Zero != client) return;
                }
                break;

            default: break;
        }

        MRS_LOG_ERR("on_error local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X} status={2}",
            mrs_get_version(MRS_VERSION_KEY), mrs_connection_get_remote_version(connection, MRS_VERSION_KEY), ToString(mrs_get_connection_error_string(status)));
    }
    
    // レコードのパース
    private static void ParseRecord(MrsConnection connection, IntPtr connection_data, UInt32 seqnum, UInt16 options, UInt16 payload_type, IntPtr _payload, UInt32 payload_len)
    {
        Mrs.MRS_LOG_DEBUG("ParseRecord seqnum=0x{0} options=0x{1:X2} payload={2:X}/{3}", seqnum, options, payload_type, payload_len);
        // MRS_PAYLOAD_TYPE_BEGIN - MRS_PAYLOAD_TYPE_ENDの範囲内で任意のIDを定義し、対応するアプリケーションコードを記述する
        switch (payload_type)
        {
            case 0x01:
                break;
            default:
                break;
        }
    }

    // レコード受信時に呼ばれる
    private static void on_read_record(MrsConnection connection, IntPtr connection_data, UInt32 seqnum, UInt16 options, UInt16 payload_type, IntPtr _payload, UInt32 payload_len)
    {
        ParseRecord(connection, connection_data, seqnum, options, payload_type, _payload, payload_len);
    }



    void StartToConnect()
    {
        mrs.Connect.Request connect_request = new mrs.Connect.Request();
        connect_request.ConnectionType = MrsConnectionType.TCP;
        connect_request.Addr = g_ArgServerAddr;
        connect_request.Port = ToUInt16(g_ArgServerPort);
        connect_request.TimeoutMsec = ToUInt32(g_ArgTimeoutMsec);
        g_Connect.AddRequest(connect_request);

        g_Connect.SetFallbackConnectCallback(on_fallback_connect);
        mrs_initialize();
        do
        {
            for (UInt32 i = 0; i < g_Connections; ++i)
            {
                MrsConnection client = g_Connect.FallbackConnect();
                g_nowconnect = client;
                if (MrsConnection.Zero == client)
                {
                    MRS_LOG_ERR("mrs_connect[{0}]: {1}", i, ToString(mrs_get_error_string(mrs_get_last_error())));
                    break;
                }
            }
        } while (false);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause) mrs_update_keep_alive();
    }
}