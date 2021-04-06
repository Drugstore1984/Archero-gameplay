using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CryoDI;

public class PlayerHealth : CryoBehaviour
{
    [SerializeField] private int _health = 100;
	[SerializeField] private GameObject _healthText;
	[Dependency]
	private EventEmitter Events { get; set; }
	private void Start()
	{
        Events.OnPlayerAdd_FireEvent();
        _healthText = GameObject.FindGameObjectWithTag("HealthText");
		UpdateText();
	}
    public void GetDamage(int damage)
	{
		_health -= damage;
		UpdateText();
		if (_health <= 0)
		{
			PlayerDeath();
		}
	}
	private void PlayerDeath()
	{
        Events.OnPlayerDead_FireEvent();
        gameObject.SetActive(false);
	}
	private void UpdateText()
    {
		_healthText.GetComponent<Text>().text = _health.ToString();
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
			GetDamage(20);
        }
    }
}
