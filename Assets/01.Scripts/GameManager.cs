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
    private int beeSpawnIndex; //���߿� Scriptable ������Ʈ�� ���������� ������ �����ͼ� ��ȯ���� �ٲܰ�

    public int getStar;

    public bool isLive; //������ �����ߴ���
    public Stage stage;
    public StageData nowStage;

    void Start()
    {
        Instance = this;
        stage = Resources.Load<Stage>("SO/Stage");
        SetStage(0); //TODO: ����� ���������� 
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

    // TODO: O ǥ�� �� �Ҹ� �߰�
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

    //���� X������ ǥ�÷� ����
    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        SetStage(nowStage.stageIndex--);
    }
}
