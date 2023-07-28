using System;

public class LevelMoneyPresenter
{
    private LevelMoneyModel _moneyModel;

    public event Action<int> MoneyAdded;

    public LevelMoneyPresenter(LevelMoneyModel levelMoneyModel)
    {
        _moneyModel = levelMoneyModel;
    }

    public void Enable()
    {
        _moneyModel.MoneyAdded += OnMoneyAdded;
    }

    public void AddMoney(int value)
    {
        _moneyModel.AddMoney(value);
    }

    private void OnMoneyAdded()
    {
        MoneyAdded?.Invoke(_moneyModel.Value);
    }
}
