#pragma warning disable 0162 // code unreached.
#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.
#pragma warning disable 0618 // obslolete
#pragma warning disable 0108 
#pragma warning disable 0649 //never used
#pragma warning disable 0429 //never used

using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using System.IO;
using UnityEditor.PackageManager.Requests;
using System;

[InitializeOnLoad]
public class AddressableChecker : EditorWindow 
{
	private const string AsyncSymbol = "async";
    private static AddressableChecker Instance;

    #region Check Package

    public static void CheckItNow()
    {
        ListUnityPackages.List();
        return;
    }

    static AddressableChecker()
	{
        ListUnityPackages.OnPackagesRetrived = OnPackageListReceived;
	}

    private static void OnPackageListReceived(PackageCollection packages)
    {
        foreach (var package in packages)
        {
            if (package.name.ToLower().Contains("addressable"))
            {
                SetScriptingDefineSymbols();

                if (Instance != null)
                {
                    Instance.Close();
                }
                return;
            }
        }

        RemoveScriptingDefineSymbols();
        OpenWelcomeWindow();

    }

    static void SetSymbolsForTarget(BuildTargetGroup target, string scriptingSymbol)
    {
        var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

        string sTemp = scriptingSymbol;

        if (!s.Contains(sTemp))
        {

            s = s.Replace(scriptingSymbol + ";", "");

            s = s.Replace(scriptingSymbol, "");

            s = scriptingSymbol + ";" + s;

            PlayerSettings.SetScriptingDefineSymbolsForGroup(target, s);
        }
    }

    static void RemoveSymbolsForTarget(BuildTargetGroup target, string scriptingSymbol)
    {
        var s = PlayerSettings.GetScriptingDefineSymbolsForGroup(target);

        string sTemp = scriptingSymbol;

        if (s.Contains(sTemp))
        {

            s = s.Replace(scriptingSymbol + ";", "");

            s = s.Replace(scriptingSymbol, "");

            PlayerSettings.SetScriptingDefineSymbolsForGroup(target, s);
        }
    }

    #endregion

    #region Popup Window 
   
    static void SetScriptingDefineSymbols()
    {
        SetSymbolsForTarget(BuildTargetGroup.Android, AsyncSymbol);
        SetSymbolsForTarget(BuildTargetGroup.iOS, AsyncSymbol);
        SetSymbolsForTarget(BuildTargetGroup.WSA, AsyncSymbol);
        SetSymbolsForTarget(BuildTargetGroup.Standalone, AsyncSymbol);
    }

    static void RemoveScriptingDefineSymbols()
    {
        RemoveSymbolsForTarget(BuildTargetGroup.Android, AsyncSymbol);
        RemoveSymbolsForTarget(BuildTargetGroup.iOS, AsyncSymbol);
        RemoveSymbolsForTarget(BuildTargetGroup.WSA, AsyncSymbol);
        RemoveSymbolsForTarget(BuildTargetGroup.Standalone, AsyncSymbol);
    }

    public void OnGUI()
    {
        GUILayoutUtility.GetRect(position.width, 50);

        GUI.skin.label.wordWrap = true;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label("1. Open Window->PackageManager\n",  GUILayout.MaxWidth(350));
        GUILayout.Label("2. Find Addressable package (Make sure that you hace checked 'All Packages' not 'In Project')\n", GUILayout.MaxWidth(350*2));
        GUILayout.Label("3. Select suitable version according to your Unity version\n", GUILayout.MaxWidth(350));
        GUILayout.Label("4. Install the plugin", GUILayout.MaxWidth(350));
    }

    void OnEnable()
    {
        Instance = this;
#if UNITY_5_3_OR_NEWER
        titleContent = new GUIContent("Please import Unity Addressable to activate async mode");
#endif
    }

    public static bool IsOpen
    {
        get { return Instance != null; }
    }

    public static void OpenWelcomeWindow()
    {
        var window = GetWindow<AddressableChecker>(true);
        window.position = new Rect(700, 400, 380, 200);
    }

    #endregion


}
