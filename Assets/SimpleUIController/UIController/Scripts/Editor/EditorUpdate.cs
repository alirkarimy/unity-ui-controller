using System.IO;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class EditorUpdate
{
    static EditorUpdate()
    {        
        EditorApplication.update += Update;
        Application.logMessageReceived += OnLogMessageReceived;
    }

    static void Update()
    {
        EditorApplication.update -= Update;
        AddressableChecker.CheckItNow();
    }
   
    private static void OnLogMessageReceived(string condition, string stackTrace, LogType type)
    {
        /*
         * Check for having issue with Addressable
         * In case of error we will remove related Symbols that we had added before
        */
        if (type == LogType.Error && condition.Contains("Addressable"))
        {
            AddressableChecker.CheckItNow();
        }
    }
}
