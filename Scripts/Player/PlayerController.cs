using System;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private MobileJoystick _joystick;

    private PlayerAnimator _playerAnimator;
    private CharacterController _characterController;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }
    private void Update()
    {
        ManageMovement();
    }
    private void ManageMovement()
    {
        Vector3 moveVector = _joystick.GetMoveVector() * moveSpeed * Time.deltaTime;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        _characterController.Move(moveVector);

        _playerAnimator.ManageAnimations(moveVector);
    }
}
