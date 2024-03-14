using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    private GameObject resultPanelPrefab;

    public bool isOnUI;


    public void Awake()
    {
        Instance = this;
    }

    public void OnGameClearPanel(StageData stageData)
    {
        isOnUI = true;
        GameObject resultPanel = Instantiate(resultPanelPrefab, uiCanvasTransform);
        resultPanel.GetComponent<ResultPanelItem>().SetItem(stageData);
    }

    public void OnStage()
    {
        level_Text.text = "Level " + GameManager.Instance.nowStage.stageIndex;
        timerText.text = GameManager.Instance.nowStage.clearTime.ToString();
    }

    public void OnExit()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnEnter()
    {
        SceneManager.LoadScene("GameScene");
    }
}
