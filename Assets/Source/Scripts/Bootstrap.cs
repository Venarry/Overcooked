using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private OrdersFactory _orderFactory;

    private void Awake()
    {
        _orderFactory.Create();
        _orderFactory.Enable();

        CookingPotFactory cookingPotFactory = new CookingPotFactory();
        CookingPotView cookingPot = cookingPotFactory.Create(15);
        cookingPot.Enable();

        // player spawn, inputs and etc
    }
}
