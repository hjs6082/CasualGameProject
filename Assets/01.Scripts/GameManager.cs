using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    public bool isLive; //게임이 시작했는지
    public Stage stage;
    public StageData nowStage;

    void Start()
    {
        Instance = this;
        stage = Resources.Load<Stage>("SO/Stage");
        nowStage = stage.stageDatas[0];
        UIManager.Instance.OnStage();
        foreach (Transform child in onStageTransform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(nowStage.stagePrefab, onStageTransform);
        player = FindObjectOfType<Character>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            NextStage();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            PrevStage();
        }
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
        //UIManager.Instance.OnGameOverPanel();
    }

    public void GameClear()
    {
        Time.timeScale = 0;
    }

    public void NextStage()
    {
        UIManager.Instance.OnStage();
        nowStage = stage.stageDatas[nowStage.stageIndex];
        foreach (Transform child in onStageTransform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(nowStage.stagePrefab, onStageTransform);
        player = FindObjectOfType<Character>().gameObject;
    }

    public void PrevStage()
    {
        nowStage = stage.stageDatas[nowStage.stageIndex - 1];
        UIManager.Instance.OnStage();
        foreach (Transform child in onStageTransform)
        {
            Destroy(child.gameObject);
        }
        Instantiate(nowStage.stagePrefab, onStageTransform);
        player = FindObjectOfType<Character>().gameObject;
    }
}
