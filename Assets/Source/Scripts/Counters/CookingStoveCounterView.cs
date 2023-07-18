using System.Collections.Generic;
using UnityEngine;

public class CookingStoveCounterView : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private CounterInteractPresenter<ICookable> _counterInteractPresenter;
    private CounterCookPresenter _counterCookPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;

    private void Awake()
    {
        CounterInteractModel<ICookable> counterInteractModel = new(_holdPoint, _type);
        _counterInteractPresenter = new(counterInteractModel);
        CounterCookModel counterCookModel = new();
        _counterCookPresenter = new(counterCookModel);
        Enable();
    }

    private void FixedUpdate()
    {
        _counterCookPresenter.Cook();
    }

    public void Init(CounterInteractPresenter<ICookable> counterInteractPresenter, CounterCookPresenter counterCookPresenter)
    {
        _counterInteractPresenter = counterInteractPresenter;
        _counterCookPresenter = counterCookPresenter;
    }

    public void Enable()
    {
        _counterInteractPresenter.Enable();
        _counterInteractPresenter.CookableSet += OnCoockableSet;
        _counterInteractPresenter.CookableRemoved += OnCoockableRemove;
    }

    public void Disable()
    {
        _counterInteractPresenter.Disable();
        _counterInteractPresenter.CookableSet -= OnCoockableSet;
        _counterInteractPresenter.CookableRemoved -= OnCoockableRemove;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterInteractPresenter.Interact(objectInteractSystem);
    }

    private void OnCoockableSet(ICookable cookable)
    {
        _counterCookPresenter.SetCookable(cookable);
    }

    private void OnCoockableRemove()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
