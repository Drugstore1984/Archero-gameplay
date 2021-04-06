using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Obstacle"))
        {
            Physics.IgnoreCollision(collision.collider,GetComponent<Collider>());
        }
    }
}
