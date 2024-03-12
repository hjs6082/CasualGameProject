using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    void Start()
    {
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

    void PlayImpactSound()
    {
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound); // ȿ���� ���
        }
    }
}
