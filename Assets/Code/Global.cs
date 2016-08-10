using UnityEngine;
using System.Collections;
using DG.Tweening;

public sealed class Global : MonoBehaviour {

    public static Global _global;

    private Canvas _canvas;

    public ManagerData _managerData;
    public ManagerScreens _managerScreens;

    private float _screenCurrWidth;
    private float _screenCurrHeight;

    private void Awake()
    {
        _global = this;
        _managerData = new ManagerData();
        _managerScreens = new ManagerScreens();
    }

	// Use this for initialization
	private void Start () {

        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        initConstants();

        _managerData.load();
        _managerScreens.load(_canvas);
    }

	// Update is called once per frame
	private void Update () {
	
	}

    private void initConstants()
    {
        _screenCurrWidth = Screen.width;
        _screenCurrHeight = Screen.height;
    }

    public Tweener onDelayedCall(float duration, TweenCallback onComplete)
    {
        //return ManagerTowers._dummyGoCanvasGroup.transform.DOMove(Vector3.one, duration).OnComplete(callback);

        float start = 1;
        float stop = 0;
        float startVal = 1;

        return DOTween.To(() => start, x => startVal = x, stop, duration).OnComplete(onComplete);
    }

    public float ScreenWidth {

        get
        {
            return _screenCurrWidth;
        }
        set
        {

        }
    }

    public float ScreenHeight
    {
        get
        {
            return _screenCurrHeight;
        }
        set
        {

        }
    }

    public static Color HexToColor(string hex, byte alpha = 255)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        return new Color32(r, g, b, alpha);
    }
}
