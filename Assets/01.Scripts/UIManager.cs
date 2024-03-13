using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private Transform uiCanvasTransform;
    [SerializeField]
    private TextMeshProUGUI level_Text;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private LinesDrawer linesDrawer;

    [SerializeField]
    private GameObject resultPanelPrefab;



    public void Awake()
    {
        Instance = this;
    }

    public void OnGameClearPanel(StageData stageData)
    {
        GameObject resultPanel = Instantiate(resultPanelPrefab, uiCanvasTransform);
        resultPanel.GetComponent<ResultPanelItem>().SetItem(stageData);
    }

    public void OnStage()
    {
        level_Text.text = "Level " + GameManager.Instance.nowStage.stageIndex;
        timerText.text = GameManager.Instance.nowStage.clearTime.ToString();
        linesDrawer.ClearLine();
    }
}
