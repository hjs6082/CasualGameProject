using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트 사용

public class DrawManager : MonoBehaviour
{
    [SerializeField]
    private GameObject linePrefab; // LineRenderer 컴포넌트가 포함된 프리팹
    private GameObject currentLine; // 현재 그리고 있는 선

    [SerializeField]
    private float minDistance = 0.1f;
    private float drawTime = 1.5f; // 선을 그리는 데 주어진 시간 (초)
    private float timeRemaining; // 남은 시간
    private bool isDrawing = false; // 현재 그리고 있는지 여부

    [SerializeField]
    private Slider timeSlider; // 슬라이더 컴포넌트 참조

    [SerializeField]
    private GameObject characterObject; // 캐릭터 오브젝트

    //슬라이더 값 설정
    void Start()
    {
        timeRemaining = drawTime;
        timeSlider.maxValue = drawTime; // 슬라이더의 최대값 설정
        timeSlider.value = timeRemaining; // 초기 슬라이더 값 설정
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
            GameStart(); // 사용자가 손을 떼었을 때 GameStart 호출
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
        //edgeCollider.points = new Vector2[] { startPosition }; // 초기 EdgeCollider 설정

        //currentLine.GetComponent<EdgeCollider2D>().edgeRadius = 0.1f; // 필요에 따라 콜라이더의 반지름 조절
    }

    private void UpdateLine(Vector3 newPosition)
    {
        LineRenderer lineRenderer = currentLine.GetComponent<LineRenderer>();
        EdgeCollider2D edgeCollider = currentLine.GetComponent<EdgeCollider2D>();


        newPosition.z = 0; // Z 축 값을 0으로 설정
        if (lineRenderer.positionCount > 0 && Vector3.Distance(newPosition, lineRenderer.GetPosition(lineRenderer.positionCount - 1)) > minDistance)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);

            // LineRenderer의 모든 점을 EdgeCollider에 복사
            Vector2[] points = new Vector2[lineRenderer.positionCount];
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                Vector3 pos = lineRenderer.GetPosition(i);
                points[i] = new Vector2(pos.x, pos.y);
            }
            edgeCollider.points = points; // EdgeCollider2D 업데이트
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
                GameStart(); // 시간이 초과되었을 때 GameStart 호출
            }
        }
        timeSlider.value = timeRemaining; // 슬라이더 값 업데이트
    }

    private void GameStart()
    {
        // 여기에 GameStart 관련 로직을 구현합니다
        currentLine.GetComponent<Rigidbody2D>().gravityScale = 1;
        currentLine.GetComponent<EdgeCollider2D>().isTrigger = false;
        characterObject.AddComponent<Rigidbody2D>();
        GameManager.Instance.GameStart();
        Debug.Log("Game Start!");
    }
}
