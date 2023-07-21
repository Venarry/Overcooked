using System;

public interface ICookable
{
    public event Action CookStageAdded;
    public KitchenObjectType Type { get; }
    public float MaxCookedTime { get; }
    public void Cook(float step = 0);
    public void AddCookStage();
    public void SubtractCookStage();
    public void SetOverCookedStage();
    public bool CanPlaceOn(KitchenObjectType type);
}
