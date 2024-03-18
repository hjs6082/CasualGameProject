using UnityEngine;

public class StayInScreen : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // 카메라의 월드 좌표에서의 화면 경계값을 계산합니다.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // 오브젝트의 너비 절반
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // 오브젝트의 높이 절반
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        // 화면 경계를 넘어가지 않도록 오브젝트의 위치를 제한합니다.
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
