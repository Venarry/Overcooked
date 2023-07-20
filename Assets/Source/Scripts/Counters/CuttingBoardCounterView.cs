using UnityEngine;

public class CuttingBoardCounterView : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private float _cookingStepPerTick;

    private CounterInteractPresenter _counterInteractPresenter;
    private CounterCookPresenter _counterCookPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;

    private void Awake()
    {
        CounterInteractModel counterModel = new(_holdPoint, _type);
        CounterInteractPresenter counterInteractPresenter = new(counterModel);

        CounterCookModel counterCookModel = new();
        CounterCookPresenter counterCookPresenter = new(counterCookModel);

        Init(counterInteractPresenter, counterCookPresenter);
        Enable();
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

    public void Enable()
    {
        _counterInteractPresenter.Enable();

        _counterInteractPresenter.CookableSet += OnCookableSet;
        _counterInteractPresenter.CookableRemoved += OnCookableRemoved;
    }

    public void Disable()
    {
        _counterInteractPresenter.Disable();

        _counterInteractPresenter.CookableSet -= OnCookableSet;
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

    public void OnCookableRemoved()
    {
        _counterCookPresenter.RemoveCookable();
    }
}
