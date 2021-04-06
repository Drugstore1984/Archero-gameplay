using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamearaAdjust : MonoBehaviour
{
    [SerializeField] private float _sceneWidth = 17f;
    [SerializeField] private float _minDesiredHalfHeight = 7.8f;
    private void Start()
    {
        StartCoroutine(OrientationDefine());
    }
    private void MainCameraAdjust()
    {
        float unitsPerPixel = _sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        if (desiredHalfHeight <= _minDesiredHalfHeight)
        {
            desiredHalfHeight = _minDesiredHalfHeight;
        }
        //Debug.Log(desiredHalfHeight);
        Camera.main.orthographicSize = desiredHalfHeight;
    }
    IEnumerator OrientationDefine()
    {
        while (true)
        {
            yield return null;
            MainCameraAdjust();
        }
    }
}
