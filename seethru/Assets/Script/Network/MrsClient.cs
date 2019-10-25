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
    private static uint g_playercount = 0;


    private static MrsClient myClient;
    private static RoomManager g_roomManager;

    private bool updatefix = false;

    private static System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
    
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
        GameManager.ipAddress = "127.0.0.1";
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

		if (!createMrs) {
			DontDestroyOnLoad(this.gameObject);
			createMrs = true;
		}
		else {
			Destroy(this.gameObject);
			return;
		}

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
        GameManager.GameStart((uint)_startdata.stageid,_startdata.spawnid,_startdata.sumplayer);

    }

    static private void InitMyData()
    {
        myNewData.x = 0;
        myNewData.y = 0;
        myNewData.angle = 0;
        myNewData.dead = false;
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
        MRS_LOG_DEBUG( "parse_record seqnum={0} options=0x{1:X2} payload=0x{2:X2}/{3}", seqnum, options, payload_type, payload_len );
        // MRS_PAYLOAD_TYPE_BEGIN - MRS_PAYLOAD_TYPE_ENDの範囲内で任意のIDを定義し、対応するアプリケーションコードを記述する
        switch (payload_type)
        {

            // 0x01 : 自分のプロファイルが返送されてきた
            case 0x01:
                {
                    S_DataProfile data = (S_DataProfile)Marshal.PtrToStructure(payload, typeof(S_DataProfile));

                    GameManager.ConnectionServer((uint)data.player_id, myClient);
                    netsettings.SetProfile(data.player_id, GameManager.playerName);
                    GameManager.UpdateProfileList(data.player_id, GameManager.playerName);
					FadeManeger.Fadeout("MatchRoom");
                }
                break;

            // 0x02 : 誰かのプロファイルが送られてきた
            case 0x02:
                {
                    S_DataProfile data = (S_DataProfile)Marshal.PtrToStructure(payload, typeof(S_DataProfile));
					GameManager.UpdateProfileList(data.player_id, data.name);
                }
                break;

            // 0x03 : ゲームスタートの合図が送られてきた
            case 0x03:
                {
                    S_StartingData starting = (S_StartingData)Marshal.PtrToStructure(payload, typeof(S_StartingData));
                    MRS_LOG_DEBUG("Starting... Table:{0} countPlayers:{1}", String.Join(", ", starting.spawnid), starting.sumplayer);

                    InitMrsforGame(starting);
                    g_playercount = starting.sumplayer;
                }
                break;

            // 0x04 : サーバーサイドの全員の初期化同期完了
            case 0x04:
                {
                    g_gameon = true;
                }
                break;

                // 0x05 : カウントダウン開始タイミング受け取り
            case 0x05:
                {
                    GameManager.CountStart();
                }
                break;

                // 0x06 : 試合開始のカウントダウン時間受け取り
            case 0x06:
                unsafe{
                    Int32 countDown = *(Int32*) payload;
                    GameManager.CountDown(countDown);
                }
                break;

                // 0x07 : 選択中のステージ番号
            case 0x07:
                unsafe{
                    //byte[] data = new byte[payload_len];
                    //Marshal.Copy(data, 0, payload, (int)payload_len);
                    //string str = BitConverter.ToString(data, 0);
                    IntPtr ptr = payload;
                    Int32 stagenum = *(Int32*)ptr;
                    MRS_LOG_DEBUG("STAGE NUM: {0}", stagenum);

                    GameManager.StageNumSelect(stagenum);
                }
                break;

            //---------------------------------------------- ゲーム進行中の座標系 0x1#

            // 0x12 : [data.id]番目のプレイヤーの座標データが送られてきた
            case 0x12:
                {
                    //MRS_LOG_DEBUG("RECEIVED DATA:{0}", payload);
                    S_DataPlayer data = (S_DataPlayer)Marshal.PtrToStructure(payload, typeof(S_DataPlayer));
                    if (GameManager.players[data.id] != null)
                    {
                        GameManager.players[data.id].GetComponent<Player>().receivePos = new Vector2(data.x, data.y);
                        GameManager.players[data.id].transform.eulerAngles = new Vector3(0.0f, 0.0f, data.angle);

                        // 送信側プレイヤーが死んでいるなら受信側のクライアントでも死なす
                        if (data.dead) { GameManager.players[data.id].transform.GetComponent<Player>().isDead = data.dead; }
                    }
                    //MRS_LOG_DEBUG("RECEIVED DATA  pos_x:{0} pos_y:{1} pos_z:{2} look:{3} move:{4} ammos:{5}"
                    //    data.x, data.y, data.z, data.angle, data.move_a, data.ammos);
                }
                break;

            // 0x13 : 発射された弾の座標と角度のデータが送られてきた
            case 0x13:
                {
                    S_DataShots data = (S_DataShots)Marshal.PtrToStructure(payload, typeof(S_DataShots));

                    GameManager.BulletPlace(new Vector2(data.x, data.y), Quaternion.AngleAxis(data.angle, Vector3.forward), data.whos_shot, data.bullet_id);

                }
                break;

            // 0x11 : 10/18現在未使用のゲーム中情報送受信用
            case 0x11:
                {
                    stopwatch.Start();
                    //MRS_LOG_DEBUG("RECEIVED PACK:{0}", payload);
                    unsafe
                    {
                        S_DataPlayerPackage package = *(S_DataPlayerPackage*)payload;
                        S_DataPlayer[] data = new S_DataPlayer[g_playercount];

                        data[0] = new S_DataPlayer();
                        data[0] = package.data0;
                        data[1] = new S_DataPlayer();
                        data[1] = package.data1;
                        data[2] = new S_DataPlayer();
                        data[2] = package.data2;
                        data[3] = new S_DataPlayer();
                        data[3] = package.data3;

                        for (int i = 0; i < g_playercount; i++)
                        {
                            if (GameManager.players[i] != null && GameManager.playID != i)
                            {
                                if (!GameManager.players[i].GetComponent<Player>().isDead)
                                {
                                    GameManager.players[i].transform.position = new Vector3(data[i].x, data[i].y, 0);
                                    GameManager.players[i].transform.eulerAngles = new Vector3(0.0f, 0.0f, data[i].angle);

                                    // 送信側プレイヤーが死んでいるなら受信側のクライアントでも死なす
                                    if (data[i].dead) { GameManager.players[i].transform.GetComponent<Player>().isDead = data[i].dead; }
                                    //MRS_LOG_DEBUG("RECEIVED DATA  id:{0} pos_x:{1} pos_y:{2} look:{3} dead:{4}",
                                    //data[i].id, data[i].x, data[i].y, data[i].angle, data[i].dead);
                                }
                            }
                        }
                        stopwatch.Stop();
                        //MRS_LOG_DEBUG("RECEIVED DATA TIME : {0}", stopwatch.ElapsedMilliseconds);
                        stopwatch.Reset();
                    }
                }
                break;

            // 0x15 床の落下の合図
            case 0x15:
                {
                    MRS_LOG_DEBUG("RECEIVE 0x15 Turn on to FALL FLOOR");
                    GameManager.FallFloor();
                }
                break;


            //--------------------------------------- 死亡関係 0x2#

            // 0x21 落下死したプレイヤーのID
            case 0x21:
                unsafe{
                    Int32 fellPlayerID = *(Int32*)payload;
                    MRS_LOG_DEBUG("Player No.{0} was falling out",fellPlayerID);
                    GameManager.PlayerDeadFall(fellPlayerID);
                }
                break;

            // 0x22 被弾死したプレイヤーのID
            case 0x22:
                unsafe
                {
                    S_DeadHit data = (S_DeadHit)Marshal.PtrToStructure(payload, typeof(S_DeadHit));

                    MRS_LOG_DEBUG("Player No.{0} was shot by Player No.{1} at Bullet No.{2}", data.player_id, data.whosby_id, data.bullet_id);
                    GameManager.PlayerDeadHit(data.player_id, data.bullet_id);
                }
                break;

			//--------------------------------------- シーンの切り替え 0x3#
			case 0x31:
				{
					
				}
				break;
			default: { } break;
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
		//MRS_LOG_DEBUG( "on_key_exchange" );
		//connected = true;
		//g_paytype = 0x01;
		//IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(netsettings.GetMyProfile()));
		//Marshal.StructureToPtr(netsettings.GetMyProfile(), p_data, false);
		//if (g_nowconnect != null)
		//{
		//    mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(netsettings.GetMyProfile()));
		//}
		//Marshal.FreeHGlobal(p_data);

		SendProfileData();

    }
    
    // ソケット接続時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsConnectCallback))]
    private static void on_connect( MrsConnection connection, IntPtr connection_data ){
        MRS_LOG_DEBUG( "on_connect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version( MRS_VERSION_KEY ), mrs_connection_get_remote_version( connection, MRS_VERSION_KEY ) );

        if ( g_IsKeyExchange ){
            mrs_set_cipher( connection, mrs_cipher_create( MrsCipherType.ECDH ) );
            mrs_key_exchange( connection, on_key_exchange );
        }else{
        }

    }
    
    // ソケット切断時に呼ばれる
    [AOT.MonoPInvokeCallback(typeof(MrsDisconnectCallback))]
    private static void on_disconnect( MrsConnection connection, IntPtr connection_data ){
        MRS_LOG_DEBUG( "on_disconnect local_mrs_version=0x{0:X} remote_mrs_version=0x{1:X}",
            mrs_get_version( MRS_VERSION_KEY ), mrs_connection_get_remote_version( connection, MRS_VERSION_KEY ) );
        connected = false;
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
        //MRS_LOG_DEBUG( "on_fallback_connect connection_type="+ request.ConnectionType +" addr="+ request.Addr +" port="+ request.Port +" timeout_msec="+ request.TimeoutMsec );
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
        //MRS_LOG_DEBUG( "connection_type={0} is_key_exchange={1} is_encrypt_records={2} write_data_len={3} write_count={4} connections={5} server_addr={6} server_port={7} timeout_msec={8} is_valid_record={9} connection_path={10}",
        //    g_ArgConnectionType, g_ArgIsKeyExchange, g_ArgIsEncryptRecords, g_ArgWriteDataLen, g_ArgWriteCount, g_ArgConnections,
        //    g_ArgServerAddr, g_ArgServerPort, g_ArgTimeoutMsec, g_ArgIsValidRecord, g_ArgConnectionPath );

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
        connect_request.Addr           = GameManager.ipAddress;
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
                    //MRS_LOG_ERR( "mrs_connect[{0}]: {1}", i, ToString( mrs_get_error_string( mrs_get_last_error() ) ) );
                    break;
                }
            }
        }while ( false );
    }
    
    void FixedUpdate(){
        updatefix = !updatefix;
        if (updatefix)
        {
            if (g_gameon)
            {
                g_paytype = 0x12;
                CompareMyData();
            }
            if (connected && !g_gameon)
            {
                //g_gameon = true;
                //mrs.Utility.LoadScene("ProtoScene");
            }
        }
            mrs_update();
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

    /// <summary>
    /// 毎フレーム自分のデータを送信
    /// </summary>
    public static void CompareMyData()
    {
		Debug.Log("Compare");
        // 前フレームで死んでいるなら、他プレイヤーに座標データは送信しない
        if (GameManager.players[GameManager.playID] != null)
        {
            //if (!GameManager.players[GameManager.playID].transform.GetComponent<Player>().isDead)
            //{
                myNewData.id = GameManager.playID;
                myNewData.x = GameManager.players[GameManager.playID].transform.position.x;
                myNewData.y = GameManager.players[GameManager.playID].transform.position.y;
                myNewData.angle = GameManager.players[GameManager.playID].transform.eulerAngles.z;
                myNewData.dead = GameManager.players[GameManager.playID].transform.GetComponent<Player>().isDead;
                IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(myNewData));
                Marshal.StructureToPtr(myNewData, p_data, false);
                if (g_nowconnect != null)
                {
                    mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(myNewData));
                    MRS_LOG_DEBUG("SEND MY DATA");
                }
                Marshal.FreeHGlobal(p_data);
                myData = myNewData;
            //}
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
        shot.bullet_id = -1;
        shot.whos_shot = GameManager.playID;
        shot.x = _x;
        shot.y = _y;
        shot.angle = _angle;

        IntPtr p_shotdata = Marshal.AllocHGlobal(Marshal.SizeOf(shot));
        Marshal.StructureToPtr(shot, p_shotdata, false);
        if (g_nowconnect != null)
        {
            g_paytype = 0x13;
            mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_shotdata, (uint)Marshal.SizeOf(shot));
        }
        Marshal.FreeHGlobal(p_shotdata);
    }

    /// <summary>
    /// サーバーへ準備ができた合図を送る
    /// </summary>
    public void SendRoomReady()
    {
        g_paytype = 0x03;
        IntPtr blank = Marshal.AllocHGlobal(Marshal.SizeOf(myNewData));
        Marshal.StructureToPtr(myNewData, blank, false);
        
        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, blank, sizeof(int));

        Marshal.FreeHGlobal(blank);
    }

    
    public void SendPlayerDied()
    {
    }


    /// <summary>
    /// 接続に必要な情報を外部からセット
    /// </summary>
    /// <param name="_ip">IPアドレス</param>
    public void SetSettings(string _ip, string _name)
    {
        GameManager.playerName = _name;
        GameManager.ipAddress = _ip;
        netsettings.SetProfile(-1, _name);
    }


    /// <summary>
    /// サーバー接続の切断
    /// </summary>
    public void DisconnectRoom()
    {
        mrs_close(g_nowconnect);
        g_gameon = false;
    }

    /// <summary>
    /// スタート前にスポーン位置の送信
    /// </summary>
    public void SendStartingPos()
    {
        g_paytype = 0x04;
        CompareMyData();
    }

    public void setRoomManager(RoomManager _roomMng)
    {
        g_roomManager = _roomMng;
    }

    /// <summary>
    /// 床落下のタイミングを送信
    /// </summary>
    public void SendFallFloor()
    {
        g_paytype = 0x15;
        byte[] blank = System.Text.Encoding.ASCII.GetBytes("1");
        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, blank, (uint)blank.Length);
        MRS_LOG_DEBUG("SENT FALL FLOOR");
    }

    public void SendCountDownStart()
    {
        g_paytype = 0x05;
        byte[] blank = System.Text.Encoding.ASCII.GetBytes("1");
        MRS_LOG_DEBUG("SEND COUNT DOWN START");
        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, blank, (uint)blank.Length);
    }

    /// <summary>
    /// あと＊秒のカウントダウンをサーバーに送信
    /// </summary>
    /// <param name="_second">残りのカウントダウン時間</param>
    public unsafe void SendCountDown(int _second)
    {
        g_paytype = 0x06;
        IntPtr sendTime = Marshal.AllocHGlobal(sizeof(Int32));
        *(Int32*)sendTime = _second;
        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, sendTime, (uint)sizeof(Int32));
        MRS_LOG_DEBUG("SENT COUNT DOWN TIME  TIMES LEFT: {0}",_second);
        Marshal.FreeHGlobal(sendTime);
    }

    /// <summary>
    /// 現在選択中のステージ番号を送信
    /// </summary>
    /// <param name="_stagenum">ステージ番号</param>
    public unsafe void SendStageNumber(int _stagenum)
    {
        g_paytype = 0x07;
        IntPtr send = Marshal.AllocHGlobal(sizeof(Int32));
        *(Int32*)send = _stagenum;

        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, send, (uint)sizeof(Int32));
        MRS_LOG_DEBUG("SENT STAGE NUMBER : {0}", _stagenum);

        Marshal.FreeHGlobal(send);
    }

    /// <summary>
    /// ゲームスタートしたらオンにして
    /// </summary>
    public void setGameStartFlag()
    {
        g_gameon = true;
    }

	/// <summary>
	/// ルームに帰るときの処理
	/// </summary>
    public unsafe void backToRoom()
    {
        g_gameon = false;
		SendProfileData();
	}

	/// <summary>
	/// 落下死したらサーバーに自分のIDを送信
	/// </summary>
	public unsafe void SendPlayerDeadFall()
    {
        g_paytype = 0x21;
        IntPtr send = Marshal.AllocHGlobal(sizeof(Int32));
        *(Int32*)send = (Int32)GameManager.playID;

        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, send, (uint)sizeof(Int32));
        MRS_LOG_DEBUG("SENT FELL NUMBER : {0}", GameManager.playID);

        Marshal.FreeHGlobal(send);
    }

    /// <summary>
    /// 被弾死したらサーバーに自分のIDと当たった弾のIDを送信
    /// </summary>
    /// <param name="_bulletID"></param>
    public unsafe void SendPlayerDeadHit(int _bulletID)
    {
        S_DeadHit sendHit = new S_DeadHit();
        sendHit.bullet_id = _bulletID;
        sendHit.player_id = (int)GameManager.playID;
        sendHit.whosby_id = -1;

        g_paytype = 0x22;
        IntPtr send = Marshal.AllocHGlobal(sizeof(S_DeadHit));
        *(S_DeadHit*)send = (S_DeadHit)sendHit;

        mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, send, (uint)sizeof(S_DeadHit));
        MRS_LOG_DEBUG("SENT FELL NUMBER : {0}", GameManager.playID);

        Marshal.FreeHGlobal(send);
    }


	//-----------------------------------------------------------------------
	public void SendOnResult(){
		g_paytype = 0x31;
		g_gameon = false;
		mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, null, 0);
	}

	//-----------------------------------------------------------------------
	static public void SendProfileData() {
		netsettings.SetProfile(-1, GameManager.playerName);
		connected = true;
		g_paytype = 0x01;
		IntPtr p_data = Marshal.AllocHGlobal(Marshal.SizeOf(netsettings.GetMyProfile()));
		Marshal.StructureToPtr(netsettings.GetMyProfile(), p_data, false);
		if (g_nowconnect != null) {
			mrs_write_record(g_nowconnect, g_RecordOptions, g_paytype, p_data, (uint)Marshal.SizeOf(netsettings.GetMyProfile()));
		}
		Marshal.FreeHGlobal(p_data);
		InitMyData();

	}
}