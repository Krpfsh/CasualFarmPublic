using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform _joystickOutLine;
    [SerializeField] private RectTransform _joystickKnob;

    [Header("Settings")]
    [SerializeField] private float _moveFactor;
    private Vector3 _clickedPosition;
    private Vector3 _move;
    private bool _canControl;
    private void Start()
    {
        HideJoystick();
    }
    private void Update()
    {
        if (_canControl)
        {
            ControlJoystick();
        }
    }
    public void ClickedOnJoystickZoneCallback()
    {
        _clickedPosition = Input.mousePosition;
        _joystickOutLine.position = _clickedPosition;
        ShowJoystick();
    }
    private void ShowJoystick()
    {
        _joystickOutLine.gameObject.SetActive(true);
        _canControl = true;
    }
    private void HideJoystick()
    {
        _joystickOutLine.gameObject.SetActive(false);
        _canControl = false;
        _move = Vector3.zero;
    }
    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - _clickedPosition;

        float moveMagnitude = direction.magnitude * _moveFactor / Screen.width;
        moveMagnitude = Mathf.Min(moveMagnitude, _joystickOutLine.rect.width/2);

        _move = direction.normalized*moveMagnitude;
        Vector3 targetPosition = _clickedPosition + _move;

        _joystickKnob.position = targetPosition;
        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }
    public Vector3 GetMoveVector()
    {
        return _move;
    }
}
