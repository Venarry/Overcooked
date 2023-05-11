public interface ICookableHolder
{
    public int CookablesCount { get; }
    public bool TryAddCookable(ICookable cookable);
    public void GiveCookablesInOutHolder(ICookableHolder cookableHolder);
}
