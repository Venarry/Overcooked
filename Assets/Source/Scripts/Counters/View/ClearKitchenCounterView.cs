using System;
using UnityEngine;

public class ClearKitchenCounterView : MonoBehaviour, IInteractable, ITypeProvider
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private CounterInteractPresenter _counterInteractPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        CounterInteractModel counterInteractModel = new(this);
        CounterInteractPresenter counterInteractPresenter = new(counterInteractModel);

        Init(counterInteractPresenter);
    }

    public void Init(CounterInteractPresenter counterInteractPresenter)
    {
        _counterInteractPresenter = counterInteractPresenter;
    }

    private void OnEnable()
    {
        _counterInteractPresenter.Enable();
        _counterInteractPresenter.PickableSet += OnPickableSet;
    }

    private void OnDisable()
    {
        _counterInteractPresenter.Disable();
        _counterInteractPresenter.PickableSet -= OnPickableSet;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterInteractPresenter.Interact(objectInteractSystem);
    }
    private void OnPickableSet(IPickable pickable)
    {
        pickable.SetParent(_holdPoint);
    }
}
