public class CookingStoveBuilder
{
    public void Build(CookingStoveCounterView cookingStoveCounterView)
    {
        CounterInteractModel counterInteractModel = new(cookingStoveCounterView);
        CounterInteractPresenter counterInteractPresenter = new(counterInteractModel);

        CounterCookModel counterCookModel = new(cookingStoveCounterView);
        CounterCookPresenter counterCookPresenter = new(counterCookModel);
        cookingStoveCounterView.Init(counterInteractPresenter, counterCookPresenter);
    }
}
