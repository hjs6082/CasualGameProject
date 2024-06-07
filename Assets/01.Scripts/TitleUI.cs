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
    private GameObject stageSelectPanel; // 스테이지 선택 패널 추가
    [SerializeField]
    private GameObject chapterObject;
    [SerializeField]
    private Transform chapterTransform;
    [SerializeField]
    private Transform stageTransform; // 스테이지 트랜스폼 추가

    [SerializeField]
    private Image allChapterStarImage;
    [SerializeField]
    private TMP_Text allChapterStarText;
    [SerializeField]
    private Image currentChapterStarImage; // 현재 선택된 챕터의 별 정보를 표시할 이미지 추가
    [SerializeField]
    private TMP_Text currentChapterStarText; // 현재 선택된 챕터의 별 정보를 표시할 텍스트 추가

    private List<GameObject> currentStageObjects = new List<GameObject>(); // 현재 표시된 스테이지 오브젝트들을 저장
    private List<GameObject> currentChapterObjects = new List<GameObject>(); // 현재 표시된 챕터 오브젝트들을 저장

    public void OnClickChapterButton()
    {
        titlePanel.SetActive(false);
        chapterSelectPanel.SetActive(true);
        stageSelectPanel.SetActive(false); // 스테이지 패널 비활성화

        // 기존 챕터 오브젝트 제거
        foreach (var chapterObject in currentChapterObjects)
        {
            Destroy(chapterObject);
        }
        currentChapterObjects.Clear();

        int allChapterGetStar = 0;
        int allChapterStar = 0;
        bool allChaptersComplete = true; // 모든 챕터의 별을 모두 획득했는지 여부

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
                true // 챕터 표시 여부를 전달
            );
            chapterPrefab.GetComponent<Button>().onClick.AddListener(() => OnChapterSelected(chapterData)); // 챕터 클릭 이벤트 추가

            if (stageStar != stageAllStar)
            {
                allChaptersComplete = false; // 모든 챕터의 별을 모두 획득하지 못했음을 표시
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

        // 기존 스테이지 오브젝트 제거
        foreach (var stageObject in currentStageObjects)
        {
            Destroy(stageObject);
        }
        currentStageObjects.Clear();

        int chapterStageStar = 0;
        int chapterStageAllStar = 0;

        // 새로운 스테이지 오브젝트 생성
        foreach (var stageData in chapterData.stageDatas)
        {
            GameObject stagePrefab = Instantiate(chapterObject, stageTransform);
            currentStageObjects.Add(stagePrefab);
            string stageNumber = stageData.stageIndex.ToString();
            stagePrefab.GetComponent<ChapterUIItem>().SetStageItem(
                stageData.stageSprite,
                stageNumber,
                stageData.getStar + "/3", // 스테이지에서도 별 정보를 표시
                stageData.isClear,
                stageData.getStar
            );
            stagePrefab.GetComponent<Button>().onClick.AddListener(() => OnStageSelected(stageData)); // 스테이지 클릭 이벤트 추가
            chapterStageStar += stageData.getStar;
            chapterStageAllStar += 3;
        }

        // 현재 선택된 챕터의 별 정보 업데이트
        currentChapterStarText.text = chapterStageStar + "/" + chapterStageAllStar;
        Color starColor = chapterStageStar == chapterStageAllStar ? Color.yellow : Color.white;
        currentChapterStarImage.color = starColor;
    }

    private void OnStageSelected(StageData stageData)
    {
        GameManager.Instance.SetStage(stageData); // 선택된 스테이지 설정
        SceneManager.LoadScene("ProtoScene"); // ProtoScene으로 씬 전환
    }

    public void OnClickBackButton()
    {
        titlePanel.SetActive(true);
        chapterSelectPanel.SetActive(false);
        stageSelectPanel.SetActive(false); // 스테이지 패널 비활성화

        // 기존 챕터 오브젝트 제거
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

        // 기존 스테이지 오브젝트 제거
        foreach (var stageObject in currentStageObjects)
        {
            Destroy(stageObject);
        }
        currentStageObjects.Clear();
    }
}
