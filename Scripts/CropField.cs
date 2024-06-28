using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _tilesParent;
    private List<CropTile> _cropTiles = new List<CropTile>();

    [Header(" Settings ")]
    [SerializeField] private CropData _cropData;
    private TileFieldState _state;
    private int _tilesSown;
    private int _tilesWatered;
    private int _tilesHarvested;

    [Header(" Actions ")]
    public static Action<CropField> onFullySown;
    public static Action<CropField> onFullyWatered;
    public static Action<CropField> onFullyHarvested;
    private void Start()
    {
        _state = TileFieldState.Empty;
        StoreTiles();
    }

    private void StoreTiles()
    {
        for (int i = 0; i < _tilesParent.childCount; i++)
            _cropTiles.Add(_tilesParent.GetChild(i).GetComponent<CropTile>());

    }

    public void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        for (int i = 0; i < seedPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(seedPositions[i]);
            if (closestCropTile == null)
                continue;
            if (!closestCropTile.IsEmpty())
                continue;
            Sow(closestCropTile);
        }
    }
    private void Sow(CropTile cropTile) { 
        cropTile.Sow(_cropData);
        _tilesSown++;
        if (_tilesSown == _cropTiles.Count)
        {
            FieldFullySown();
        }
    }

    private void FieldFullySown()
    {
        _state = TileFieldState.Sown;
        onFullySown?.Invoke(this);
    }

    private CropTile GetClosestCropTile(Vector3 seedPosition)
    {
        float minDistance = 5000;
        int closestCropTileIndex = -1;
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            CropTile cropTile = _cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }

        if (closestCropTileIndex == -1)
        {
            return null;
        }
        return _cropTiles[closestCropTileIndex];
    }
    public bool IsEmpty()
    {
        return _state == TileFieldState.Empty;
    }
    public void WaterCollidedCallback(Vector3[] waterPositions)
    {
        for (int i = 0; i < waterPositions.Length; i++)
        {
            CropTile closestCropTile = GetClosestCropTile(waterPositions[i]);
            if (closestCropTile == null)
                continue;
            if (!closestCropTile.IsSown())
                continue;
            Water(closestCropTile);
        }
    }
    [NaughtyAttributes.Button]
    private void InstantlySowTiles()
    {
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Sow(_cropTiles[i]);
        }
    }
    [NaughtyAttributes.Button]
    private void InstantlyWaterTiles()
    {
        
        for (int i = 0; i < _cropTiles.Count; i++)
        {
            Water(_cropTiles[i]);
        }
    }
    private void Water(CropTile cropTile)
    {
        cropTile.Water();
        _tilesWatered++;
        if(_tilesWatered == _cropTiles.Count)
        {
            FieldFullyWatered();
        }
    }

    private void FieldFullyWatered()
    {
        _state = TileFieldState.Watered;
        onFullyWatered?.Invoke(this);
    }

    public bool IsSown()
    {
        return _state == TileFieldState.Sown;
    }
    public bool IsWatered()
    {
        return _state == TileFieldState.Watered;
    }
    public void Harvest(Transform harvestSphere)
    {
        float sphereRadius = harvestSphere.localScale.x;

        for (int i = 0; i < _cropTiles.Count; i++)
        {
            if (_cropTiles[i].IsEmpty())
            {
                continue;
            }
            float distanceCropSphere = Vector3.Distance(harvestSphere.position, _cropTiles[i].transform.position);

            if(distanceCropSphere <= sphereRadius)
            {
                HarvestTile(_cropTiles[i]); 
            }
        }
    }

    private void HarvestTile(CropTile cropTile)
    {
        cropTile.Harvest();
        _tilesHarvested++;
        if(_tilesHarvested == _cropTiles.Count)
        {
            FieldFullyHarvested();
        }
    }

    private void FieldFullyHarvested()
    {
        _tilesSown = 0;
        _tilesWatered = 0;
        _tilesHarvested = 0;
        _state = TileFieldState.Empty;
        onFullyHarvested?.Invoke(this);
    }
}
