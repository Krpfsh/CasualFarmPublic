using UnityEngine;
using DG.Tweening;

public class Crop : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _cropRenderer;
    [SerializeField] private ParticleSystem _harvestedParticles;
    public void ScaleUp()
    {
        _cropRenderer.DOScale(1,1).SetEase(Ease.OutBack);
    }
    public void ScaleDown()
    {
        _cropRenderer.DOScale(0, 1).SetEase(Ease.OutBack).OnComplete(()=> Destroy(gameObject));

        _harvestedParticles.gameObject.SetActive(true);
        _harvestedParticles.transform.parent = null;
        _harvestedParticles.Play();
    }
}
