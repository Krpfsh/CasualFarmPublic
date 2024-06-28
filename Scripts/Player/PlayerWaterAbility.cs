using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerWaterAbility : MonoBehaviour
{
    [Header(" Elements ")]
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;

    [Header(" Settings ")]
    private CropField _currentCropField;
    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();
        WaterParticles.OnWaterCollided += WaterCollidedCallback;
        CropField.onFullyWatered += CropFieldFullyWaterCallback;
        _playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }
    private void OnDestroy()
    {
        WaterParticles.OnWaterCollided -= WaterCollidedCallback;
        CropField.onFullyWatered -= CropFieldFullyWaterCallback;
        _playerToolSelector.OnToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanWater())
            _playerAnimator.StopWaterAnimation();
    }

    private void WaterCollidedCallback(Vector3[] waterPositions)
    {
        if (_currentCropField == null) return;
        _currentCropField.WaterCollidedCallback(waterPositions);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanWater())
        {
            if(_currentCropField == null)
            _currentCropField = cropField.GetComponent<CropField>();
            _playerAnimator.PlayWaterAnimation();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsSown())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(other.GetComponent<CropField>());
        };

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crop Field"))
        {
            _playerAnimator.StopWaterAnimation();
            _currentCropField = null;
        }
    }
    private void CropFieldFullyWaterCallback(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopWaterAnimation();
        };
    }

}

