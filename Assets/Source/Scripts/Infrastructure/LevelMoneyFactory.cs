using UnityEngine;

public class LevelMoneyFactory
{
    private readonly LevelMoneyView _prefab = Resources.Load<LevelMoneyView>(AssetsPath.LevelMoneyView);

    public LevelMoneyView Create(LevelMoneyModel levelMoneyModel)
    {
        LevelMoneyView levelMoneyView = Object.Instantiate(_prefab);
        LevelMoneyPresenter levelMoneyPresenter = new(levelMoneyModel);

        levelMoneyView.Init(levelMoneyPresenter);

        return levelMoneyView;
    }
}
