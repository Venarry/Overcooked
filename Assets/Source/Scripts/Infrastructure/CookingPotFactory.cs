using UnityEngine;

public class CookingPotFactory
{
    private readonly CookingHolderView _template = Resources.Load<CookingHolderView>(AssetsPath.CookingPot);
    private readonly CookingPotBuilder _builder = new();

    public CookingHolderView Create(int maxCookablesCount)
    {
        CookingHolderView cookingPotView = Object.Instantiate(_template);
        _builder.Build(cookingPotView, maxCookablesCount);

        return cookingPotView;
    }
}
