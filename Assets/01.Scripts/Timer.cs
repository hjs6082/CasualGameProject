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
    private Image slider; // Slider�� �ƴ� Image ������Ʈ�� ���

    private float time;
    private float maxTime; // �ִ� �ð��� ������ ����
    public bool isStart;

    public void StartTimer(float time)
    {
        this.time = time;
        this.maxTime = time; // Ÿ�̸� ���� �� �ִ� �ð� ����
        timeText.text = time.ToString();
        slider.fillAmount = 1f; // Ÿ�̸� ���� �� slider�� ��ü�� ä��
        isStart = true;
    }

    private void Update()
    {
        if (!isStart) return;

        if (time > 0f)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.CeilToInt(time).ToString();
            slider.fillAmount = time / maxTime; // ���� �ð��� �ִ� �ð����� ���� ������ fillAmount ������Ʈ
        }
        else
        {
            GameManager.Instance.GameClear();
            isStart = false;
        }
    }
}
