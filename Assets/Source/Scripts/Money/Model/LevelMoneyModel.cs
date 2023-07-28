using System;
using UnityEngine;

public class LevelMoneyModel
{
    public int Value { get; private set; }

    public event Action MoneyAdded;

    public void AddMoney(int money)
    {
        Value += money;
        MoneyAdded?.Invoke();
    }
}
