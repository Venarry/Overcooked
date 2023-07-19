using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private OrdersSetup _orderFactory;
    [SerializeField] private PresetsInitialize _presetsInitialize;
    [SerializeField] private DishView _testDish;

    [SerializeField] private CookingStoveCounterView _cookingStoveCounterView;

    private void Awake()
    {
        _presetsInitialize.MakeInit();
        _orderFactory.Create();
        _orderFactory.Enable();

        CookingPotFactory cookingPotFactory = new();
        CookingPotView cookingPot = cookingPotFactory.Create(15);
        cookingPot.Enable();

        _cookingStoveCounterView.AddCookable(_testDish);

        // player spawn, inputs and etc
    }
}
