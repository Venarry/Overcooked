using TMPro;
using UnityEngine;

public class LevelMoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyLabel;
    [SerializeField] private TextMeshProUGUI _moneyMultiplierLabel;

    private readonly int _maxMoneyMultiplier = 4;
    private readonly float _timeForMultilierReduce = 25;

    private LevelMoneyPresenter _presenter;
    private int _currentMoneyMultiplier = 1;
    private float _timer;

    private void Start()
    {
        RefreshUI(0);
    }

    public void Update()
    {
        if (_currentMoneyMultiplier > 1)
        {
            _timer += Time.deltaTime;

            if (_timer >= _timeForMultilierReduce)
            {
                _timer = 0;
                _currentMoneyMultiplier--;
                _moneyMultiplierLabel.text = _currentMoneyMultiplier.ToString();
            }
        }
    }

    public void Init(LevelMoneyPresenter levelMoneyPresenter)
    {
        _presenter = levelMoneyPresenter;
    }

    public void Enable()
    {
        _presenter.Enable();
        _presenter.MoneyAdded += OnMoneyAdded;
    }

    public void AddMoney(int startValue)
    {
        _presenter.AddMoney(startValue * _currentMoneyMultiplier);
    }
    
    private void OnMoneyAdded(int value)
    {
        if (_currentMoneyMultiplier < _maxMoneyMultiplier)
            _currentMoneyMultiplier++;

        RefreshUI(value);
    }

    private void RefreshUI(int moneyValue)
    {
        _moneyLabel.text = moneyValue.ToString();
        _moneyMultiplierLabel.text = _currentMoneyMultiplier.ToString();
    }
}
