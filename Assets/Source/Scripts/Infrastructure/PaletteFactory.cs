using UnityEngine;

public class PaletteFactory
{
    private readonly Material _template = Resources.Load<Material>(AssetsPath.PaletteMaterial);

    public Material Create() => new Material(_template);
}
