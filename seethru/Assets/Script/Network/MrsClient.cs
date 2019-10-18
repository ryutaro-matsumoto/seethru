using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using DataStructures;

using UnityEngine.SceneManagement;

using MrsServer = System.IntPtr;
using MrsConnection = System.IntPtr;
using MrsCipher = System.IntPtr;
public class MrsClient : Mrs {
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
    
    protected static bool        g_IsKeyExchange;
    protected static bool        g_IsEncryptRecords;
    protected static UInt16      g_RecordOptions;
    protected static UInt32      g_WriteDataLen;
    protected static UInt32      g_WriteCount;
    protected static UInt32      g_Connections;
    protected static UInt32      g_ReadCount;
    protected static String      g_Header;
    protected static mrs.Connect g_Connect;
    protected static bool        g_IsValidRecord;

    protected static byte[] sampledata;
    protected static MrsConnection g_nowconnect;
    protected static ushort g_paytype = 0x01;
    protected static Single movepos = -10.52f;
    protected static Single angle = 0;
    protected static Int16 ammos = 5;

    protected static NetworkSettingData netsettings;
    protected static DataStructures.S_DataPlayer myData;
    protected static DataStructures.S_DataPlayer myNewData;
    protected static GameObject g_Object;
    protected static GameObject g_EnemyObject;
    private static bool connected;
    private static bool g_gameon;
    private static bool createMrs;

    protected static string g_playerName;

    private static MrsClient myClient;

    
    static MrsClient()
    {
#if UNITY_WEBGL
        g_ArgConnectionType   = "3";
        g_ArgIsKeyExchange    = "0";
#else
        g_ArgConnectionType = "2";
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
    
    protected bool m_IsRunning;
    
    void Awake(){
        gameObject.AddComponent< mrs.ScreenLogger >();
        gameObject.AddComponent<GameManager>();
        gameObject.AddComponent<NetworkSettingData>();

        myClient = this;

        m_IsRunning = false;
        g_ReadCount = 0;
        g_Connect = new mrs.Connect();
        netsettings = gameObject.GetComponent<NetworkSettingData>();
        InitMyData();

        g_gameon = false;
        if (!createMrs)
        {
            DontDestroyOnLoad(this.gameObject);
            createMrs = true;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

#if false
    void OnGUI()
    {
        if (!m_IsRunning)
        {
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
                m_IsRunning = true;
                StartEchoClient();
                //mrs.Utility.LoadScene("SampleScene");
            }
        }
        else
        {
            if (GUILayout.Button("Back", GUILayout.Width(300), GUILayout.Height(50)))
            {
                m_IsRunning = false;
                mrs_close(g_nowconnect);
                mrs.Utility.LoadScene("NetworkSetting");
            }

            if (GUILayout.Button("Send Player Data", GUILayout.Width(300), GUILayout.Height(50)))
            {
                g_Object = GameObject.Find("MainPlayer");
                S_DataPlayer player = new S_DataPlayer();
                player.x = g_Object.transform.position.x;
                player.y = g_Object.transform.position.y;
                player.angle = g_Object.transform.localEulerAngles.z;
                player.bullets = g_Object.GetComponent<Player>().bullet;
                player.died = false;
                IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(player));
                Marshal.StructureToPtr(player, p_data, false);
                if (g_nowconnect != null)
                {
                    g_paytype = 0x02;
                    mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(player));
                }
                Marshal.FreeHGlobal(p_data);
            }

            if (GUILayout.Button("Send Shot Data", GUILayout.Width(300), GUILayout.Height(50)))
            {

                S_DataShots shot = new S_DataShots();
                shot.x = movepos;
                shot.y = 1.0f;
                shot.angle = angle;
                IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(shot));
                Marshal.StructureToPtr(shot, p_data, false);
                if (g_nowconnect != null)
                {
                    g_paytype = 0x03;
                    mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(shot));
                }
                Marshal.FreeHGlobal(p_data);
            }
        }
    }
