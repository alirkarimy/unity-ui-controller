﻿//#define ANDROID_NATIVE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CUtils
{
#if ANDROID_NATIVE && UNITY_ANDROID && !UNITY_EDITOR
	private static AndroidJavaClass cls_UnityPlayer;
	private static AndroidJavaObject obj_Activity;
#endif

    static CUtils()
    {
#if ANDROID_NATIVE && UNITY_ANDROID && !UNITY_EDITOR
		AndroidJNI.AttachCurrentThread();
		cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }

    public static bool IsAppInstalled(string packageName)
    {
#if ANDROID_NATIVE && UNITY_ANDROID && !UNITY_EDITOR
		return obj_Activity.Call<bool>("isAppInstalled", packageName);
#else
        return false;
#endif
    }

    public static void PushNotification(int id)
    {
#if ANDROID_NATIVE && UNITY_ANDROID && !UNITY_EDITOR
		obj_Activity.Call("pushNotificaiton", id);
#endif
    }
    

    public static void OpenStore(string id)
    {
#if UNITY_EDITOR || UNITY_ANDROID
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + id);
        //#elif ANDROID_NATIVE && UNITY_ANDROID 
        //		obj_Activity.Call("OpenStore", id);
#elif UNITY_IPHONE || UNITY_IOS
		Application.OpenURL("https://itunes.apple.com/app/id" + id);
#elif UNITY_BLACKBERRY
		Application.OpenURL("https://appworld.blackberry.com/webstore/content/" + id);
#elif UNITY_STANDALONE_OSX
		Application.OpenURL("https://itunes.apple.com/app/id" + id);
#endif
    }

    public static void LikeInstagramPage(string instaID)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		Application.OpenURL("https://www.instagram.com/" + instaID);
#else
        Application.OpenURL("https://www.instagram.com/" + instaID);
#endif
        //SetLikeFbPage(faceID);
    }

    public static void LikeTelegramChannel(string telegramID)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		Application.OpenURL("https://t.me/" + telegramID);
#else
        Application.OpenURL("https://t.me/" + telegramID);
#endif
        //SetLikeFbPage(faceID);
    }

    public static string ReadFileContent(string path)
    {
        TextAsset file = Resources.Load(path) as TextAsset;
        return file == null ? null : file.text;
    }

    public static Vector3 CopyVector3(Vector3 ori)
    {
        Vector3 des = new Vector3(ori.x, ori.y, ori.z);
        return des;
    }

    public static bool EqualVector3(Vector3 v1, Vector3 v2)
    {
        return Vector3.SqrMagnitude(v1 - v2) <= 0.0000001f;
    }

    public static float GetSign(Vector3 A, Vector3 B, Vector3 M)
    {
        return Mathf.Sign((B.x - A.x) * (M.y - A.y) - (B.y - A.y) * (M.x - A.x));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    public static void Shuffle<T>(params T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            var temp = array[i];
            var randomIndex = UnityEngine.Random.Range(0, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    public static void ShuffleChilds(Transform root)
    {
        if (root == null) return;
        if (root.childCount <= 1) return;

        int childCount = root.childCount;
        List<Vector3> _tempPositions = new List<Vector3>();
        for (int i = 0; i < childCount; i++)
        {
            Vector3 temp = root.GetChild(i).transform.localPosition;
            var randomIndex = UnityEngine.Random.Range(0, childCount);
            root.GetChild(i).transform.localPosition = root.GetChild(randomIndex).transform.localPosition;
            root.GetChild(randomIndex).transform.localPosition = temp;
        }

    }

    public static string[] SeparateLines(string lines)
    {
        return lines.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n"[0]);
    }

    public static void ChangeSortingLayerRecursively(Transform root, string sortingLayerName, int offsetOrder = 0)
    {
        SpriteRenderer renderer = root.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder += offsetOrder;
        }

        foreach (Transform child in root)
        {
            ChangeSortingLayerRecursively(child, sortingLayerName, offsetOrder);
        }
    }

    public static void ChangeRendererColorRecursively(Transform root, Color color)
    {
        SpriteRenderer renderer = root.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }

        foreach (Transform child in root)
        {
            ChangeRendererColorRecursively(child, color);
        }
    }

    public static void ChangeImageColorRecursively(Transform root, Color color)
    {
        Image image = root.GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }

        foreach (Transform child in root)
        {
            ChangeImageColorRecursively(child, color);
        }
    }

    public static string GetFacePictureURL(string facebookID, int? width = null, int? height = null, string type = null)
    {
        string url = string.Format("/{0}/picture", facebookID);
        string query = width != null ? "&width=" + width.ToString() : "";
        query += height != null ? "&height=" + height.ToString() : "";
        query += type != null ? "&type=" + type : "";
        query += "&redirect=false";
        if (query != "")
            url += ("?g" + query);
        return url;
    }

    public static double GetCurrentTime()
    {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        return span.TotalSeconds;
    }

    public static double GetCurrentTimeInDays()
    {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        return span.TotalDays;
    }

    public static double GetCurrentTimeInMills()
    {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        return span.TotalMilliseconds;
    }

    public static double GetTimeInMills(DateTime myDateTime)
    {
        return myDateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }

    public static double GetDiffTimeInMills(DateTime myDateTime)
    {
        return myDateTime.ToUniversalTime().Subtract(DateTime.UtcNow).TotalMilliseconds;
    }

    public static int ConvertSecondsToDays(double seconds)
    {
        float _seconds = (float)seconds;
        return Mathf.FloorToInt(_seconds / (3600*24));
    }


    public static T GetRandom<T>(params T[] arr)
    {
        return arr[UnityEngine.Random.Range(0, arr.Length)];
    }

 

    public static string BuildStringFromCollection(ICollection values, char split = '|')
    {
        string results = "";
        int i = 0;
        foreach (var value in values)
        {
            results += value;
            if (i != values.Count - 1)
            {
                results += split;
            }
            i++;
        }
        return results;
    }

    public static List<T> BuildListFromString<T>(string values, char split = '|')
    {
        List<T> list = new List<T>();
        if (string.IsNullOrEmpty(values))
            return list;

        string[] arr = values.Split(split);
        List<string> sortedByLenghValues = new List<string>();
        foreach (string value in arr)
        {
            if (string.IsNullOrEmpty(value)) continue;
            sortedByLenghValues.Add(value);
        }
        sortedByLenghValues= sortedByLenghValues.OrderBy(str => str.Length).ToList();

        foreach (string value in sortedByLenghValues)
        {
            if (string.IsNullOrEmpty(value)) continue;
            T val = (T)Convert.ChangeType(value, typeof(T));
            list.Add(val);
        }
        return list;
    }

