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
    }
}