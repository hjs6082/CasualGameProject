using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField]
    private TextMeshProUGUI level_Text;


    [SerializeField]
    private GameObject resultPanelPrefab;

    public void Awake()
    {
        Instance = this;
    }

    public void OnGameOverPanel()
    {
        //resultPanelPrefab.GetComponent<ResultPanelItem>().SetItem();
    }

    public void OnStage()
    {
        level_Text.text = "Level " + GameManager.Instance.nowStage.stageIndex;
    }
}
