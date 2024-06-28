using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerHarvestAbility : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform _harvestSphere;
    private PlayerAnimator _playerAnimator;
    private PlayerToolSelector _playerToolSelector;

    [Header(" Settings ")]
    private CropField _currentCropField;
    private bool _canHarvest;
    private void Start()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerToolSelector = GetComponent<PlayerToolSelector>();
        //WaterParticles.OnWaterCollided += WaterCollidedCallback;
        CropField.onFullyHarvested += CropFieldFullyHarvestedCallback;
        _playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }
    private void OnDestroy()
    {
        //WaterParticles.OnWaterCollided -= WaterCollidedCallback;
        CropField.onFullyHarvested -= CropFieldFullyHarvestedCallback;
        _playerToolSelector.OnToolSelected -= ToolSelectedCallback;
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanHarvest())
            _playerAnimator.StopHarvestAnimation();
    }

    private void CropFieldFullyHarvestedCallback(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopHarvestAnimation();
        };
    }
    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanHarvest())
        {
            if (_currentCropField == null)
                _currentCropField = cropField.GetComponent<CropField>();
            _playerAnimator.PlayHarvestAnimation();
            if (_canHarvest)
            {
                _currentCropField.Harvest(_harvestSphere);
            }
        }
    }
    public void HarvestingStartedCallback()
    {
        _canHarvest = true;
    }
    public void HarvestingStoppedCallback()
    {
        _canHarvest = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsWatered())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(_currentCropField);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsWatered())
        {
            _currentCropField = other.GetComponent<CropField>();
            EnteredCropField(other.GetComponent<CropField>());
        };

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crop Field"))
        {
            _playerAnimator.StopHarvestAnimation();
            _currentCropField = null;
        }
    }


}
