using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using Utils;
using System;

public class Debugger : MonoBehaviour
{
	public class DebugLogVO
	{
		public string logString;
		public string stackTrace;
		public LogType logType;

		public DebugLogVO ()
		{
			
		}

		public DebugLogVO (string logString, string stackTrace, LogType logType)
		{
			this.logString = logString;
			this.stackTrace = stackTrace;
			this.logType = logType;
		}
	}

	public static TextType writeType = TextType.None;
	public static int channel;

	public TextType _writeType = TextType.None;
	public int _channel = (int)LogChannel.Default;
	public bool resetAtStart = true;
	public string fileName;

	public bool _isLogEnable = true;
	public bool _isWarningEnable = true;
	public bool _isErrorEnable = true;
	public bool _isExceptionEnable = true;
	public bool _isAssertEnable = true;
	public bool _isColorenable = true;


	public static bool isLogEnable = true;
	public static bool isWarningEnable = true;
	public static bool isErrorEnable = true;
	public static bool isExceptionEnable = true;
	public static bool isAssertEnable = true;
	public static bool isColorenable = true;


	private string commonString = null;
	private List<DebugLogVO> threadedLogs = new List<DebugLogVO> ();

	void Awake ()
	{
		Application.logMessageReceivedThreaded += CaptureLogThread;
		#if UNITY_EDITOR
		writeType = _writeType;
		channel = _channel;
		//        _channel = channel;

		isLogEnable = _isLogEnable;
		isWarningEnable = _isWarningEnable;
		isErrorEnable = _isErrorEnable;
		isExceptionEnable = _isExceptionEnable;
		isAssertEnable = _isAssertEnable;
		isColorenable = _isColorenable;
		#endif
	}

	void Start ()
	{
     
		#if UNITY_EDITOR
		writeType = _writeType;
		channel = _channel;
		//        _channel = channel;

		isLogEnable = _isLogEnable;
		isWarningEnable = _isWarningEnable;
		isErrorEnable = _isErrorEnable;
		isExceptionEnable = _isExceptionEnable;
		isAssertEnable = _isAssertEnable;
		isColorenable = _isColorenable;
		#endif
		if (resetAtStart)
			Reset (writeType, fileName);
	}


	void Update ()
	{
		#if UNITY_EDITOR
		if (writeType != _writeType) {
			writeType = _writeType; 
		}
		#endif

		if (_channel != channel) {
			channel = _channel;
		}

		isLogEnable = _isLogEnable;
		isWarningEnable = _isWarningEnable;
		isErrorEnable = _isErrorEnable;
		isExceptionEnable = _isExceptionEnable;
		isAssertEnable = _isAssertEnable;
		isColorenable = _isColorenable;

		if (threadedLogs.Count > 0) {
			lock (threadedLogs) {
				for (int i = 0; i < threadedLogs.Count; i++) {
					DebugLogVO l = threadedLogs [i];
//					HandleLog (l.logString, l.stackTrace, (LogType)l.logType);
				}
				threadedLogs.Clear ();
			}
		}
	}

	public static void Reset (TextType debugLog = TextType.None, string fileName = "")
	{
		if (debugLog != TextType.None) {
			TextToFile.Reset (debugLog, fileName);
		}	
	}

	void OnDestroy ()
	{
		CloseDegbug ();
		Application.logMessageReceivedThreaded -= CaptureLogThread;
	}

	public static void CloseDegbug ()
	{
		TextToFile.CloseDebug ();
	}

	public static void Log (object message, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isLogEnable)
			return;
		if ((Debugger.channel & (int)channel) != 0) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.Log (message);
		}
	}

	public static void Log (object message, UnityEngine.Object context, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isLogEnable)
			return;
		if ((Debugger.channel & (int)channel) != 0) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.Log (message, context);
		}
	}

	public static void LogWarning (object message, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isWarningEnable)
			return;
		if ((Debugger.channel & (int)channel) != 0) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.LogWarning (message);
		}
	}

	public static void LogWarning (object message, UnityEngine.Object context, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isWarningEnable)
			return;
		if ((Debugger.channel & (int)channel) != 0) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.LogWarning (message, context);
		}
	}

	public static void LogError (object message, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isErrorEnable)
			return;
		if ((Debugger.channel >= (int)channel)) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.LogError (message);
		}
	}

	public static void LogError (object message, UnityEngine.Object context, string logColor = LogColor.none, LogChannel channel = LogChannel.Default)
	{
		if (!isErrorEnable)
			return;
		if ((Debugger.channel >= (int)channel)) {
			if (logColor != LogColor.none && isColorenable)
				message = string.Format (logColor, message.ToString ());
			Debug.LogError (message, context);
		}
	}

	public static void LogException (Exception exception, LogChannel channel = LogChannel.Default)
	{
		if (!isExceptionEnable)
			return;
		if ((Debugger.channel >= (int)channel)) {
			Debug.LogException (exception);
		}
	}

	public static void LogException (Exception exception, UnityEngine.Object context, LogChannel channel = LogChannel.Default)
	{
		if (!isExceptionEnable)
			return;
		if ((Debugger.channel >= (int)channel)) {
			Debug.LogException (exception, context);
		}
	}

	private void CaptureLogThread (string logString, string stackTrace, LogType type)
	{
		DebugLogVO vo = new DebugLogVO (logString, stackTrace, type);
		lock (threadedLogs) {
			threadedLogs.Add (vo);
		}
	}


	private void SendLogToServer (string message)
	{
		
//		C_Error_Report_0x560 cmd = new C_Error_Report_0x560 ();
//		cmd.content = b.Get ();
//		ModuleContext.GetInstance<PacketManager> ().SendMessage<C_Error_Report_0x560> (cmd);
//		ModuleContext.GetInstance<PersistService> ().LogShared ("logError.log", cmd.content);
//
//		if (PlatformManager.instance == null || !PlatformManager.instance.IsGameServerLogined) {
//			anysdk.AnySDKAnalytics.getInstance ().logError ("Error", cmd.content);
//		}
	}
}
