using UnityEngine;
using UnityEngine.UI;

public class LinesDrawer : MonoBehaviour
{
	public GameObject linePrefab;
	public LayerMask cantDrawOverLayer;
	int cantDrawOverLayerIndex;

	[Space(30f)]
	public Gradient lineColor;
	public float linePointsMinDistance;
	public float lineWidth;

	Line currentLine;

	Camera cam;

    [SerializeField]
    private Image[] stars; // 별 UI 이미지 배열

    // 시간 관리를 위한 변수들 추가
    public float drawTimeLimit = 2f; // 선을 그릴 수 있는 총 시간
	private float timeRemaining; // 남은 시간
	public Slider timeSlider; // 시간을 보여줄 슬라이더

	private bool canDraw = true; // 선을 그릴 수 있는지 여부를 결정하는 플래그


	void Start()
	{
		cam = Camera.main;
		cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
        drawTimeLimit = GameManager.Instance.nowStage.clearTime;
        timeRemaining = drawTimeLimit; // 남은 시간 초기화
		timeSlider.maxValue = drawTimeLimit; // 슬라이더의 최대값 설정
		timeSlider.value = timeRemaining; // 슬라이더 초기값 설정
        foreach (var star in stars)
        {
            star.color = Color.white;
        }
	}

    void Update()
    {
        if (UIManager.Instance.isOnUI)
            return;

        if (Input.GetMouseButtonDown(0) && canDraw && currentLine == null && !GameManager.Instance.isLive)
        {
            BeginDraw();
        }
        else if (currentLine != null && !GameManager.Instance.isLive)
        {
            if (canDraw)
            {
                Draw();
                timeRemaining -= Time.deltaTime;
                timeSlider.value = timeRemaining;

                UpdateStarRating();

                if (timeRemaining <= 0)
                {
                    canDraw = false;
                    EndDraw();
                    GameManager.Instance.GameStart();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && !GameManager.Instance.isLive)
        {
            EndDraw();
            CheckStar();
            if (!UIManager.Instance.isOnUI)
            {
                GameManager.Instance.GameStart();
            }
        }

        if (!canDraw && Input.GetMouseButtonDown(0) && currentLine == null)
        {
            Debug.Log("이미 선이 생성되었습니다.");
        }
    }

    void BeginDraw()
    {
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
        {
            EndDraw();
        }
        else
        {
            currentLine.AddPoint(mousePosition);
        }
    }

    void EndDraw()
    {
        if (currentLine != null)
        {
            if (currentLine.pointsCount < 2)
            {
                Destroy(currentLine.gameObject);
            }
            else
            {
                currentLine.gameObject.layer = cantDrawOverLayerIndex;
                currentLine.UsePhysics(true);
                currentLine = null;
            }
        }
        canDraw = false;
    }

    public void ClearLine()
    {
        // 현재 화면에 있는 모든 Line 객체를 찾아서 파괴합니다.
        foreach (Transform child in transform)
        {
            // Line 컴포넌트가 있는 자식 객체를 찾아 파괴합니다.
            if (child.GetComponent<Line>() != null)
            {
                Destroy(child.gameObject);
            }
        }

        // 선을 그릴 수 있는 상태로 리셋합니다.
        currentLine = null;
        canDraw = true;
        timeRemaining = drawTimeLimit; // 남은 시간도 초기화
        timeSlider.value = timeRemaining; // 슬라이더의 값을 초기 상태로 설정
    }

    public void ResetStar()
    {
        foreach (var star in stars)
        {
            star.color = Color.white;
        }
    }

    // 별 등급을 업데이트하는 메서드
    private void UpdateStarRating()
    {
        float timeRatio = timeRemaining / drawTimeLimit;

        // 남은 시간 비율에 따라 별 색상 업데이트
        if (timeRatio <= 0)
        {
            foreach (var star in stars)
            {
                star.color = Color.black;
            }
        }
        else if (timeRatio <= 1f / 3f) // 1/3 이하, 마지막 두 별을 black으로
        {
            stars[stars.Length - 1].color = Color.black;
            stars[stars.Length - 2].color = Color.black;
        }
        else if (timeRatio <= 2f / 3f) // 2/3 이하, 마지막 별만 black으로
        {
            stars[stars.Length - 1].color = Color.black;
        }
    }

    private void CheckStar()
    {
        float timeRatio = timeRemaining / drawTimeLimit;

        if(timeRatio <= 0)
        {
            GameManager.Instance.getStar = 0;
        }
        else if (timeRatio <= 1f / 3f) // 1/3 이하, 마지막 두 별을 black으로
        {
            GameManager.Instance.getStar = 1;
        }
        else if (timeRatio <= 2f / 3f) // 2/3 이하, 마지막 별만 black으로
        {
            GameManager.Instance.getStar = 2;
        }
        else
        {
            GameManager.Instance.getStar = 3;
        }
    }
}
