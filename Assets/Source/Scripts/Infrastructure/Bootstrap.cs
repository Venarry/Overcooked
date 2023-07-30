using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private LevelOrderSystemBuilder _ordersSetup;
    [SerializeField] private PresetsInitialize _presetsInitialize;

    [SerializeField] private CookingStoveCounterView _cookingStoveCounterView;

    private void Awake()
    {
        LevelMoneyFactory levelMoneyFactory = new();
        OrdersFactory ordersBuilder = new();
        OrdersSpawnerFactory ordersSpawnerBuilder = new();
        OrderCounterBuilder orderCounterBuilder = new();
        LevelMoneyModel levelMoneyModel = new();
        OrdersHandler ordersHandler = new(levelMoneyModel);

        _presetsInitialize.MakeInit();
        _ordersSetup.Build(levelMoneyModel, ordersHandler, levelMoneyFactory, ordersBuilder, ordersSpawnerBuilder, orderCounterBuilder);

        CookingPotFactory cookingPotFactory = new();
        CookingHolderView cookingPot = cookingPotFactory.Create(15);

        _cookingStoveCounterView.TryPlaceItem(cookingPot);
    }
}
