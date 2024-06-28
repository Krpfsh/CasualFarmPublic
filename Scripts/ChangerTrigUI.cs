using UnityEngine;
using DG.Tweening;
public class ChangerTrigUI : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private GameObject changerUI;
    [SerializeField] private string audioId;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changerUI.transform.DOScale(0.5f, 0.5f).SetEase(Ease.InSine);
            AudioManager.instance.Play(audioId);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            changerUI.transform.DOScale(0f, 0.5f).SetEase(Ease.InSine);
        }
    }
}
