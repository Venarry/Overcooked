public class CounterCookModel
{
    private ICookable _cookable;

    public void Cook(float step = 0)
    {
        if(_cookable != null)
            _cookable.Cook(step);
    }

    public void SetCookable(ICookable cookable)
    {
        _cookable = cookable;
    }

    public void RemoveCookable()
    {
        _cookable = null;
    }
}
