using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab; // LineRenderer 컴포넌트가 포함된 프리팹

    private List<GameObject> lines = new List<GameObject>(); // 생성된 모든 선을 관리하는 리스트
    private GameObject currentLine; // 현재 그리고 있는 선

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
        lines.Add(currentLine); // 생성된 선을 리스트에 추가

        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;

        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0; // Z 위치를 0으로 설정하여 2D 평면에 선을 그립니다.
        lineRenderer.SetPosition(0, startPosition); // 첫 번째 위치 설정
    }

    private void UpdateLine(Vector3 newPosition)
    {
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = currentLine.GetComponent<EdgeCollider2D>();

        newPosition.z = 0; // Z 위치를 2D 평면에 맞게 조정
        if (lineRenderer.positionCount == 0 || Vector3.Distance(newPosition, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > minDistance)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);

            // EdgeCollider2D를 업데이트하기 위해 LineRenderer의 모든 점을 Vector2 배열로 변환
            Vector2[] edgePoints = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(i);
                edgePoints[i] = new Vector2(pos.x, pos.y);
            }
            edgeCollider.SetPoints(new List<Vector2>(edgePoints)); // EdgeCollider2D에 점들 설정
        }
    }


    private void RemoveLines()
    {
        // 모든 선 삭제
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
            lines.Clear();
        }

        // 마지막에 그린 선 삭제
        if (Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
        {
            GameObject lastLine = lines[lines.Count - 1];
            Destroy(lastLine);
            lines.RemoveAt(lines.Count - 1);
        }
    }
}
