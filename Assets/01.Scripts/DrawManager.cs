using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ������Ʈ ���

public class DrawManager : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab; // LineRenderer ������Ʈ�� ���Ե� ������
    private GameObject currentLine; // ���� �׸��� �ִ� ��

    [SerializeField]
    private float minDistance = 0.1f;
    private float drawTime = 1.5f; // ���� �׸��� �� �־��� �ð� (��)
    private float timeRemaining; // ���� �ð�
    private bool isDrawing = false; // ���� �׸��� �ִ��� ����

    [SerializeField]
    private Slider timeSlider; // �����̴� ������Ʈ ����

    [SerializeField]
    private GameObject characterObject; // ĳ���� ������Ʈ

    //�����̴� �� ����
    void Start()
    {
        timeRemaining = drawTime;
        timeSlider.maxValue = drawTime; // �����̴��� �ִ밪 ����
        timeSlider.value = timeRemaining; // �ʱ� �����̴� �� ����
    }

    void Update()
    {
        DrawLine();
        UpdateTime();
    }

    private void DrawLine()
    {
        if (Input.GetMouseButtonDown(0) && timeRemaining > 0 && currentLine == null)
        {
            CreateNewLine();
            isDrawing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            GameStart(); // ����ڰ� ���� ������ �� GameStart ȣ��
        }
        else if (Input.GetMouseButton(0) && currentLine != null && isDrawing)
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;
            UpdateLine(currentPosition);
        }
    }

    private void CreateNewLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0;
        lineRenderer.SetPosition(0, startPosition);
        //edgeCollider.points = new Vector2[] { startPosition }; // �ʱ� EdgeCollider ����

        //currentLine.GetComponent<EdgeCollider2D>().edgeRadius = 0.1f; // �ʿ信 ���� �ݶ��̴��� ������ ����
    }

    private void UpdateLine(Vector3 newPosition)
    {
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = currentLine.GetComponent<EdgeCollider2D>();


        newPosition.z = 0; // Z �� ���� 0���� ����
        if (lineRenderer.positionCount > 0 && Vector3.Distance(newPosition, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > minDistance)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);

            // LineRenderer�� ��� ���� EdgeCollider�� ����
            Vector2[] points = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(i);
                points[i] = new Vector2(pos.x, pos.y);
            }
            edgeCollider.points = points; // EdgeCollider2D ������Ʈ
        }
    }

    private void UpdateTime()
    {
        if (isDrawing && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                isDrawing = false;
                GameStart(); // �ð��� �ʰ��Ǿ��� �� GameStart ȣ��
            }
        }
        timeSlider.value = timeRemaining; // �����̴� �� ������Ʈ
    }

    private void GameStart()
    {
        // ���⿡ GameStart ���� ������ �����մϴ�
        currentLine.GetComponent<Rigidbody2D>().gravityScale = 1;
        currentLine.GetComponent<EdgeCollider2D>().isTrigger = false;
        characterObject.AddComponent<Rigidbody2D>();
        GameManager.Instance.GameStart();
        Debug.Log("Game Start!");
    }
}
