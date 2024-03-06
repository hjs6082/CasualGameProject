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

    void Update()
    {
        DrawLine();
        RemoveLines();
    }

    private void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
        }
        else if (Input.GetMouseButton(0) && currentLine != null)
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
        startPosition.z = 0; // Z ��ġ�� 0���� �����Ͽ� 2D ��鿡 ���� �׸��ϴ�.
        lineRenderer.SetPosition(0, startPosition); // ù ��° ��ġ ����
    }

    private void UpdateLine(Vector3 newPosition)
    {
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
        // ��� �� ����
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
            lines.Clear();
        }

        // �������� �׸� �� ����
        if (Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
        {
            GameObject lastLine = lines[lines.Count - 1];
            Destroy(lastLine);
            lines.RemoveAt(lines.Count - 1);
        }
    }
}
