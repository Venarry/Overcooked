public interface ICookable : IPlaceable
{
    public void Cook(float step = 0);
    public void AddCookStage();
}