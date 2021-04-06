using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoDI;

public class GameContainer : UnityStarter
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }
    protected override void SetupContainer(CryoContainer container)
    {
        container.RegisterSingleton<EventEmitter>(LifeTime.Scene);
    }
}
