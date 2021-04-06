using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDelay : MonoBehaviour
{
    [SerializeField] private GameObject countDown;
    private void Start()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3f;
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.SetActive(false);
        Time.timeScale = 1;
    }
}
