using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healther : Receiver<IHealth>
{
    public event Action OnDead;
    public event Action<int, int> OnHealthChange;
    private int _health;
    public int MaxHealth => _data.maxHealth;
    public int Health
    {
        get { return _health; }
        set 
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChange?.Invoke(MaxHealth, _health);

            if (_health <= 0)
                OnDead?.Invoke();
        }
    }

    public void Kill()
    {
        Health = 0;
    }

    public void Damage(int damage)
    {
        Health -= damage;
    }

    public void Restore()
    {
        Health = MaxHealth;
    }

    protected override void OnReceive()
    {
        base.OnReceive();
        Restore();
    }


}
