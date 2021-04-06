using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private GameObject _circle, _circlePoint;
    [SerializeField] private float _untuchibleBorderY = 7f;
    private Touch _firstTouch;
    private Vector3 _touchPosition, _joysticStartPosition;
    private void Start()
    {
        _joysticStartPosition = transform.localPosition;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _firstTouch = Input.GetTouch(0);
            _touchPosition = Camera.main.ScreenToWorldPoint(_firstTouch.position);

            switch (_firstTouch.phase)
            {
                case TouchPhase.Began:
                    if (_touchPosition.y < _untuchibleBorderY)
                    {
                        transform.position = new Vector3(_touchPosition.x, _touchPosition.y, transform.position.z);
                    }
                    break;

                case TouchPhase.Stationary:
                    MoveJoystick();
                    break;

                case TouchPhase.Moved:
                    MoveJoystick();
                    break;

                case TouchPhase.Ended:
                    _circlePoint.transform.localPosition = new Vector3(0, 0, -1);
                    transform.localPosition = _joysticStartPosition;
                    break;
            }
        }
    }
    private void MoveJoystick()
    {
        _circlePoint.transform.position = _touchPosition;
        _circlePoint.transform.position = new Vector3(
            Mathf.Clamp(_circlePoint.transform.position.x, _circle.transform.position.x - 0.8f, _circle.transform.position.x + 0.8f),
            Mathf.Clamp(_circlePoint.transform.position.y, _circle.transform.position.y - 0.8f, _circle.transform.position.y + 0.8f), transform.position.z);
    }
}
