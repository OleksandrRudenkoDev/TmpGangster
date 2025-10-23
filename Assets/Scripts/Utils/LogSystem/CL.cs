using UnityEngine;

namespace Utils.LogSystem
{
    public class CL : MonoBehaviour
    {
        [SerializeField]
        private Logs _allowedLogs;

        private static CL _instance;

        public static void Log(string message, Logs logPermission = Logs.None, LogType logType = LogType.Log)
        {
            if(_instance == null)
            {
                LogMessage(message, logType);
            } else
            {
                _instance.LogMessage(message, logPermission, logType);
            }
        }

        private static void LogMessage(string message, LogType logType)
        {
            switch(logType)
            {
                case LogType.Error:
                    UnityEngine.Debug.LogError(message);
                    break;
                case LogType.Assert:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogType.Warning:
                    UnityEngine.Debug.LogWarning(message);
                    break;
                case LogType.Log:
                    UnityEngine.Debug.Log(message);
                    break;
                case LogType.Exception:
                    UnityEngine.Debug.LogError(message);
                    break;
                default:
                    UnityEngine.Debug.Log(message);
                    break;
            }
        }

        public CL()
        {
            _instance = this;
        }

        private void LogMessage(string message, Logs logPermission = Logs.None, LogType logType = LogType.Log)
        {
            if(!_allowedLogs.HasFlag(logPermission))
            {
                return;
            }

            LogMessage(message, logType);
        }
    }
    public enum Logs
    {
        None,
        SaveLogs,
        StateMachineLogs
    }
}