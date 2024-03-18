using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private Image[] clearCheckImages;
    [SerializeField]
    private LinesDrawer linesDrawer;
    [SerializeField]
    private Transform spawnBeePosition;
    [SerializeField]
    private Transform onStageTransform;
    [SerializeField]
    private GameObject player;
    public GameObject Player { get { return player; } }
    [SerializeField]
    private Timer timer;
    
    [SerializeField]
    private int beeSpawnIndex; //나중에 Scriptable 오브젝트로 스테이지의 정보를 가져와서 소환수를 바꿀것

    public int getStar;

    public bool isLive; //게임이 시작했는지
    public Stage stage;
    public StageData nowStage;

    void Start()
    {
        Instance = this;
        stage = Resources.Load<Stage>("SO/Stage");
        SetStage(0); //TODO: 저장된 스테이지로 

        // 원하는 해상도로 설정합니다. 예: 너비 900, 높이 1600
        int width = 900;
        int height = 1600;

        // 풀스크린 모드를 비활성화하고, 위에서 지정한 해상도로 게임을 시작합니다.
        Screen.SetResolution(width, height, false);
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void GameStart()
    {
        isLive = true;
        Time.timeScale = 1;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        foreach (var item in FindObjectsOfType<HoneyComb>())
        {
            item.SpawnBee(nowStage.beeIndex,nowStage.beeAtk);
        }
        timer.StartTimer(nowStage.clearTime);
    }

    public void GameOver()
    {
        isLive = false;
        timer.isStart = false;
        //Time.timeScale = 0;
        PlayClearCheckAnimation(clearCheckImages[0], SetGameOverStage);
    }

    // TODO: O 표시 및 소리 추가
    public void GameClear()
    {
        //Time.timeScale = 0;
        isLive = false;
        PlayClearCheckAnimation(clearCheckImages[1], SetGameClearStage);
    }

    public void SetStage(int stageIndex)
    {
        getStar = 0;
        linesDrawer.ClearLine();
        nowStage = stage.stageDatas[stageIndex];
        UIManager.Instance.OnStage();
        timer.ResetTimer(nowStage.clearTime);
        linesDrawer.ResetStar();
        foreach (Transform child in onStageTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (var clearCheckImage in clearCheckImages)
        {
            clearCheckImage.transform.localScale = Vector3.zero;
        }
        Instantiate(nowStage.stagePrefab, onStageTransform);
        player = FindObjectOfType<Character>().gameObject;
        linesDrawer.gameObject.SetActive(true);
    }

    private void SetGameOverStage()
    { 
        SetStage(nowStage.stageIndex - 1);
    }

    private void SetGameClearStage()
    {
        UIManager.Instance.OnGameClearPanel(nowStage);
    }

    private void PlayClearCheckAnimation(Image targetImage, TweenCallback onCompleteAction)
    {
        targetImage.gameObject.GetComponent<AudioSource>().Play();
        // DOTween 애니메이션 Sequence 생성
        Sequence sequence = DOTween.Sequence();

        // 먼저 스케일을 0에서 조금 큰 값으로 변경
        sequence.Append(targetImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f));

        // 그 다음 원래 스케일인 1로 변경
        sequence.Append(targetImage.transform.DOScale(Vector3.one, 0.25f));
        sequence.OnComplete(onCompleteAction); // 애니메이션이 끝난 후 실행할 액션
    }
}
