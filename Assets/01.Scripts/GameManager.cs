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
        timer.isStart = false;
        //Time.timeScale = 0;
        PlayClearCheckAnimation(clearCheckImages[0], SetGameOverStage);
    }

    // TODO: O ǥ�� �� �Ҹ� �߰�
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
        // DOTween �ִϸ��̼� Sequence ����
        Sequence sequence = DOTween.Sequence();

        // ���� �������� 0���� ���� ū ������ ����
        sequence.Append(targetImage.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f));

        // �� ���� ���� �������� 1�� ����
        sequence.Append(targetImage.transform.DOScale(Vector3.one, 0.25f));
        sequence.OnComplete(onCompleteAction); // �ִϸ��̼��� ���� �� ������ �׼�
    }
}
