using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelOrderSystemBuilder _ordersSetup;
    [SerializeField] private PresetsInitialize _presetsInitialize;

    [SerializeField] private CookingStoveCounterView _cookingStoveCounterView;
    [SerializeField] private DishesCounterView _dishesCounterView;

    private void Awake()
    {
        LevelMoneyFactory levelMoneyFactory = new();
        OrdersFactory ordersBuilder = new();
        OrdersSpawnerFactory ordersSpawnerBuilder = new();

        _presetsInitialize.MakeInit();
        _ordersSetup.Build(levelMoneyFactory, ordersBuilder, ordersSpawnerBuilder);

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
