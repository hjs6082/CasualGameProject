using UnityEngine;

public class StayInScreen : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // ī�޶��� ���� ��ǥ������ ȭ�� ��谪�� ����մϴ�.
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; // ������Ʈ�� �ʺ� ����
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; // ������Ʈ�� ���� ����
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        // ȭ�� ��踦 �Ѿ�� �ʵ��� ������Ʈ�� ��ġ�� �����մϴ�.
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
