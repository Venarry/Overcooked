public class CounterCookPresenter
{
    private readonly CounterCookModel _counterCookModel;

    public CounterCookPresenter(CounterCookModel counterCookModel)
    {
        _counterCookModel = counterCookModel;
    }

    public void Cook(float step = 0)
    {
        _counterCookModel.Cook(step);
    }

    public void SetCookable(ICookable cookable)
    {
        _counterCookModel.SetCookable(cookable);
    }

    public void RemoveCookable()
    {
        _counterCookModel.RemoveCookable();
    }
}
