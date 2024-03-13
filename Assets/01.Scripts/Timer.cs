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
    private Image slider; // Slider가 아닌 Image 컴포넌트로 사용

    private float time;
    private float maxTime; // 최대 시간을 저장할 변수
    public bool isStart;

    public void StartTimer(float time)
    {
        this.time = time;
        this.maxTime = time; // 타이머 시작 시 최대 시간 설정
        timeText.text = time.ToString();
        slider.fillAmount = 1f; // 타이머 시작 시 slider를 전체로 채움
        isStart = true;
    }

    private void Update()
    {
        if (!isStart) return;

        if (time > 0f)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.CeilToInt(time).ToString();
            slider.fillAmount = time / maxTime; // 현재 시간을 최대 시간으로 나눈 비율로 fillAmount 업데이트
        }
        else
        {
            GameManager.Instance.GameClear();
            isStart = false;
        }
    }
}
