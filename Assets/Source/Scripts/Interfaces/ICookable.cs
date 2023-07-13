public interface ICookable
{
    public KitchenObjectType Type { get; }
    public void Cook(float step = 0);
    public void AddCookStage();
    public bool CanPlace(KitchenObjectType type);
}
