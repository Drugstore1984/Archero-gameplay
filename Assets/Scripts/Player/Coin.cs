using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoDI;

public class Coin : CryoBehaviour
{
    [Dependency]
    private EventEmitter Events { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Events.OnCoinAdd_FireEvent();
            gameObject.SetActive(false);
        }
    }
}
