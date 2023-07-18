using System;

public class CounterInteractPresenter<T>
{
    private readonly CounterInteractModel<T> _counterModel;

    public event Action<ICookable> CookableSet;
    public event Action CookableRemoved;

    public bool CanInteract => _counterModel.CanInteract;

    public CounterInteractPresenter(CounterInteractModel<T> counterModel)
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

    private void OnCookableSet(ICookable cookable)
    {
        CookableSet?.Invoke(cookable);
    }

    private void OnCookableRemoved()
    {
        CookableRemoved?.Invoke();
    }
}
