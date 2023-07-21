public class CounterCookModel
{
    private ICookable _cookable;
    private readonly ITypeProvider _typeProvider;

    public CounterCookModel(ITypeProvider typeProvider)
    {
        _typeProvider = typeProvider;
    }

    public void Cook(float step = 0)
    {
        _cookable?.Cook(step);
    }

    public void SetCookable(ICookable cookable)
    {
        if (_cookable != null)
        {
            return;
        }

        _cookable = cookable;
        _cookable.CookStageAdded += OnCookStageAdded;
    }

    public void RemoveCookable()
    {
        if (_cookable != null)
        {
            _cookable.CookStageAdded -= RemoveCookable;
        }

        _cookable = null;
    }

    private void OnCookStageAdded()
    {
        if (_cookable.CanPlaceOn(_typeProvider.Type))
            return;

        RemoveCookable();
    }
}
