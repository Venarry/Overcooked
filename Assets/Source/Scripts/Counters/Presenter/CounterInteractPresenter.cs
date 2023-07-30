using System;

public class CounterInteractPresenter
{
    private readonly CounterInteractModel _counterModel;

    public event Action<ICookable> CookableSet;
    public event Action<IPickable> PickableSet;
    public event Action CookableRemoved;

    public bool CanInteract => _counterModel.CanInteract;

    public CounterInteractPresenter(CounterInteractModel counterModel)
    {
        _counterModel = counterModel;
    }

    public void Enable()
    {
        _counterModel.CookableSet += OnCookableSet;
        _counterModel.PickableSet += OnPickableSet;
        _counterModel.CookableRemoved += OnCookableRemoved;
    }

    public void Disable()
    {
        _counterModel.CookableSet -= OnCookableSet;
        _counterModel.PickableSet -= OnPickableSet;
        _counterModel.CookableRemoved -= OnCookableRemoved;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        _counterModel.Interact(playerObjectInteract);
    }

    public bool TryPlaceItem(IPickable item) =>
        _counterModel.TryPlacePickable(item);

    private void OnCookableSet(ICookable cookable)
    {
        CookableSet?.Invoke(cookable);
    }

    private void OnPickableSet(IPickable pickable)
    {
        PickableSet?.Invoke(pickable);
    }

    private void OnCookableRemoved()
    {
        CookableRemoved?.Invoke();
    }
}
