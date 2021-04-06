using System;
using UnityEngine;
using CryoDI;
using UnityEngine.UI;

public class LevelManager : CryoBehaviour
{
    [Dependency]
    private EventEmitter Events { get; set; }
    private int _enemyCounter = 0;
    private bool _playerDead = false;
    [SerializeField] GameObject _deathMenu, _winMenu;
    private void Start()
    {
        Events.OnEnemyAdd += Event_OnEnemyAdd;
        Events.OnEnemyDead += Event_OnEnemyDead;
        Events.OnPlayerAdd += Event_OnPlayerAdd;
        Events.OnPlayerDead += Event_OnPlayerDead;
        Events.OnExitDoors += Event_OnExitDoors;
    }
    private void Event_OnEnemyAdd(object sender, EventArgs e)
    {
        _enemyCounter++;
    }
    private void Event_OnEnemyDead(object sender, EventArgs e)
    {
        _enemyCounter--;
    }
    private void Event_OnPlayerAdd(object sender, EventArgs e)
    {
        _playerDead = false;
    }
    private void Event_OnPlayerDead(object sender, EventArgs e)
    {
        _playerDead = true;
    }
    private void Event_OnExitDoors(object sender, EventArgs e)
    {
        OnExitDoors();
    }
    private void Update()
    {
        if (_enemyCounter <= 0)
        {
            PlayerWin();
        }
        if (_playerDead)
        {
            PlayerDeath();
        }
    }
    private void PlayerDeath()
    {
        Time.timeScale = 0;
        _deathMenu.SetActive(true);
    }
    private void PlayerWin()
    {
        GameObject[] doors = GameObject.FindGameObjectsWithTag("Doors");
        foreach (GameObject door in doors)
        {
            door.gameObject.GetComponent<MeshRenderer>().enabled = false;
            door.gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }
    private void OnExitDoors()
    {
        Time.timeScale = 0;
        _winMenu.SetActive(true);
    }
}
