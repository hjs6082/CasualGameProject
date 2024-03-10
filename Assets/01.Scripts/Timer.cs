using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text timeText;
    [SerializeField]
    private Image slider;

    private float time;
    public bool isStart;

    public void StartTimer(float time)
    {
        this.time = time;
        timeText.text = time.ToString();
        isStart = true;
    }

    private void Update()
    {
        if (!isStart) return;

        if(time > 0f)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.CeilToInt(time).ToString();
            slider.fillAmount = time;
        }
        else
        {
            GameManager.Instance.GameOver();
            isStart = false;
        }
    }
}
