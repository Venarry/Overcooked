public interface ICookableHolder : IInteractable
{
    public int CookablesCount { get; }
    public bool TryAddCookable(ICookable cookable);
    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder);
}
