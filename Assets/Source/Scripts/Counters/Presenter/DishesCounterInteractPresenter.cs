using System;

public class DishesCounterInteractPresenter
{
    private readonly DishesCounterInteractModel _model;

    public int MaxDishesCount => _model.MaxDishes;
    public bool CanInteract => _model.CanInteract;

    public event Action<IPickable> DishAdded;
    public event Action<IPickable> DishRemoved;

    public DishesCounterInteractPresenter(DishesCounterInteractModel model)
    {
        _model = model;
    }

    public void Enable()
    {
        _model.DishAdded += OnDishAdded;
        _model.DishRemoved += OnDishRemoved;
    }

    public void Disable()
    {
        _model.DishAdded -= OnDishAdded;
        _model.DishRemoved -= OnDishRemoved;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        _model.Interact(playerObjectInteract);
    }

    public bool TryAddDish(DishView dishView) => _model.TryAddDish(dishView);

    public void Wash(float step = 0)
    {
        _model.Wash(step);
    }

    private void OnDishAdded(IPickable pickable)
    {
        DishAdded?.Invoke(pickable);
    }

    private void OnDishRemoved(IPickable pickable)
    {
        DishRemoved?.Invoke(pickable);
    }
}
