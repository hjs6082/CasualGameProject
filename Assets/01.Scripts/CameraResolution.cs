using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private int ScreenSizeX = 0;
    private int ScreenSizeY = 0;

    //카메라 재설정
    private void RescaleCamera()
    {
        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        float targetaspect = 9.0f / 16.0f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        float scaleheight = windowaspect / targetaspect;
        Camera camera = GetComponent<Camera>();

        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;
            camera.rect = rect;
        }
        else
        {
            float scalewidth = 1.0f / scaleheight;
            Rect rect = camera.rect;
            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }

    void Start()
    {
        RescaleCamera();
    }

    void Update()
    {
        RescaleCamera();
    }

    //안드로이드 실행시 화면을 맞춰줌
    void OnPreCull()
    {
        if (Application.isEditor) return;

        Camera.main.rect = new Rect(0, 0, 1, 1);
        GL.Clear(true, true, Color.black); // Clears the screen to black

        Camera.main.rect = GetComponent<Camera>().rect;
    }
}
