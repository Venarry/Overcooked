using System;
using System.Collections.Generic;

public class DishesCounterInteractModel
{
    private Stack<CookingHolderView> _dishes = new();
    private readonly ITypeProvider _typeProvider;


    public int MaxDishes { get; private set; }
    public bool CanInteract => true;

    public event Action<IPickable> DishAdded;
    public event Action<IPickable> DishRemoved;

    public DishesCounterInteractModel(int maxDishes, ITypeProvider typeProvider)
    {
        MaxDishes = maxDishes;
        _typeProvider = typeProvider;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        if (playerObjectInteract.HasPickable)
        {
            if(playerObjectInteract.TryGetPickableType(out DishView dishView))
            {
                if (TryAddDish(dishView) == false)
                    return;

                playerObjectInteract.RemovePickableRoot();
            }
        }
        else
        {
            if(TryTakeDish(out IPickable pickable) == false)
                return;

            playerObjectInteract.TryGivePickable(pickable);
        }
    }

    public bool TryAddDish(DishView dish)
    {
        if(_dishes.Count >= MaxDishes)
            return false;

        if(dish.CanPlaceOn(_typeProvider.Type) == false)
            return false;

        _dishes.Push(dish);
        DishAdded?.Invoke(dish);

        return true;
    }

    public bool TryTakeDish(out IPickable pickable)
    {
        pickable = null;

        if(_dishes.Count == 0)
            return false;

        pickable = _dishes.Peek();
        _dishes.Pop();
        DishRemoved?.Invoke(pickable);

        return true;
    }
}
