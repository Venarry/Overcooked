using System;

public class CounterInteractPresenter
{
    private readonly CounterInteractModel _counterModel;

    public event Action<ICookable> CookableSet;
    public event Action CookableRemoved;

    public bool CanInteract => _counterModel.CanInteract;

    public CounterInteractPresenter(CounterInteractModel counterModel)
    {
        _counterModel = counterModel;
    }

    public void Enable()
    {
        _counterModel.CookableSet += OnCookableSet;
        _counterModel.CookableRemoved += OnCookableRemoved;
    }

    public void Disable()
    {
        _counterModel.CookableSet -= OnCookableSet;
        _counterModel.CookableRemoved -= OnCookableRemoved;
    }

    public void Interact(PlayerObjectInteract playerObjectInteract)
    {
        _counterModel.Interact(playerObjectInteract);
    }

    public bool TryPlaceItem(IPickable item) =>
        _counterModel.TryPlaceItem(item);

    private void OnCookableSet(ICookable cookable)
    {
        CookableSet?.Invoke(cookable);
    }

    private void OnCookableRemoved()
    {
        CookableRemoved?.Invoke();
    }
}
