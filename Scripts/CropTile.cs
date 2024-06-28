using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System;

public class CropTile : MonoBehaviour
{
    private TileFieldState _state;

    [Header(" Elements ")]
    [SerializeField] private Transform _cropParent;
    [SerializeField] private MeshRenderer _tileRenderer;
    private Crop _crop;
    private CropData _cropData;


    [Header("Events ")]
    public static Action<ProductType> OnCropHarvested;
    private void Start()
    {
        _state = TileFieldState.Empty;
    }
    public void Sow(CropData cropData)
    {
        AudioManager.instance.Play("ThrowSeads");
        _state = TileFieldState.Sown;
        _crop = Instantiate(cropData.CropPrefab, transform.position, Quaternion.identity, _cropParent);
        _cropData = cropData;
    }
    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public bool IsSown()
    {
        return _state == TileFieldState.Sown;
    }
    public void Water()
    {
        AudioManager.instance.Play("Watering");
        _state = TileFieldState.Watered;
        _crop.ScaleUp();
        _tileRenderer.material.DOColor(Color.white * .3f, 1).SetEase(Ease.OutBack);
    }
    public void Harvest()
    {
        AudioManager.instance.Play("Harvesting");
        _state = TileFieldState.Empty;
        _crop.ScaleDown();
        _tileRenderer.material.DOColor(Color.white, 1);

        OnCropHarvested?.Invoke(_cropData.cropType);
    }
}
