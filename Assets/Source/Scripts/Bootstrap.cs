using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private OrdersSetup _orderFactory;
    [SerializeField] private PresetsInitialize _presetsInitialize;

    [SerializeField] private CookingStoveCounterView _cookingStoveCounterView;
    [SerializeField] private DishesCounterView _dishesCounterView;

    private void Awake()
    {
        _presetsInitialize.MakeInit();
        _orderFactory.Create();
        _orderFactory.Enable();

        CookingPotFactory cookingPotFactory = new();
        CookingHolderView cookingPot = cookingPotFactory.Create(15);
        cookingPot.Enable();

        _cookingStoveCounterView.TryPlaceItem(cookingPot);

        DishFactory dishFactory = new();

        for (int i = 0; i < _dishesCounterView.MaxDishesCount; i++)
        {
            DishView newDish = dishFactory.Create(2);
            newDish.Enable();
            newDish.WashDishes();

            if (_dishesCounterView.TryAddDish(newDish) == false)
                newDish.RemoveObject();
        }
    }
}
