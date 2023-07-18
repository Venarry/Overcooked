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

    public void SetCookable(ICookable cookable)
    {
        _counterCookPresenter.SetCookable(cookable);
    }

    public void RemoveCookable()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
