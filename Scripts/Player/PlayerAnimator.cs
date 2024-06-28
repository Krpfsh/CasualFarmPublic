using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _waterParticle;

    [Header("Settings")]
    [SerializeField] private float _moveSpeedMultiplier;

    public void ManageAnimations(Vector3 moveVector)
    {
        if(moveVector.magnitude > 0)
        {
            _animator.SetFloat("moveSpeed", moveVector.magnitude * _moveSpeedMultiplier);
            PlayRunAnimation();


            _animator.transform.forward = moveVector.normalized;
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void PlayIdleAnimation()
    {
        _animator.Play("Idle");
    }

    private void PlayRunAnimation()
    {
        _animator.Play("Run");
    }
    //saw
    public void PlaySowAnimation()
    {
        _animator.SetLayerWeight(1, 1);
    }
    public void StopSowAnimation()
    {
        _animator.SetLayerWeight(1, 0);
    }
    //water
    public void PlayWaterAnimation()
    {
        _animator.SetLayerWeight(2, 1);
    }
    public void StopWaterAnimation()
    {
        _animator.SetLayerWeight(2, 0);
        _waterParticle.Stop();
    }
    public void PlayHarvestAnimation()
    {
        _animator.SetLayerWeight(3, 1);
    }
    public void StopHarvestAnimation()
    {
        _animator.SetLayerWeight(3, 0);
    }
}
