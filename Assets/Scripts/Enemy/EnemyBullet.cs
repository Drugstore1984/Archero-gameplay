using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] int pooledAmount = 1;
    public List<GameObject> splashes;
    [SerializeField] GameObject bulletSplash;
    [SerializeField] int damage = 20;
    private void Awake()
    {
        splashes = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bulletSplash);
            obj.SetActive(false);
            splashes.Add(obj);
        }
    }
    private void DisableBullet()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < splashes.Count; i++)
        {
            if (!splashes[i].activeInHierarchy)
            {
                splashes[i].transform.position = transform.position;
                splashes[i].SetActive(true);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.tag.Equals("Untouchable"))
        {
            DisableBullet();
        }
        if (other.gameObject.tag.Equals("Player"))
        {
            other.GetComponent<PlayerHealth>().GetDamage(damage);
        }
    }
}
