using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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
}
