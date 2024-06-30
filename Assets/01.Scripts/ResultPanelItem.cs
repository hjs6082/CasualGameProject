using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ResultPanelItem : MonoBehaviour
{
    public Transform[] stars; // 애니메이션 대상 별들의 Transform 배열
    public float initialScale = 0f; // 애니메이션 시작 전 별의 크기
    public float maxScale = 2f; // 별이 확대될 최대 크기
    public float finalScale = 1f; // 별의 최종 크기
    public float duration = 2f; // 각 별 애니메이션의 지속 시간
    public float delayBetweenStars = 0.5f; // 별들 사이의 애니메이션 지연 시간
    public AudioClip impactSound; // 박힐 때 재생할 효과음
    private AudioSource audioSource; // 오디오 소스 컴포넌트

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
        //Animation 재생
        audioSource = GetComponent<AudioSource>(); // 오디오 소스 컴포넌트 가져오기
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

    //효과음 재생

    void PlayImpactSound()
    {
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound); // 효과음 재생
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
