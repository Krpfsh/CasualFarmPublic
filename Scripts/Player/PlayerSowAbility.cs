using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerToolSelector))]
public class PlayerSowAbility : MonoBehaviour
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
        SeedParticles.OnSeedsCollided += SeedsCollidedCallback;
        CropField.onFullySown += CropFieldFullySownCallback;
        _playerToolSelector.OnToolSelected += ToolSelectedCallback;
    }
    private void OnDestroy()
    {
        SeedParticles.OnSeedsCollided -= SeedsCollidedCallback;
        CropField.onFullySown -= CropFieldFullySownCallback;
        _playerToolSelector.OnToolSelected -= ToolSelectedCallback;
    }
    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!_playerToolSelector.CanSow())
            _playerAnimator.StopSowAnimation();
    }

    private void SeedsCollidedCallback(Vector3[] seedPositions)
    {
        if (_currentCropField == null) return;
        _currentCropField.SeedsCollidedCallback(seedPositions);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsEmpty())
        {
            _currentCropField = other.GetComponent<CropField>();
            
            EnteredCropField(_currentCropField);
        }

        if (CanFeedChicken(other))
        {
            FeedChicken();
        }
        
    }

    private void FeedChicken()
    {
        _playerAnimator.PlaySowAnimation();
    }

    private bool CanFeedChicken(Collider collider)
    {
        return collider.CompareTag("Chicken") && _playerToolSelector.CanSow();
    }

    private void EnteredCropField(CropField cropField)
    {
        if (_playerToolSelector.CanSow())
        {
            _playerAnimator.PlaySowAnimation();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop Field") && other.GetComponent<CropField>().IsEmpty())
        {
            EnteredCropField(other.GetComponent<CropField>());
        };

        if (CanFeedChicken(other))
        {
            FeedChicken();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crop Field") || other.CompareTag("Chicken"))
        {
            _playerAnimator.StopSowAnimation();
            _currentCropField = null;
        }
    }
    private void CropFieldFullySownCallback(CropField cropField)
    {
        if (cropField == _currentCropField)
        {
            _playerAnimator.StopSowAnimation();
        };
    }

}
