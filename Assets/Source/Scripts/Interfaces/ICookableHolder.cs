public interface ICookableHolder
{
    public int MaxCookables { get; }
    public int CookablesCount { get; }
    public bool TryAddCookable(ICookable cookable);
    public bool TryAddCookables(ICookable[] cookable);
    public ICookable[] TakeCookables();
}
