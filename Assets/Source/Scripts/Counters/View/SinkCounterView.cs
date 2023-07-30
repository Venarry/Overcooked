using System;
using UnityEngine;

public class SinkCounterView : MonoBehaviour, IInteractable, IAlternativelyInteractable, ITypeProvider
{
    [SerializeField] private DishesCounterView _dirtyDishCounter;
    [SerializeField] private DishesCounterView _cleanDishCounter; // в конструктор
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private DishesCounterInteractPresenter _dishesCounterInteractPresenter;
    private bool _isInitialized;

    public bool CanInteract => _dishesCounterInteractPresenter.CanInteract;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        DishesCounterInteractModel dishesCounterInteractModel = new(maxDishes: 1, this);
        DishesCounterInteractPresenter dishesCounterInteractPresenter = new(dishesCounterInteractModel);

        Init(dishesCounterInteractPresenter);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            InteractAlternatively();
        }
    }

    public void Init(DishesCounterInteractPresenter dishesCounterInteractPresenter)
    {
        //gameObject.SetActive(false);

        _dishesCounterInteractPresenter = dishesCounterInteractPresenter;
        _isInitialized = true;

        //gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _dishesCounterInteractPresenter.Enable();
        _dishesCounterInteractPresenter.DishAdded += OnDishAdded;
        _dishesCounterInteractPresenter.DishWashed += OnDishWashed;

        _dirtyDishCounter.DishAdded += OnDirtyCounterDishAdded;
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;

        _dishesCounterInteractPresenter.Disable();
        _dishesCounterInteractPresenter.DishAdded -= OnDishAdded;
        _dishesCounterInteractPresenter.DishWashed -= OnDishWashed;

        _dirtyDishCounter.DishAdded -= OnDirtyCounterDishAdded;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _dishesCounterInteractPresenter.Interact(objectInteractSystem);
    }

    public void InteractAlternatively()
    {
        _dishesCounterInteractPresenter.Wash(1);
    }

    private void OnDishAdded(DishView dish)
    {
        dish.SetParent(_holdPoint);
    }

    private void OnDishWashed(DishView _)
    {
        if (_dishesCounterInteractPresenter.TryTakeDish(out DishView washedDish) == false)
            return;

        if (_cleanDishCounter.TryAddDish(washedDish) == false)
            return;

        if (_dirtyDishCounter.TryTakeDish(out DishView dish) == false)
            return;

        _dishesCounterInteractPresenter.TryAddDish(dish);
    }

    private void OnDirtyCounterDishAdded()
    {
        if (_dishesCounterInteractPresenter.HavePlace == false)
            return;

        if(_dirtyDishCounter.TryTakeDish(out DishView dish))
        {
            _dishesCounterInteractPresenter.TryAddDish(dish);
        }
    }
}
