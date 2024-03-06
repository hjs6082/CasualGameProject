using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab; // LineRenderer ������Ʈ�� ���Ե� ������
    private List<GameObject> lines = new List<GameObject>(); // ������ ��� ���� �����ϴ� ����Ʈ
    private GameObject currentLine; // ���� �׸��� �ִ� ��

    [SerializeField]
    private float minDistance = 0.1f;
    private float drawTime = 2.0f; // ���� �׸��� �� �־��� �ð� (��)
    private float timeRemaining; // ���� �ð�
    private bool isDrawing = false; // ���� �׸��� �ִ��� ����

    [SerializeField]
    private GameObject testChracterObject;

    void Start()
    {
        timeRemaining = drawTime;
    }

    void Update()
    {
        DrawLine();
        RemoveLines();
        UpdateTime();
    }

    private void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (timeRemaining > 0)
            {
                CreateNewLine();
                isDrawing = true; // �׸��� ����
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false; // �׸��� �ߴ�, �ð� �Ͻ� ����
            Debug.Log(timeRemaining);
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
        lines.Add(currentLine); // ������ ���� ����Ʈ�� �߰�
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0; // Z ��ġ�� 0���� ����
        lineRenderer.SetPosition(0, startPosition); // ù ��° ��ġ ����
    }

    private void UpdateLine(Vector3 newPosition)
    {
        if (!isDrawing || timeRemaining <= 0) return;

        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        newPosition.z = 0; // Z ��ġ�� 2D ��鿡 �°� ����
        if (lineRenderer.positionCount == 0 || Vector3.Distance(newPosition, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > minDistance)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);

            // EdgeCollider2D�� ������Ʈ�ϱ� ���� LineRenderer�� ��� ���� Vector2 �迭�� ��ȯ
            Vector2[] edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(i);
                edgePoints[i] = new Vector2(pos.x, pos.y);
            }
            edgeCollider.SetPoints(new List<Vector2>(edgePoints)); // EdgeCollider2D�� ���� ����
        }
    }

    private void RemoveLines()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
            lines.Clear();
            timeRemaining = drawTime; // �ð� ����
        }

        if (Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
        {
            GameObject lastLine = lines[lines.Count - 1];
            Destroy(lastLine);
            lines.RemoveAt(lines.Count - 1);
            timeRemaining = drawTime; // �ð� ����
        }
    }

    private void UpdateTime()
    {
        if (isDrawing && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                FinishDrawing(); // �ð��� �� �Ǿ��� �� �׸��� �ߴ�
            }
        }
    }

    private void FinishDrawing()
    {
        isDrawing = false; // �׸��� �ߴ�
        timeRemaining = 0; // �ð��� 0���� ����
        // ������ ���� �������ϴ� �߰� ������ �ʿ��� �� �ֽ��ϴ�.

        foreach (var line in lines)
        {
            line.AddComponent<Rigidbody2D>();
        }

        testChracterObject.AddComponent<Rigidbody2D>();
    }
}
