using System;
using UnityEngine;

public class CookingStoveCounterView : MonoBehaviour, IInteractable, ITypeProvider
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private CounterInteractPresenter _counterInteractPresenter;
    private CounterCookPresenter _counterCookPresenter;

    private bool _isInitialized;

    public bool CanInteract => _counterInteractPresenter.CanInteract;
    public Transform HoldTransform => _holdPoint;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void FixedUpdate()
    {
        _counterCookPresenter.Cook();
    }

    public void Init(CounterInteractPresenter counterInteractPresenter, CounterCookPresenter counterCookPresenter)
    {
        gameObject.SetActive(false);

        _counterInteractPresenter = counterInteractPresenter;
        _counterCookPresenter = counterCookPresenter;
        _isInitialized = true;

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            return;

        _counterInteractPresenter.Enable();
        _counterInteractPresenter.CookableSet += SetCookable;
        _counterInteractPresenter.PickableSet += OnPickableSet;
        _counterInteractPresenter.CookableRemoved += RemoveCookable;
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            return;
        Debug.Log("Disabgle");
        _counterInteractPresenter.Disable();
        _counterInteractPresenter.CookableSet -= SetCookable;
        _counterInteractPresenter.PickableSet -= OnPickableSet;
        _counterInteractPresenter.CookableRemoved -= RemoveCookable;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterInteractPresenter.Interact(objectInteractSystem);
    }

    public bool TryPlaceItem(IPickable cookable) => 
        _counterInteractPresenter.TryPlaceItem(cookable);

    private void SetCookable(ICookable cookable)
    {
        _counterCookPresenter.SetCookable(cookable);
    }

    private void OnPickableSet(IPickable pickable)
    {
        pickable.SetParent(_holdPoint);
    }

    private void RemoveCookable()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
