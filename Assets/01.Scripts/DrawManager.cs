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
    private float drawTime = 2.0f; // 선을 그리는 데 주어진 시간 (초)
    private float timeRemaining; // 남은 시간
    private bool isDrawing = false; // 현재 그리고 있는지 여부

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
                isDrawing = true; // 그리기 시작
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false; // 그리기 중단, 시간 일시 중지
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
        lines.Add(currentLine); // 생성된 선을 리스트에 추가
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        Vector3 startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPosition.z = 0; // Z 위치를 0으로 설정
        lineRenderer.SetPosition(0, startPosition); // 첫 번째 위치 설정
    }

    private void UpdateLine(Vector3 newPosition)
    {
        if (!isDrawing || timeRemaining <= 0) return;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            foreach (GameObject line in lines)
            {
                Destroy(line);
            }
            lines.Clear();
            timeRemaining = drawTime; // 시간 리셋
        }

        if (Input.GetKeyDown(KeyCode.Z) && lines.Count > 0)
        {
            GameObject lastLine = lines[lines.Count - 1];
            Destroy(lastLine);
            lines.RemoveAt(lines.Count - 1);
            timeRemaining = drawTime; // 시간 리셋
        }
    }

    private void UpdateTime()
    {
        if (isDrawing && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                FinishDrawing(); // 시간이 다 되었을 때 그리기 중단
            }
        }
    }

    private void FinishDrawing()
    {
        isDrawing = false; // 그리기 중단
        timeRemaining = 0; // 시간을 0으로 설정
        // 마지막 선을 마무리하는 추가 로직이 필요할 수 있습니다.

        foreach (var line in lines)
        {
            line.AddComponent<Rigidbody2D>();
        }

        testChracterObject.AddComponent<Rigidbody2D>();
    }
}
