using UnityEngine;

//Explane : UI를 레터박스를 만들어 핸드폰 해상도에 맞추기위한 스크립트
public class SafeArea : MonoBehaviour
{
    private RectTransform panelSafeArea;
    private Rect currentSafeArea = new Rect();
    private ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;

    void Awake()
    {
        panelSafeArea = GetComponent<RectTransform>();
        currentSafeArea = Screen.safeArea;
        ApplySafeArea();
    }

    void Update()
    {
        if (currentSafeArea != Screen.safeArea || currentOrientation != Screen.orientation)
        {
            currentSafeArea = Screen.safeArea;
            currentOrientation = Screen.orientation;
            ApplySafeArea();
        }
    }

    //SafeArea 생성
    private void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;

        var safeArea = Screen.safeArea;
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        var pixelRect = GetComponentInParent<Canvas>().pixelRect;
        anchorMin.x /= pixelRect.width;
        anchorMin.y /= pixelRect.height;
        anchorMax.x /= pixelRect.width;
        anchorMax.y /= pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
    }
}
