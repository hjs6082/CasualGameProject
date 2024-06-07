using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterUIItem : MonoBehaviour
{
    [SerializeField]
    private Image mapImage;
    [SerializeField]
    private TMP_Text chapterText;
    [SerializeField]
    private TMP_Text starText;
    [SerializeField]
    private Image starImage;
    [SerializeField]
    public List<StageData> chapterStageData;

    public void SetItem(Sprite mapSprite, string chapterOrStage, string star, bool isAllStar, ChapterData chapterData, bool isChapter)
    {
        mapImage.sprite = mapSprite;
        chapterText.text = isChapter ? "Chapter " + chapterOrStage : "Stage " + chapterOrStage;
        starText.text = star;

        Color color = isAllStar ? Color.yellow : Color.white;
        starImage.color = color;

        chapterStageData = chapterData.stageDatas;

        if (isChapter)
        {
            bool isClearedWithAllStars = true;
            foreach (var stageData in chapterData.stageDatas)
            {
                if (!stageData.isClear || stageData.getStar < 3)
                {
                    isClearedWithAllStars = false;
                    break;
                }
            }

            mapImage.color = isClearedWithAllStars ? new Color(0.5f, 0.5f, 0.5f, 1) : Color.white;
        }
    }

    public void SetStageItem(Sprite mapSprite, string stageNumber, string star, bool isClear, int getStar)
    {
        mapImage.sprite = mapSprite;
        chapterText.text = "Stage " + stageNumber;
        starText.text = star;

        Color color = getStar == 3 ? Color.yellow : Color.white;
        starImage.color = color;

        if (isClear && getStar == 3)
        {
            mapImage.color = new Color(0.5f, 0.5f, 0.5f, 1); // 이미지 색상을 어둡게
        }
        else
        {
            mapImage.color = Color.white; // 이미지 색상을 원래대로
        }
    }
}
