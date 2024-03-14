using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        Time.timeScale = 0;
        StartCoroutine(GameOverCoroutine());
    }

    // TODO: O 표시 및 소리 추가
    public void GameClear()
    {
        //Time.timeScale = 0;
        UIManager.Instance.OnGameClearPanel(nowStage);
        isLive = false;
    }

    public void SetStage(int stageIndex)
    {
        getStar = 0;
        linesDrawer.ClearLine();
        nowStage = stage.stageDatas[stageIndex];
        UIManager.Instance.OnStage();
        linesDrawer.ResetStar();
        foreach (Transform child in onStageTransform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(nowStage.stagePrefab, onStageTransform);
        player = FindObjectOfType<Character>().gameObject;
    }

    //추후 X음성과 표시로 변경
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        SetStage(nowStage.stageIndex--);
    }
}
