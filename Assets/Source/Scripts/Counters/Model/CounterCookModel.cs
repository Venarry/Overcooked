public class CounterCookModel
{
    private ICookable _cookable;

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
        _cookable.CookStageAdded += RemoveCookable;
    }

    public void RemoveCookable()
    {
        if (_cookable != null)
        {
            _cookable.CookStageAdded -= RemoveCookable;
        }

        _cookable = null;
    }
}
