using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ResultPanelItem : MonoBehaviour
{
    public Transform[] stars; // �ִϸ��̼� ��� ������ Transform �迭
    public float initialScale = 0f; // �ִϸ��̼� ���� �� ���� ũ��
    public float maxScale = 2f; // ���� Ȯ��� �ִ� ũ��
    public float finalScale = 1f; // ���� ���� ũ��
    public float duration = 2f; // �� �� �ִϸ��̼��� ���� �ð�
    public float delayBetweenStars = 0.5f; // ���� ������ �ִϸ��̼� ���� �ð�
    public AudioClip impactSound; // ���� �� ����� ȿ����
    private AudioSource audioSource; // ����� �ҽ� ������Ʈ

    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private Button x2ClaimButton;
    [SerializeField]
    private Button claimButton;
    [SerializeField]
    private Button retryButton;

    void Start()
    {
        //Animation ���
        audioSource = GetComponent<AudioSource>(); // ����� �ҽ� ������Ʈ ��������
        AnimateStars();
    }

    void AnimateStars()
    {
        Sequence sequence = DOTween.Sequence();

        foreach (Transform star in stars)
        {
            star.localScale = Vector3.one * initialScale;

            sequence.Append(star.DOScale(maxScale, duration * 0.5f).SetEase(Ease.OutBack));
            sequence.Append(star.DOScale(finalScale, duration * 0.5f).SetEase(Ease.InBack).OnComplete(() => PlayImpactSound()));

            sequence.AppendInterval(delayBetweenStars);
        }
    }

    //ȿ���� ���

    void PlayImpactSound()
    {
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound); // ȿ���� ���
        }
    }

    public void SetItem(StageData stageData)
    {
        levelText.text = "Level " + stageData.stageIndex;
        coinText.text = "x " + stageData.coinIndex;
        retryButton.onClick.AddListener(() => { GameManager.Instance.SetStage(stageData.stageIndex - 1); Destroy(this.gameObject); UIManager.Instance.isOnUI = false; });
        claimButton.onClick.AddListener(() => { GameManager.Instance.SetStage(stageData.stageIndex); Destroy(this.gameObject); UIManager.Instance.isOnUI = false; });
        foreach (var star in stars)
        {
            star.gameObject.GetComponent<Image>().color = Color.white;
        }
        switch (GameManager.Instance.getStar)
        {
            case 0:
                foreach (var star in stars)
                {
                    star.gameObject.GetComponent<Image>().color = Color.black;
                }
                break;
            case 1:
                stars[stars.Length - 1].gameObject.GetComponent<Image>().color = Color.black;
                stars[stars.Length - 2].gameObject.GetComponent<Image>().color = Color.black;
                break;
            case 2:
                stars[stars.Length - 1].gameObject.GetComponent<Image>().color = Color.black;
                break;
        }
    }
}
