using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoDI;

public class Doors : CryoBehaviour
{
    [Dependency]
    private EventEmitter Events { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            Events.OnExitDoors_FireEvent();
        }
    }
}