#if UNITY_EDITOR
    public static string[] GetSortingLayerNames()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }

    public static int[] GetSortingLayerUniqueIDs()
    {
        Type internalEditorUtilityType = typeof(InternalEditorUtility);
        PropertyInfo sortingLayerUniqueIDsProperty = internalEditorUtilityType.GetProperty("sortingLayerUniqueIDs", BindingFlags.Static | BindingFlags.NonPublic);
        return (int[])sortingLayerUniqueIDsProperty.GetValue(null, new object[0]);
    }
#endif
    

    public static List<T> GetObjectInRange<T>(Vector3 position, float radius, int layerMask = Physics2D.DefaultRaycastLayers) where T : class
    {
        List<T> list = new List<T>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, layerMask);

        foreach (Collider2D col in colliders)
        {
            list.Add(col.gameObject.GetComponent(typeof(T)) as T);
        }
        return list;
    }

    public static Sprite GetSprite(string textureName, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(textureName);
        foreach (Sprite sprite in sprites)
        {
            if (sprite.name == spriteName)
            {
                return sprite;
            }
        }
        return null;
    }

    public static List<Transform> GetActiveChildren(Transform parent)
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in parent)
        {
            if (child.gameObject.activeSelf) list.Add(child);
        }
        return list;
    }

    public static List<Transform> GetChildren(Transform parent)
    {
        List<Transform> list = new List<Transform>();
        foreach (Transform child in parent)
        {
            list.Add(child);
        }
        return list;
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public static void ReloadScene(bool useScreenFader = false)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene(currentIndex);
    }

    public static void CheckConnection(MonoBehaviour behaviour, Action<int> connectionListener)
    {
        behaviour.StartCoroutine(ConnectUrl("http://www.google.com", connectionListener));
    }

    private static IEnumerator ConnectUrl(string url, Action<int> connectionListener)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
            connectionListener(1);
        else if (string.IsNullOrEmpty(www.text))
            connectionListener(2);
        else
            connectionListener(0);
    }

    public static void CheckDisconnection(MonoBehaviour behaviour, Action onDisconnected)
    {
        behaviour.StartCoroutine(ConnectUrl("http://www.google.com", onDisconnected));
    }

    private static IEnumerator ConnectUrl(string url, Action onDisconnected)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            yield return new WaitForSeconds(2f);
            www = new WWW(url);
            yield return www;

            if (www.error != null)
                onDisconnected();
        }
    }


    public static IEnumerator LoadPicture(string url, Action<Texture2D> callback, int width, int height, bool useCached = true)
    {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WEBPLAYER
        string localPath = GetLocalPath(url);
        bool loaded = false;

        if (useCached)
        {
            loaded = LoadFromLocal(callback, localPath, width, height);
        }

        if (!loaded)
        {
            WWW www = new WWW(url);
            yield return www;
            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                callback(www.texture);
                System.IO.File.WriteAllBytes(localPath, www.bytes);
            }
            else
            {
                LoadFromLocal(callback, localPath, width, height);
            }
        }
