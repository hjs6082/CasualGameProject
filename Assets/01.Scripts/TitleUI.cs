using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject titlePanel;
    [SerializeField]
    private GameObject chapterSelectPanel;
    [SerializeField]
    private GameObject stageSelectPanel; // �������� ���� �г� �߰�
    [SerializeField]
    private GameObject chapterObject;
    [SerializeField]
    private Transform chapterTransform;
    [SerializeField]
    private Transform stageTransform; // �������� Ʈ������ �߰�

    [SerializeField]
    private Image allChapterStarImage;
    [SerializeField]
    private TMP_Text allChapterStarText;
    [SerializeField]
    private Image currentChapterStarImage; // ���� ���õ� é���� �� ������ ǥ���� �̹��� �߰�
    [SerializeField]
    private TMP_Text currentChapterStarText; // ���� ���õ� é���� �� ������ ǥ���� �ؽ�Ʈ �߰�

    private List<GameObject> currentStageObjects = new List<GameObject>(); // ���� ǥ�õ� �������� ������Ʈ���� ����
    private List<GameObject> currentChapterObjects = new List<GameObject>(); // ���� ǥ�õ� é�� ������Ʈ���� ����

    public void OnClickChapterButton()
    {
        titlePanel.SetActive(false);
        chapterSelectPanel.SetActive(true);
        stageSelectPanel.SetActive(false); // �������� �г� ��Ȱ��ȭ

        // ���� é�� ������Ʈ ����
        foreach (var chapterObject in currentChapterObjects)
        {
            Destroy(chapterObject);
        }
        currentChapterObjects.Clear();

        int allChapterGetStar = 0;
        int allChapterStar = 0;
        bool allChaptersComplete = true; // ��� é���� ���� ��� ȹ���ߴ��� ����

        Stage stage = GameManager.Instance.stage;
        for (int i = 0; i < stage.chapterDatas.Count; i++)
        {
            GameObject chapterPrefab = Instantiate(chapterObject, chapterTransform);
            currentChapterObjects.Add(chapterPrefab);
            int stageStar = 0;
            int stageAllStar = 0;
            foreach (var stageData in stage.chapterDatas[i].stageDatas)
            {
                stageStar += stageData.getStar;
                stageAllStar += 3;
                allChapterGetStar += stageData.getStar;
                allChapterStar += 3;
            }

            ChapterData chapterData = stage.chapterDatas[i];
            chapterPrefab.GetComponent<ChapterUIItem>().SetItem(
                chapterData.stageDatas[0].stageSprite,
                chapterData.chapterIndex.ToString(),
                (stageStar + "/" + stageAllStar.ToString()),
                stageStar == stageAllStar,
                chapterData,
                true // é�� ǥ�� ���θ� ����
            );
            chapterPrefab.GetComponent<Button>().onClick.AddListener(() => OnChapterSelected(chapterData)); // é�� Ŭ�� �̺�Ʈ �߰�

            if (stageStar != stageAllStar)
            {
                allChaptersComplete = false; // ��� é���� ���� ��� ȹ������ �������� ǥ��
            }
        }

        allChapterStarText.text = allChapterGetStar.ToString() + "/" + allChapterStar.ToString();
        Color color = allChaptersComplete ? Color.yellow : Color.white;
        allChapterStarImage.color = color;
    }

    private void OnChapterSelected(ChapterData chapterData)
    {
        chapterSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(true);

        // ���� �������� ������Ʈ ����
        foreach (var stageObject in currentStageObjects)
        {
            Destroy(stageObject);
        }
        currentStageObjects.Clear();

        int chapterStageStar = 0;
        int chapterStageAllStar = 0;

        // ���ο� �������� ������Ʈ ����
        foreach (var stageData in chapterData.stageDatas)
        {
            GameObject stagePrefab = Instantiate(chapterObject, stageTransform);
            currentStageObjects.Add(stagePrefab);
            string stageNumber = stageData.stageIndex.ToString();
            stagePrefab.GetComponent<ChapterUIItem>().SetStageItem(
                stageData.stageSprite,
                stageNumber,
                stageData.getStar + "/3", // �������������� �� ������ ǥ��
                stageData.isClear,
                stageData.getStar
            );
            stagePrefab.GetComponent<Button>().onClick.AddListener(() => OnStageSelected(stageData)); // �������� Ŭ�� �̺�Ʈ �߰�
            chapterStageStar += stageData.getStar;
            chapterStageAllStar += 3;
        }

        // ���� ���õ� é���� �� ���� ������Ʈ
        currentChapterStarText.text = chapterStageStar + "/" + chapterStageAllStar;
        Color starColor = chapterStageStar == chapterStageAllStar ? Color.yellow : Color.white;
        currentChapterStarImage.color = starColor;
    }

    private void OnStageSelected(StageData stageData)
    {
        GameManager.Instance.SetStage(stageData); // ���õ� �������� ����
        SceneManager.LoadScene("ProtoScene"); // ProtoScene���� �� ��ȯ
    }

    public void OnClickBackButton()
    {
        titlePanel.SetActive(true);
        chapterSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(false); // �������� �г� ��Ȱ��ȭ

        // ���� é�� ������Ʈ ����
        foreach (var chapterObject in currentChapterObjects)
        {
            Destroy(chapterObject);
        }
        currentChapterObjects.Clear();
    } 

    public void OnClickStageBackButton()
    {
        chapterSelectPanel.SetActive(true);
        stageSelectPanel.SetActive(false);

        // ���� �������� ������Ʈ ����
        foreach (var stageObject in currentStageObjects)
        {
            Destroy(stageObject);
        }
        currentStageObjects.Clear();
    }
}
