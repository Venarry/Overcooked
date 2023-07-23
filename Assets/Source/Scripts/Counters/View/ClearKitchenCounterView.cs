using UnityEngine;

public class ClearKitchenCounterView : MonoBehaviour, IInteractable
{
    [SerializeField] private KitchenObjectType _type;
    [SerializeField] private Transform _holdPoint;

    private CounterInteractPresenter _counterInteractPresenter;

    public bool CanInteract => _counterInteractPresenter.CanInteract;

    private void Awake()
    {
        CounterInteractModel counterInteractModel = new(_holdPoint, _type);
        CounterInteractPresenter counterInteractPresenter = new(counterInteractModel);

        Init(counterInteractPresenter);
        Enable();
    }

    public void Init(CounterInteractPresenter counterInteractPresenter)
    {
        _counterInteractPresenter = counterInteractPresenter;
    }

    public void Enable()
    {
        _counterInteractPresenter.Enable();
    }

    public void Disable()
    {
        _counterInteractPresenter.Disable();
    }

    public void Interact(PlayerObjectInteract objectInteractSystem)
    {
        _counterInteractPresenter.Interact(objectInteractSystem);
    }
}
