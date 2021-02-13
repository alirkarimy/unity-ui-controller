using System;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

static class ListPackages
{
    static ListRequest Request;
    public static Action<PackageCollection> OnPackagesRetrived;

    internal static void List()
    {
        if (Request != null) return;

        Request = Client.List();    // List packages installed for the project
        EditorApplication.update += Progress;
    }

    static void Progress()
    {
        if (Request.IsCompleted)
        {
            if (Request.Status == StatusCode.Success)
            {
                OnPackagesRetrived?.Invoke(Request.Result);
            }
            else if (Request.Status >= StatusCode.Failure)
                Debug.Log(Request.Error.message);

            EditorApplication.update -= Progress;
            Request = null;
        }
    }

}
