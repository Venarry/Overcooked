using UnityEngine;

public class CookingPotFactory
{
    private readonly CookingPotView _template = Resources.Load<CookingPotView>(AssetsPath.CookingPot);
    private readonly CookingPotBuilder _builder = new();

    public CookingPotView Create(int maxCookablesCount)
    {
        CookingPotView cookingPotView = Object.Instantiate(_template);
        _builder.Build(cookingPotView, maxCookablesCount);

        return cookingPotView;
    }
}