#else
        yield return null;
#endif
    }

    private static string GetLocalPath(string url)
    {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WEBPLAYER
        string justFilename = System.IO.Path.GetFileName(new Uri(url).LocalPath);
        return Application.persistentDataPath + "/" + justFilename;
#else
        return null;
#endif
    }

    public static IEnumerator CachePicture(string url, Action<bool> result)
    {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WEBPLAYER
        string localPath = GetLocalPath(url);
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            System.IO.File.WriteAllBytes(localPath, www.bytes);
            if (result != null) result(true);
        }
        else
        {
            if (result != null) result(false);
        }
#else
        yield return null;
#endif
    }

    public static bool IsCacheExists(string url)
    {
#if !UNITY_WSA && !UNITY_WP8
        return System.IO.File.Exists(GetLocalPath(url));
#else
        return false;
#endif
    }

    private static bool LoadFromLocal(Action<Texture2D> callback, string localPath, int width, int height)
    {
#if !UNITY_WSA && !UNITY_WP8 && !UNITY_WEBPLAYER
        if (System.IO.File.Exists(localPath))
        {
            var bytes = System.IO.File.ReadAllBytes(localPath);
            var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
            tex.LoadImage(bytes);
            if (tex != null)
            {
                callback(tex);
                return true;
            }
        }
        return false;
#else
        return false;
#endif
    }

    public static Sprite CreateSprite(Texture2D texture, int width, int height)
    {
        return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public static List<List<T>> Split<T>(List<T> source, Predicate<T> split)
    {
        List<List<T>> result = new List<List<T>>();
        bool begin = false;
        for (int i = 0; i < source.Count; i++)
        {
            T element = source[i];
            if (split(element))
            {
                begin = false;
            }
            else
            {
                if (begin == false)
                {
                    begin = true;
                    result.Add(new List<T>());
                }
                result[result.Count - 1].Add(element);
            }
        }
        return result;
    }

    public static List<List<T>> GetArrList<T>(List<T> source, Predicate<T> take)
    {
        List<List<T>> result = new List<List<T>>();
        bool begin = false;
        foreach (T element in source)
        {
            if (take(element))
            {
                if (begin == false)
                {
                    begin = true;
                    result.Add(new List<T>());
                }
                result[result.Count - 1].Add(element);
            }
            else
            {
                begin = false;
            }
        }
        return result;
    }

    public static List<T> ToList<T>(T obj)
    {
        List<T> list = new List<T>();
        list.Add(obj);
        return list;
    }

    public static int ChooseRandomWithProbs(float[] probs)
    {
        float total = 0;
        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = UnityEngine.Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    public static bool IsObjectSeenByCamera(Camera camera, GameObject gameObj, float delta = 0)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(gameObj.transform.position);
        return (screenPoint.z > 0 && screenPoint.x > -delta && screenPoint.x < 1 + delta && screenPoint.y > -delta && screenPoint.y < 1 + delta);
    }

    public static Vector3 GetMiddlePoint(Vector3 begin, Vector3 end, float delta = 0)
    {
        Vector3 center = Vector3.Lerp(begin, end, 0.5f);
        Vector3 beginEnd = end - begin;
        Vector3 perpendicular = new Vector3(-beginEnd.y, beginEnd.x, 0).normalized;
        Vector3 middle = center + perpendicular * delta;
        return middle;
    }

    public static double RoundDecimalNumber(double num,int decimalNum)
    {
        return Math.Round(num, decimalNum);
    }
    public static AnimationClip GetAnimationClip(Animator anim, string name)
    {
        var ac = anim.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name) return ac.animationClips[i];
        }
        return null;
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static void Pause()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPaused = true;
#endif
    }
}
