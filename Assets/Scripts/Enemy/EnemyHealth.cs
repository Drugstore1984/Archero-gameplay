using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CryoDI;

public class EnemyHealth : CryoBehaviour
{
	[SerializeField] private int _health = 100;
	[SerializeField] private GameObject _splashPrefab,_coinPrefab;
	[SerializeField] private int _pooledAmount = 1;
	public List<GameObject> splashesPrefabs;
	[Dependency]
	private EventEmitter Events { get; set; }
	private void Start()
	{
		Events.OnEnemyAdd_FireEvent();
		PoolSplashes();
	}
    public void GetDamage(int damage)
	{
		_health -= damage;

		if (_health <= 0)
		{
			Splash();
			EnemyDeath();
		}
	}
	private void EnemyDeath()
	{
		Events.OnEnemyDead_FireEvent();
		Instantiate(_coinPrefab, transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("Player"))
		{
			other.GetComponent<PlayerHealth>().GetDamage(20);
		}
	}
	private void PoolSplashes()
	{
		splashesPrefabs = new List<GameObject>();
		for (int i = 0; i < _pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(_splashPrefab);
			obj.SetActive(false);
			splashesPrefabs.Add(obj);
		}
	}
	private void Splash()
    {
		for (int i = 0; i < splashesPrefabs.Count; i++)
		{
			if (!splashesPrefabs[i].activeInHierarchy)
			{
				splashesPrefabs[i].transform.position = transform.position;
				splashesPrefabs[i].SetActive(true);
			}
		}
	}
}
