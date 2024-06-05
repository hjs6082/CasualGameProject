using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject titlePanel;
    [SerializeField]
    private GameObject chapterSelectPanel;
    [SerializeField]
    private GameObject chapterObject;
    [SerializeField]
    private Transform chapterTransform;

    [SerializeField]
    private Image allChapterStarImage;
    [SerializeField]
    private TMP_Text allChapterStarText;


    public void OnClickChapterButton()
    {
        titlePanel.SetActive(false);
        chapterSelectPanel.SetActive(true);
        int stageStar = 0;
        int stageAllStar = 0;

        int allChapterGetStar = 0;
        int allChapterStar = 0;
        Stage stage = GameManager.Instance.stage;
        for(int i = 0; i < stage.chapterDatas.Count; i++)
        {
            GameObject chapterPrefab = Instantiate(chapterObject, chapterTransform);
            foreach (var stageData in stage.chapterDatas[i].stageDatas)
            {
                stageStar += stageData.getStar;
                stageAllStar += 3;
                allChapterGetStar += stageData.getStar;
                allChapterStar += 3;
            }

            chapterPrefab.GetComponent<ChapterUIItem>().SetItem(stage.chapterDatas[i].stageDatas[0].stageSprite
                ,stage.chapterDatas[i].chapterIndex.ToString(),
                (stageStar + "/" + stageAllStar.ToString()), stageStar == stageAllStar ? true : false, stage.chapterDatas[i]); ;
            stageAllStar = 0;
            stageStar = 0;
        }

        allChapterStarText.text = allChapterGetStar.ToString() + "/" + allChapterStar.ToString();
        Color color = stageAllStar == stageStar ? Color.white : Color.yellow;
        allChapterStarImage.color = color;
    }

    public void OnClickBackButton()
    {
        titlePanel.SetActive(true);
        chapterSelectPanel.SetActive(false);
    }
}
