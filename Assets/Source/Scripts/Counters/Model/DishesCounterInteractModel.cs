using System;
using System.Collections.Generic;
using UnityEngine;

public class DishesCounterInteractModel
{
    private readonly Stack<DishView> _dishes = new();
    private readonly ITypeProvider _typeProvider;

    public int MaxDishes { get; private set; }
    public bool HavePlace => _dishes.Count < MaxDishes;
    public bool CanInteract => true;

    public event Action<DishView> DishAdded;
    public event Action<DishView> DishRemoved;
    public event Action<DishView> DishWashed;

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
            if(TryTakeDish(out DishView dish) == false)
                return;

            playerObjectInteract.TryGivePickable(dish);
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
        dish.DishWashed += OnDishWashed;

        return true;
    }

    public bool TryTakeDish(out DishView dish)
    {
        dish = null;

        if(_dishes.Count == 0)
            return false;

        dish = _dishes.Pop();
        dish.DishWashed -= OnDishWashed;
        DishRemoved?.Invoke(dish);
        dish.ResetCookingStageProcess();

        return true;
    }

    public void Wash(float step = 0)
    {
        if (_dishes.Count == 0)
            return;

        DishView currentDish = _dishes.Peek();
        currentDish.Wash(step);
    }

    private void OnDishWashed()
    {
        if (_dishes.Count == 0)
            return;

        DishWashed?.Invoke(_dishes.Peek());
    }
}
