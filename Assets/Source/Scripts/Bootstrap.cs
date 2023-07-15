using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private OrdersPresenter _orderPresenter;

    private void Awake()
    {
        _orderPresenter.Init();

        // player spawn and etc
    }
}
