using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private OrdersBuilder _ordersSetup;
    [SerializeField] private PresetsInitialize _presetsInitialize;

    [SerializeField] private CookingStoveCounterView _cookingStoveCounterView;
    [SerializeField] private DishesCounterView _dishesCounterView;

    private void Awake()
    {
        _presetsInitialize.MakeInit();
        _ordersSetup.Build();

        CookingPotFactory cookingPotFactory = new();
        CookingHolderView cookingPot = cookingPotFactory.Create(15);

        _cookingStoveCounterView.TryPlaceItem(cookingPot);

        DishFactory dishFactory = new();

        for (int i = 0; i < _dishesCounterView.MaxDishesCount; i++)
        {
            DishView newDish = dishFactory.Create(2);
            newDish.WashDish();

            if (_dishesCounterView.TryAddDish(newDish) == false)
                newDish.RemoveObject();
        }
    }
}
