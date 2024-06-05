using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Stage", order = 1)]
public class Stage : ScriptableObject
{
    public List<ChapterData> chapterDatas;
}

[System.Serializable]
public class ChapterData
{
    public int chapterIndex;
    public List<StageData> stageDatas;
}

[System.Serializable]
public class StageData
{
    public int stageIndex;
    public int beeIndex;
    public int getStageCoinIndex;
    public int clearTime;
    public int coinIndex;
    public float beeAtk;
    public GameObject stagePrefab;
    public Sprite stageSprite;
    public int getStar;
    public bool isClear;
}
