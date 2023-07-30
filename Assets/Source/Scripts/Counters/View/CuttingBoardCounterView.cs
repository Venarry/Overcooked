using System;
using UnityEngine;

public class CuttingBoardCounterView : MonoBehaviour, IInteractable, ITypeProvider
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private float _cookingStepPerTick;

    private CounterInteractPresenter _counterInteractPresenter;
    private CounterCookPresenter _counterCookPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;
    public KitchenObjectType Type => _type;
    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        CounterInteractModel counterModel = new(this);
        CounterInteractPresenter counterInteractPresenter = new(counterModel);

        CounterCookModel counterCookModel = new(this);
        CounterCookPresenter counterCookPresenter = new(counterCookModel);

        Init(counterInteractPresenter, counterCookPresenter);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _counterCookPresenter.Cook(_cookingStepPerTick);
        }
    }

    public void Init(CounterInteractPresenter counterInteractPresenter, CounterCookPresenter counterCookPresenter)
    {
        _counterCookPresenter = counterCookPresenter;
        _counterInteractPresenter = counterInteractPresenter;
    }

    private void OnEnable()
    {
        _counterInteractPresenter.Enable();

        _counterInteractPresenter.CookableSet += OnCookableSet;
        _counterInteractPresenter.PickableSet += OnPickableSet;
        _counterInteractPresenter.CookableRemoved += OnCookableRemoved;
    }

    private void OnDisable()
    {
        _counterInteractPresenter.Disable();

        _counterInteractPresenter.CookableSet -= OnCookableSet;
        _counterInteractPresenter.PickableSet -= OnPickableSet;
        _counterInteractPresenter.CookableRemoved -= OnCookableRemoved;
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterInteractPresenter.Interact(objectInteractSystem);
    }

    public void OnCookableSet(ICookable cookable)
    {
        _counterCookPresenter.SetCookable(cookable);
    }

    private void OnPickableSet(IPickable pickable)
    {
        pickable.SetParent(_holdPoint);
    }

    public void OnCookableRemoved()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
