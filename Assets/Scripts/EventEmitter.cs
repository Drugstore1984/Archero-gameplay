using System;
public class EventEmitter
{
    public event EventHandler OnEnemyDead;
    public event EventHandler OnEnemyAdd;
    public event EventHandler OnPlayerAdd;
    public event EventHandler OnPlayerDead;
    public event EventHandler OnCoinAdd;
    public event EventHandler OnExitDoors;

    public void OnEnemyDead_FireEvent()
    {
        OnEnemyDead?.Invoke(this, EventArgs.Empty);
    }
    public void OnEnemyAdd_FireEvent()
    {
        OnEnemyAdd?.Invoke(this, EventArgs.Empty);
    }
    public void OnPlayerAdd_FireEvent()
    {
        OnPlayerAdd?.Invoke(this, EventArgs.Empty);
    }
    public void OnPlayerDead_FireEvent()
    {
        OnPlayerDead?.Invoke(this, EventArgs.Empty);
    }
    public void OnCoinAdd_FireEvent()
    {
        OnCoinAdd?.Invoke(this, EventArgs.Empty);
    }
    public void OnExitDoors_FireEvent()
    {
        OnExitDoors?.Invoke(this, EventArgs.Empty);
    }
}

