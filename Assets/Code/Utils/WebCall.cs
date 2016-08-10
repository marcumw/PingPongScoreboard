using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using System.Text;

/// <summary>
/// Unity3D WWW class wrapper that dispatches an event when it's done so you
/// don't have to keep checking the isDone field or use it from a coroutine.
/// </summary>
/// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3021</author>
/// <license>MIT</license>
public sealed class WebCall : IDisposable
{
    private static readonly Encoding UTF8Encoding = Encoding.UTF8;

    private MonoBehaviour monoBehaviour;
    private MonoBehaviour nextMonoBehaviour;
    private WWW www;
    private bool isDisposed;

    public delegate void OnDoneHandler(WebCall webCall);

    private OnDoneHandler onDoneInvoker = webCall => { };

    private WebCall(
        MonoBehaviour monoBehaviour,
        WWW www
    )
    {
        this.monoBehaviour = monoBehaviour;
        this.www = www;
        StartCoroutine();
    }

    public WebCall(
        MonoBehaviour monoBehaviour,
        string url
    )
    {
        this.monoBehaviour = monoBehaviour;
        www = new WWW(url);
        StartCoroutine();
    }

    public WebCall(
        MonoBehaviour monoBehaviour,
        string url,
        WWWForm form
    )
    {
        this.monoBehaviour = monoBehaviour;
        www = new WWW(url, form);
        StartCoroutine();
    }

    public WebCall(
        MonoBehaviour monoBehaviour,
        string url,
        byte[] postData
    )
    {
        this.monoBehaviour = monoBehaviour;
        www = new WWW(url, postData);
        StartCoroutine();
    }

    public WebCall(
        MonoBehaviour monoBehaviour,
        string url,
        byte[] postData,
        Dictionary<string, string> headers
    )
    {
        this.monoBehaviour = monoBehaviour;
        www = new WWW(url, postData, headers);
        StartCoroutine();
    }

    public event OnDoneHandler OnDone
    {
        add { onDoneInvoker += value; }
        remove { onDoneInvoker -= value; }
    }

    public MonoBehaviour MonoBehaviour
    {
        get { return monoBehaviour; }
    }

    public void UseDifferentMonoBehaviour(
        MonoBehaviour monoBehaviour
    )
    {
        nextMonoBehaviour = monoBehaviour;
    }

    public AssetBundle AssetBundle
    {
        get { return www.assetBundle; }
    }

    public AudioClip AudioClip
    {
        get { return www.audioClip; }
    }

    public byte[] Bytes
    {
        get { return www.bytes; }
    }

    public int BytesDownloaded
    {
        get { return www.bytesDownloaded; }
    }

    public string Error
    {
        get { return www.error; }
    }

    public bool IsDone
    {
        get { return www.isDone; }
    }

    //public MovieTexture Movie
    //{
    //    get { return www.movie; }
    //}

    public float Progress
    {
        get { return www.progress; }
    }

    public Dictionary<string, string> ResponseHeaders
    {
        get { return www.responseHeaders; }
    }

    public string Text
    {
        get { return www.text; }
    }

    public Texture2D Texture
    {
        get { return www.texture; }
    }

    public Texture2D TextureNonReadable
    {
        get { return www.textureNonReadable; }
    }

//    public ThreadPriority ThreadPriority
//    {
//        get { return www.threadPriority; }
//        set { www.threadPriority = value; }
//    }

    public float UploadProgress
    {
        get { return www.uploadProgress; }
    }

    public string URL
    {
        get { return www.url; }
    }

    public void Dispose()
    {
        try
        {
            isDisposed = true;
            www.Dispose();
        }
        catch (Exception ex)
        {
            //ManagerGui.setDebugText("web call already disposed");
            throw;
        }

    }

    public AudioClip GetAudioClip(
        bool threeD
    )
    {
        return www.GetAudioClip(threeD);
    }

    public AudioClip GetAudioClip(
        bool threeD,
        bool stream
    )
    {
        return www.GetAudioClip(threeD, stream);
    }

    public AudioClip GetAudioClip(
        bool threeD,
        bool stream,
        AudioType audioType
    )
    {
        return www.GetAudioClip(threeD, stream, audioType);
    }

    public AudioClip GetAudioClipCompressed()
    {
        return www.GetAudioClipCompressed();
    }

    public AudioClip GetAudioClipCompressed(
        bool threeD
    )
    {
        return www.GetAudioClipCompressed(threeD);
    }

    public AudioClip GetAudioClipCompressed(
        bool threeD,
        AudioType audioType
    )
    {
        return www.GetAudioClipCompressed(threeD, audioType);
    }

    public void LoadImageIntoTexture(Texture2D tex)
    {
        www.LoadImageIntoTexture(tex);
    }

    public static string EscapeURL(
        string s,
        Encoding e = null
    )
    {
        return WWW.EscapeURL(s, e ?? UTF8Encoding);
    }

    public static WebCall LoadFromCacheOrDownload(
        MonoBehaviour monoBehaviour,
        string url,
        int version,
        uint crc = 0
    )
    {
        var www = WWW.LoadFromCacheOrDownload(url, version, crc);
        return new WebCall(monoBehaviour, www);
    }

    public static string UnEscapeURL(
        string s,
        Encoding e = null
    )
    {
        return WWW.UnEscapeURL(s, e ?? UTF8Encoding);
    }

    private void StartCoroutine()
    {
        if (monoBehaviour)
        {
            monoBehaviour.StartCoroutine(Coroutine());
        }
        else
        {
            var msg = "Web call to URL (" + URL + ") does not have a valid "
                + "MonoBehaviour to run a coroutine on. "
                + "The OnDone() event will never fire.";
            Debug.LogError(msg);
        }
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            // Requested to switch MonoBehaviour the coroutine is on
            if (nextMonoBehaviour)
            {
                monoBehaviour = nextMonoBehaviour;
                nextMonoBehaviour = null;
                StartCoroutine();
                yield break;
            }

            // WWW has been disposed
            if (isDisposed)
            {
                yield break;
            }

            // WWW is done
            if (www.isDone)
            {
                onDoneInvoker(this);
                yield break;
            }

            // Check again next frame
            yield return null;
        }
    }
}