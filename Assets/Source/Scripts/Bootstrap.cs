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

        CookingPotFactory cookingPotFactory = Resources.Load<CookingPotFactory>("Factorys/CookingPotFactory");
        cookingPotFactory.Create();

        // player spawn, inputs and etc
    }
}
