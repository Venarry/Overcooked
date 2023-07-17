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

        // player spawn, inputs and etc
    }
}