#endif

    /// <summary>
    /// ゲームが始まった時用のイニシャライズ
    /// </summary>
    public static void InitMrsforGame(S_StartingData _startdata)
    {
        GameManager.ProtoStart(_startdata.spawnid,_startdata.sumplayer);

        g_gameon = true;
    }

    private void InitMyData()
    {
        myData.x = 0;
        myData.y = 0;
        myData.angle = 0;
        myData.bullets = 0;
        myData.died = false;
    }

    private static String connection_type_to_string( MrsConnection connection ){
        MrsConnectionType type = mrs_connection_get_type( connection );
        switch ( type ){
        case MrsConnectionType.NONE:{ return "NONE"; }
        case MrsConnectionType.TCP:{ return "TCP"; }
        case MrsConnectionType.UDP:{ return "UDP"; }
        case MrsConnectionType.WS:{ return "WS"; }
        case MrsConnectionType.WSS:{ return "WSS"; }
        case MrsConnectionType.TCP_SSL:{ return "TCP_SSL"; }
        case MrsConnectionType.MRU:{ return "MRU"; }
        }
        return "INVALID";
    }
    
    private static void read_echo( MrsConnection connection, byte[] payload, UInt32 payload_len ){
        mrs.Time read_time = new mrs.Time();
        read_time.Set();
        mrs.Buffer buffer = new mrs.Buffer();
        buffer.Write( payload, payload_len );
        while ( 0 < buffer.GetDataLen() ){
            mrs.Time write_time = buffer.ReadTime();
            MRS_LOG_DEBUG( "read_echo data={0} data_len={1} diff_time={2}({3} - {4})",
                ToString( buffer.GetData() ), g_WriteDataLen,
                ( read_time - write_time ).ToString(), read_time.ToString(), write_time.ToString() );
            if ( ! buffer.Read( null, g_WriteDataLen ) ){
                MRS_LOG_ERR( "Lost data. len={0} {1}", buffer.GetDataLen(), mrs.Utility.ToHex( buffer.GetData(), buffer.GetDataLen() ) );
                break;
            }
            
            ++g_ReadCount;
            if ( g_WriteCount * g_Connections <= g_ReadCount ){
                MRS_LOG_DEBUG( "Since all records have been received, it is finished." );
            }
        }
    }
    
    private static void parse_record( MrsConnection connection, IntPtr connection_data, UInt32 seqnum, UInt16 options, UInt16 payload_type, IntPtr payload, UInt32 payload_len ){
        //MRS_LOG_DEBUG( "parse_record seqnum={0} options=0x{1:X2} payload=0x{2:X2}/{3}", seqnum, options, payload_type, payload_len );
        // MRS_PAYLOAD_TYPE_BEGIN - MRS_PAYLOAD_TYPE_ENDの範囲内で任意のIDを定義し、対応するアプリケーションコードを記述する
        switch ( payload_type ){
            case 0x01:
                {
                    S_DataProfile data = (S_DataProfile)Marshal.PtrToStructure(payload, typeof(S_DataProfile));

                    GameManager.ConnectionServer((uint)data.player_id, myClient);
                    netsettings.SetProfile(data.player_id, "a", data.spawn_id);
                }break;
            case 0x02:
                {
                    S_DataProfile data = (S_DataProfile)Marshal.PtrToStructure(payload, typeof(S_DataProfile));
                    
                    //netsettings.SetProfile(data.player_id, "a", data.spawn_id);
                }
                break;
            case 0x03:
                {
                    S_StartingData starting = (S_StartingData)Marshal.PtrToStructure(payload, typeof(S_StartingData));
                    MRS_LOG_DEBUG("Starting... Table:{0} countPlayers:{1}", String.Join(", ",starting.spawnid), starting.sumplayer);
                    MRS_LOG_DEBUG("My player number is : {0} !", GameManager.playID);
                    InitMrsforGame(starting);
                }break;
            case 0x12:
                {
                    //MRS_LOG_DEBUG("RECEIVED DATA:{0}", payload);
                    S_DataPlayer data = (S_DataPlayer)Marshal.PtrToStructure(payload, typeof(S_DataPlayer));
                    if (GameManager.players[data.id] != null)
                    {
                        GameManager.players[data.id].transform.position = new Vector3(data.x, data.y, 0);
                        GameManager.players[data.id].transform.eulerAngles = new Vector3(0.0f, 0.0f, data.angle);
                    }
                    //MRS_LOG_DEBUG("RECEIVED DATA  pos_x:{0} pos_y:{1} pos_z:{2} look:{3} move:{4} ammos:{5}"
                    //    data.x, data.y, data.z, data.angle, data.move_a, data.ammos);
                }break;
            case 0x13:
                {
                    //MRS_LOG_DEBUG("RECEIVED DATA:{0}", payload);
                    S_DataShots data = (S_DataShots)Marshal.PtrToStructure(payload, typeof(S_DataShots));
                    GameObject bulletPool = GameObject.Find("BulletPool");
                    Pool script = bulletPool.GetComponent<Pool>();

                    script.Place(new Vector2(data.x, data.y), Quaternion.AngleAxis(data.angle, Vector3.forward));
                    //MRS_LOG_DEBUG("RECEIVED DATA  pos_x:{0} pos_y:{1} pos_z:{2} angle:{3}",
                    //    data.pos_x, data.pos_y, data.pos_z, data.angle);
                }
                break;

            // 毎フレームデータ受信
            case 0x11:
                {
                    MRS_LOG_DEBUG("RECEIVED WRAP:{0}", payload);
                    //S_WrapData s_Wrap = (S_WrapData)Marshal.PtrToStructure(payload, typeof(S_WrapData));
                    //for (int i = 0; i < s_Wrap.num; i++)
                    //{

                    //    MRS_LOG_DEBUG("RECEIVED DATA  pos_x:{0} pos_y:{1} pos_z:{2} look:{3} move:{4} ammos:{5}",
                    //        s_Wrap.s_Data[i].pos_x, s_Wrap.s_Data[i].pos_y, s_Wrap.s_Data[i].pos_z, s_Wrap.s_Data[i].look_a, s_Wrap.s_Data[i].move_a, s_Wrap.s_Data[i].ammos);
                    //}
                }
                break;

            default: {}break;
        }
    }
    
    private static void write_echo( MrsConnection connection, byte[] data, UInt32 data_len ){
        mrs.Time write_time = new mrs.Time();
        write_time.Set();
        mrs.Buffer buffer = new mrs.Buffer();
        buffer.WriteTime( write_time );
        buffer.Write( data, data_len );
        
        if ( g_IsValidRecord ){
            mrs_write_record( connection, g_RecordOptions, g_paytype, buffer.GetData(), buffer.GetDataLen() );
            MRS_LOG_DEBUG("WROTE RECORD type:0x{0:X}", g_paytype);
        }else{
            mrs_write( connection, buffer.GetData(), buffer.GetDataLen() );
        }
    }
    
    private static void write_echo_all( MrsConnection connection ){
        g_Header = connection_type_to_string( connection ) +" ";
        g_Header += ( g_IsKeyExchange && g_IsEncryptRecords ) ? "  CRYPT" : "NOCRYPT";
        
        byte[] data = new byte[ g_WriteDataLen ];
        for ( UInt32 i = 0; i < g_WriteCount; ++i ){
            if ( 0 < g_WriteDataLen ){
                byte[] string_bytes = ToBytes( String.Format( "{0}: {1}", g_Header, i + 1 ) );
                int string_len = string_bytes.Length;
                if ( (int)g_WriteDataLen <= string_bytes.Length ){
                    string_len = (int)g_WriteDataLen;
                }
                Buffer.BlockCopy( string_bytes, 0, data, 0, string_len );
            }
            write_echo( connection, data, g_WriteDataLen );
        }
    }
    
    // 鍵交換した時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsKeyExchangeCallback))]
    private static void on_key_exchange( MrsConnection connection, IntPtr connection_data ){
        MRS_LOG_DEBUG( "on_key_exchange" );
    //    mrs.Utility.LoadScene("SampleScene");
        //write_echo_all( connection );
    }
    
    // ソケット接続時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsConnectCallback))]
    private static void on_connect( MrsConnection connection, IntPtr connection_data ){
        MRS_LOG_DEBUG( "on_connect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version( MRS_VERSION_KEY ), mrs_connection_get_remote_version( connection, MRS_VERSION_KEY ) );
        g_paytype = 0x01;
        connected = true;

        if ( g_IsKeyExchange ){
            mrs_set_cipher( connection, mrs_cipher_create( MrsCipherType.ECDH ) );
            mrs_key_exchange( connection, on_key_exchange );
        }else{
            //write_echo_all( connection );
        }

        byte[] p_data = ToBytes(g_playerName);
        if (g_nowconnect != null)
        {
            mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)p_data.Length);
        }

        mrs.Utility.LoadScene("MatchRoom");
    }
    
    // ソケット切断時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsDisconnectCallback))]
    private static void on_disconnect( MrsConnection connection, IntPtr connection_data ){
        MRS_LOG_DEBUG( "on_disconnect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version( MRS_VERSION_KEY ), mrs_connection_get_remote_version( connection, MRS_VERSION_KEY ) );
        connected = false;
        GameManager.ReceiveID(9);
    }
    
    // ソケットにエラーが発生した時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsErrorCallback))]
    private static void on_error( MrsConnection connection, IntPtr connection_data, MrsConnectionError status ){
        switch ( status ){
        case MrsConnectionError.CONNECT_ERROR:
        case MrsConnectionError.CONNECT_TIMEOUT:{
            MrsConnection client = g_Connect.FallbackConnect( connection );
            if ( MrsConnection.Zero != client ) return;
        }break;
        
        default: break;
        }
        
        MRS_LOG_ERR( "on_error local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X} status={2}",
            mrs_get_version( MRS_VERSION_KEY ), mrs_connection_get_remote_version( connection, MRS_VERSION_KEY ), ToString( mrs_get_connection_error_string( status ) ) );
    }
    
    // レコード受信時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsReadRecordCallback))]
    private static void on_read_record( MrsConnection connection, IntPtr connection_data, UInt32 seqnum, UInt16 options, UInt16 payload_type, IntPtr _payload, UInt32 payload_len ){
        parse_record( connection, connection_data, seqnum, options, payload_type, _payload, payload_len );
    }
    
    // バイナリデータ受信時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsReadCallback))]
    private static void on_read( MrsConnection connection, IntPtr connection_data, IntPtr _data, UInt32 data_len ){
        read_echo( connection, ToBytes( _data, data_len ), data_len );
    }
    
    // フォールバック接続時に呼ばれる
    private static void on_fallback_connect( MrsConnection connection, mrs.Connect.Request request ){
        MRS_LOG_DEBUG( "on_fallback_connect connection_type="+ request.ConnectionType +" addr="+ request.Addr +" port="+ request.Port +" timeout_msec="+ request.TimeoutMsec );
        mrs_set_connect_callback( connection, on_connect );
        mrs_set_disconnect_callback( connection, on_disconnect );
        mrs_set_error_callback( connection, on_error );
        
        if ( g_IsValidRecord ){
            mrs_set_read_record_callback( connection, on_read_record );
        }else{
            mrs_set_read_callback( connection, on_read );
        }
        mrs_connection_set_path( connection, g_ArgConnectionPath );
    }
    
    public void StartEchoClient(){
        MRS_LOG_DEBUG( "connection_type={0} is_key_exchange={1} is_encrypt_records={2} write_data_len={3} write_count={4} connections={5} server_addr={6} server_port={7} timeout_msec={8} is_valid_record={9} connection_path={10}",
            g_ArgConnectionType, g_ArgIsKeyExchange, g_ArgIsEncryptRecords, g_ArgWriteDataLen, g_ArgWriteCount, g_ArgConnections,
            g_ArgServerAddr, g_ArgServerPort, g_ArgTimeoutMsec, g_ArgIsValidRecord, g_ArgConnectionPath );

        g_nowconnect = new MrsConnection();

        g_IsKeyExchange = ( 0 != ToUInt32( g_ArgIsKeyExchange ) );
        g_IsEncryptRecords = ( 0 != ToUInt32( g_ArgIsEncryptRecords ) );
        g_RecordOptions = g_IsEncryptRecords ? (UInt16)MrsRecordOption.ON_CRYPT : (UInt16)MrsRecordOption.NONE;
//        g_RecordOptions |= (UInt16)MrsRecordOption.UDP_UNRELIABLE;
//        g_RecordOptions |= (UInt16)MrsRecordOption.UDP_UNSEQUENCED;
        g_WriteDataLen = ToUInt32( g_ArgWriteDataLen );
        g_WriteCount = ToUInt32( g_ArgWriteCount );
        g_Connections = ToUInt32( g_ArgConnections );
        g_IsValidRecord = ( 0 != ToUInt32( g_ArgIsValidRecord ) );
        
        mrs.Connect.Request connect_request = new mrs.Connect.Request();
        connect_request.ConnectionType = MrsConnectionType.NONE;
        connect_request.Addr           = g_ArgServerAddr;
        connect_request.Port           = ToUInt16( g_ArgServerPort );
        connect_request.TimeoutMsec    = ToUInt32( g_ArgTimeoutMsec );
        Int32 connection_type = ToInt32( g_ArgConnectionType );
        switch ( connection_type ){
        case (Int32)MrsConnectionType.TCP:
        case (Int32)MrsConnectionType.UDP:
        case (Int32)MrsConnectionType.WS:
        case (Int32)MrsConnectionType.WSS:
        case (Int32)MrsConnectionType.TCP_SSL:
        case (Int32)MrsConnectionType.MRU:{
            connect_request.ConnectionType = (MrsConnectionType)connection_type;
            g_Connect.AddRequest( connect_request );
        }break;
        
        default:{
#if UNITY_WEBGL
            connect_request.ConnectionType = MrsConnectionType.WSS;
            g_Connect.AddRequest( connect_request );
            connect_request.ConnectionType = MrsConnectionType.WS;
            connect_request.Port -= 1;
            g_Connect.AddRequest( connect_request );
#else
            connect_request.ConnectionType = MrsConnectionType.TCP;
            g_Connect.AddRequest( connect_request );
            connect_request.ConnectionType = MrsConnectionType.WSS;
            connect_request.Port += 2;
            g_Connect.AddRequest( connect_request );
            connect_request.ConnectionType = MrsConnectionType.WS;
            connect_request.Port -= 1;
            g_Connect.AddRequest( connect_request );
#endif
        }break;
        }
        g_Connect.SetFallbackConnectCallback( on_fallback_connect );

        netsettings.addr = connect_request.Addr;
        netsettings.port = connect_request.Port;

        mrs_initialize();
        do{
            for ( UInt32 i = 0; i < g_Connections; ++i ){
                MrsConnection client = g_Connect.FallbackConnect();
                g_nowconnect = client;
                netsettings.connection = client;
                if ( MrsConnection.Zero == client ){
                    MRS_LOG_ERR( "mrs_connect[{0}]: {1}", i, ToString( mrs_get_error_string( mrs_get_last_error() ) ) );
                    break;
                }
            }
        }while ( false );
    }
    
    void Update(){
        mrs_update();
        if (g_gameon)
        {
            CompareMyData();
        }
        if (connected && !g_gameon) {
            //g_gameon = true;
            //mrs.Utility.LoadScene("ProtoScene");
        }
    }
    
    void End(){
        mrs_finalize();
    }
    
    void OnDestroy(){
        End();
    }
    
    void OnApplicationPause( bool pause ){
        if ( pause ) mrs_update_keep_alive();
    }


    void CompareMyData()
    {
        if (!GameManager.players[GameManager.playID].GetComponent<Player>().isDead)
        {
            myNewData.id = GameManager.playID;
            myNewData.x = GameManager.players[GameManager.playID].transform.position.x;
            myNewData.y = GameManager.players[GameManager.playID].transform.position.y;
            myNewData.angle = GameManager.players[GameManager.playID].transform.localEulerAngles.z;
            myNewData.bullets = 1;
            myNewData.died = false;

            IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(myNewData));
            Marshal.StructureToPtr(myNewData, p_data, false);
            if (g_nowconnect != null)
            {
                g_paytype = 0x12;
                mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(myNewData));
                MRS_LOG_DEBUG("SEND MY DATA");
            }
            Marshal.FreeHGlobal(p_data);
            myData = myNewData;

            PlayerInput inputScript = GameManager.players[GameManager.playID].GetComponent<PlayerInput>();

        }
    }

    /// <summary>
    /// 弾情報の送信（発射時に呼び出してくれればOK）
    /// </summary>
    /// <param name="_x">座標Ｘ</param>
    /// <param name="_y">座標Ｙ</param>
    /// <param name="_angle">発射角度</param>
    public void SendShootData(float _x, float _y, float _angle)
    {
        S_DataShots shot;
        shot.x = _x;
        shot.y = _y;
        shot.angle = _angle;
        shot.died = false;

        IntPtr p_shotdata = Marshal.AllocHGlobal(Marshal.SizeOf(shot));
        Marshal.StructureToPtr(shot, p_shotdata, false);
        if (g_nowconnect != null)
        {
            g_paytype = 0x13;
            mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_shotdata, (uint)Marshal.SizeOf(shot));
        }
        Marshal.FreeHGlobal(p_shotdata);
    }

    public void SendRoomReady()
    {
        g_paytype = 0x03;
        int ready = 1;
        IntPtr blank = Marshal.AllocHGlobal(Marshal.SizeOf(myNewData));
        Marshal.StructureToPtr(myNewData, blank, false);
        
        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, blank, sizeof(int));

        Marshal.FreeHGlobal(blank);
    }


    /// <summary>
    /// 接続に必要な情報を外部からセット
    /// </summary>
    /// <param name="_ip">IPアドレス</param>
    public void SetSettings(string _ip, string _name)
    {
        g_playerName = _name;
        g_ArgServerAddr = _ip;
        netsettings.SetProfile(-1, _name, -1);
    }
}