using System;

public class DishesCounterInteractPresenter
{
    private readonly DishesCounterInteractModel _model;

    public int MaxDishesCount => _model.MaxDishes;
    public bool HavePlace => _model.HavePlace;
    public bool CanInteract => _model.CanInteract;

    public event Action<DishView> DishAdded;
    public event Action<DishView> DishRemoved;
    public event Action<DishView> DishWashed;

    public DishesCounterInteractPresenter(DishesCounterInteractModel model)
    {
        _model = model;
    }

    public void Enable()
    {
        _model.DishAdded += OnDishAdded;
        _model.DishRemoved += OnDishRemoved;
        _model.DishWashed += OnDishWashed;
    }

    public void Disable()
    {
        _model.DishAdded -= OnDishAdded;
        _model.DishRemoved -= OnDishRemoved;
        _model.DishWashed -= OnDishWashed;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        _model.Interact(playerObjectInteract);
    }

    public bool TryAddDish(DishView dishView) => _model.TryAddDish(dishView);

    public bool TryTakeDish(out DishView dish) => _model.TryTakeDish(out dish);

    public void Wash(float step = 0)
    {
        _model.Wash(step);
    }

    private void OnDishAdded(DishView dish)
    {
        DishAdded?.Invoke(dish);
    }

    private void OnDishRemoved(DishView dish)
    {
        DishRemoved?.Invoke(dish);
    }

    private void OnDishWashed(DishView dish)
    {
        DishWashed?.Invoke(dish);
    }
}
