public interface ICookable
{
    public void Cook(float step = 0);
    public void AddCookStage();
    public bool CanPlace(KitchenObjectType type);
}
