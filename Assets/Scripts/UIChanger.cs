using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChanger : MonoBehaviour
{
    [SerializeField] private GameObject Box; //box1
    [SerializeField] private GameObject BoxMain; //box1.2
    [SerializeField] private GameObject BoxHeader; //box1.1
    [SerializeField] private GameObject BoxFooter; //box1.3
    [SerializeField] private GameObject GridCharacters; //Grid
    [SerializeField] private Canvas _canvas;

    public bool MakePortrait = true;
    public bool MakeLandscapeLeft = false;
    public bool MakeLandscapeRight = false;

    public bool testL = false;
    public bool testR = false;
    public bool testP = false;

    private RectTransform _canvasRT;
    private RectTransform _boxRT;
    private RectTransform _boxHeaderRT;
    private RectTransform _boxMainRT;
    private RectTransform _boxFooterRt;
    private RectTransform _gridRT;

    private LayoutElement _boxLE;
    private LayoutElement _boxHeaderLE;
    private LayoutElement _boxMainLE;
    private LayoutElement _boxFooterLE;

    private GridLayoutGroup _gridLG;

    private float _startGridRTHeight;
    private List<GameObject> _buttonsList4Rotate = new List<GameObject>();

    private void Start()
    {
        _canvasRT = _canvas.GetComponent<RectTransform>();
        _boxRT = Box.GetComponent<RectTransform>();
        _boxHeaderRT = BoxHeader.GetComponent<RectTransform>();
        _boxMainRT = BoxMain.GetComponent<RectTransform>();
        _boxFooterRt = BoxFooter.GetComponent<RectTransform>();
        _gridRT = GridCharacters.GetComponent<RectTransform>();

        _boxLE = Box.GetComponent<LayoutElement>();
        _boxHeaderLE = BoxHeader.GetComponent<LayoutElement>();
        _boxMainLE = BoxMain.GetComponent<LayoutElement>();
        _boxFooterLE = BoxFooter.GetComponent<LayoutElement>();

        _gridLG = GridCharacters.GetComponent<GridLayoutGroup>();
        _startGridRTHeight = _gridRT.sizeDelta.y;

        foreach (Transform go in GridCharacters.transform)
        {
            _buttonsList4Rotate.Add(go.gameObject);
        }
        ToPortrait();
    }

    private void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight && !MakeLandscapeLeft)
        {
            ChangeOrientationMode(OrientationMode.LandscapeLeft);
            ToLandscapeLeft();
            print(1);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft && !MakeLandscapeRight)
        {
            ChangeOrientationMode(OrientationMode.LandscapeRight);
            ToLandscapeRight();
            print(1);
        }
        if (Input.deviceOrientation == DeviceOrientation.Portrait && !MakePortrait)
        {
            ChangeOrientationMode(OrientationMode.Portrait);
            ToPortrait();
            print(1);
        }

        #region ForTest
        //if (MakePortrait && !MakeLandscapeLeft)
        //{
        //    ToPortrait();
        //}
        //else if (MakeLandscapeLeft && !MakePortrait && !MakeLandscapeRight)
        //{
        //    ToLandscapeLeft();
        //}
        //else if (MakeLandscapeRight && !MakePortrait && !MakeLandscapeLeft)
        //{
        //    ToLandscapeRight();
        //}

        //if (testL)
        //{
        //    ChangeOrientationMode(OrientationMode.LandscapeLeft);
        //    ToLandscapeLeft();
        //}
        //if (testR)
        //{
        //    ChangeOrientationMode(OrientationMode.LandscapeRight);
        //    ToLandscapeRight();
        //}
        //if (testP)
        //{
        //    ChangeOrientationMode(OrientationMode.Portrait);
        //    ToPortrait();
        //}
        #endregion
    }
    private void ChangeOrientationMode(OrientationMode orientationMode)
    {
        switch (orientationMode)
        {
            case OrientationMode.Portrait:
                MakeLandscapeLeft = false;
                MakeLandscapeRight = false;
                MakePortrait = true;
                break;
            case OrientationMode.LandscapeLeft:
                MakeLandscapeLeft = true;
                MakeLandscapeRight = false;
                MakePortrait = false;
                break;
            case OrientationMode.LandscapeRight:
                MakeLandscapeLeft = false;
                MakeLandscapeRight = true;
                MakePortrait = false;
                break;
            default:
                break;
        }
    }

    private void ToLandscapeLeft()
    {
        testL = false;
        //Облака
        _boxHeaderLE.ignoreLayout = true;
        _boxHeaderRT.pivot = new Vector2(1, 1);
        _boxHeaderRT.localPosition = new Vector2(0, 0);
        _boxHeaderRT.eulerAngles = new Vector3(0, 0, 90);

        //Подгон размеров
        _boxLE.ignoreLayout = true;
        _boxRT.sizeDelta = new Vector2(_boxHeaderRT.sizeDelta.x, _boxRT.sizeDelta.x);
        _gridRT.sizeDelta = new Vector2(_gridRT.sizeDelta.x, _canvasRT.sizeDelta.y - _boxRT.sizeDelta.x);

        //Трава
        _boxFooterLE.ignoreLayout = true;
        _boxFooterRt.pivot = new Vector2(1, 0);
        _boxFooterRt.anchorMin = new Vector2(1, 1);
        _boxFooterRt.anchorMax = new Vector2(1, 1);
        _boxFooterRt.anchoredPosition3D = new Vector3(_boxFooterRt.localPosition.x * 0, 0f, 0f);
        _boxFooterRt.eulerAngles = new Vector3(0, 0, 90);

        //Переключатель с буквой
        _boxMainLE.ignoreLayout = true;
        _boxMainRT.eulerAngles = new Vector3(0, 0, 90);
        _boxMainRT.pivot = new Vector2(1, 0.5f);
        _boxMainRT.localPosition = new Vector2(_boxMainRT.anchoredPosition.x, 0);

        _gridLG.startCorner = GridLayoutGroup.Corner.LowerLeft;
        _gridLG.startAxis = GridLayoutGroup.Axis.Vertical;

        foreach (var item in _buttonsList4Rotate)
        {
            item.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90);
        }
    }

    private void ToLandscapeRight()
    {
        testR = false;
        //Облака
        _boxHeaderLE.ignoreLayout = true;
        _boxHeaderRT.pivot = new Vector2(0, 1);
        _boxHeaderRT.anchorMin = new Vector2(1, 1);
        _boxHeaderRT.anchorMax = new Vector2(1, 1);
        _boxHeaderRT.anchoredPosition3D = new Vector3(_boxHeaderRT.localPosition.x * 0, 0f, 0f);
        _boxHeaderRT.eulerAngles = new Vector3(0, 0, -90);

        //Подгон размеров
        _boxLE.ignoreLayout = true;
        _boxRT.sizeDelta = new Vector2(_boxHeaderRT.sizeDelta.x, _boxRT.sizeDelta.x);
        _gridRT.sizeDelta = new Vector2(_gridRT.sizeDelta.x, _canvasRT.sizeDelta.y - _boxRT.sizeDelta.x);

        //Трава
        _boxFooterLE.ignoreLayout = true;
        _boxFooterRt.pivot = new Vector2(0, 0);
        _boxFooterRt.anchorMin = new Vector2(0, 1);
        _boxFooterRt.anchorMax = new Vector2(0, 1);
        _boxFooterRt.anchoredPosition3D = new Vector3(_boxFooterRt.localPosition.x * 0, 0f, 0f);
        _boxFooterRt.eulerAngles = new Vector3(0, 0, -90);

        //Переключатель с буквой
        _boxMainLE.ignoreLayout = true;
        _boxMainRT.eulerAngles = new Vector3(0, 0, -90);
        _boxMainRT.pivot = new Vector2(0, 0.5f);
        _boxMainRT.localPosition = new Vector2(_boxMainRT.anchoredPosition.x, 0);

        _gridLG.startCorner = GridLayoutGroup.Corner.UpperRight;
        _gridLG.startAxis = GridLayoutGroup.Axis.Vertical;

        foreach (var item in _buttonsList4Rotate)
        {
            item.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, -90);
        }
    }

    private void ToPortrait()
    {
        testP = false;

        _boxLE.ignoreLayout = false;
        _boxHeaderLE.ignoreLayout = false;
        _boxHeaderRT.pivot = new Vector2(0, 1);
        _boxHeaderRT.position = new Vector2(0, 0);
        _boxHeaderRT.eulerAngles = new Vector3(0, 0, 0);

        _boxFooterLE.ignoreLayout = false;
        _boxFooterRt.pivot = new Vector2(0, 0);
        _boxFooterRt.anchorMin = new Vector2(0, 0);
        _boxFooterRt.anchorMax = new Vector2(0, 0);
        _boxFooterRt.position = new Vector2(0, 0);
        _boxFooterRt.eulerAngles = new Vector3(0, 0, 0);

        _boxMainLE.ignoreLayout = false;
        _boxMainRT.eulerAngles = new Vector3(0, 0, 0);
        _boxMainRT.pivot = new Vector2(0.5f, 0.5f);

        _gridLG.startCorner = GridLayoutGroup.Corner.UpperLeft;
        _gridLG.startAxis = GridLayoutGroup.Axis.Horizontal;
        _gridRT.sizeDelta = new Vector2(0, _startGridRTHeight);

        foreach (var item in _buttonsList4Rotate)
        {
            item.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
