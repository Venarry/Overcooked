using System;
using UnityEngine;

public class SinkCounterView : MonoBehaviour, IInteractable, IAlternativelyInteractable, ITypeProvider
{
    [SerializeField] private DishesCounterView _cleanDishCounter; // в конструктор
    [SerializeField] private DishesCounterView _dirtyDishCounter;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private DishesCounterInteractPresenter _dishesCounterInteractPresenter;

    public bool CanInteract => _dishesCounterInteractPresenter.CanInteract;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        DishesCounterInteractModel dishesCounterInteractModel = new(1, this);
        DishesCounterInteractPresenter dishesCounterInteractPresenter = new(dishesCounterInteractModel);

        Init(dishesCounterInteractPresenter);
        Enable();
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
        _dishesCounterInteractPresenter = dishesCounterInteractPresenter;
    }

    public void Enable()
    {
        _dishesCounterInteractPresenter.Enable();
        _dishesCounterInteractPresenter.DishAdded += OnDishAdded;
    }

    public void Disable()
    {
        _dishesCounterInteractPresenter.Disable();
        _dishesCounterInteractPresenter.DishAdded -= OnDishAdded;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _dishesCounterInteractPresenter.Interact(objectInteractSystem);
    }

    public void InteractAlternatively()
    {
        _dishesCounterInteractPresenter.Wash(1);
    }
    private void OnDishAdded(IPickable pickable)
    {
        pickable.SetParent(_holdPoint);
    }
}
