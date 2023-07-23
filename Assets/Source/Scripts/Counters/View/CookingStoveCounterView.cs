using UnityEngine;

public class CookingStoveCounterView : MonoBehaviour, IInteractable, ITypeProvider
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private CounterInteractPresenter _counterInteractPresenter;
    private CounterCookPresenter _counterCookPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;
    public Transform HoldTransform => _holdPoint;

    public KitchenObjectType Type => _type;

    public KitchenObjectType[] AvailablePlaceTypes => new KitchenObjectType[0];

    private void Awake()
    {
        CounterInteractModel counterInteractModel = new(_holdPoint, _type);
        CounterInteractPresenter counterInteractPresenter = new(counterInteractModel);

        CounterCookModel counterCookModel = new(this);
        CounterCookPresenter counterCookPresenter = new(counterCookModel);
        Init(counterInteractPresenter, counterCookPresenter);
        Enable();
    }

    private void FixedUpdate()
    {
        _counterCookPresenter.Cook();
    }

    public void Init(CounterInteractPresenter counterInteractPresenter, CounterCookPresenter counterCookPresenter)
    {
        _counterInteractPresenter = counterInteractPresenter;
        _counterCookPresenter = counterCookPresenter;
    }

    public void Enable()
    {
        _counterInteractPresenter.Enable();
        _counterInteractPresenter.CookableSet += SetCookable;
        _counterInteractPresenter.CookableRemoved += RemoveCookable;
    }

    public void Disable()
    {
        _counterInteractPresenter.Disable();
        _counterInteractPresenter.CookableSet -= SetCookable;
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

    private void RemoveCookable()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
