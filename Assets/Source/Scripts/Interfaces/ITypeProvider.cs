using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITypeProvider
{
    public KitchenObjectType Type { get; }
    public KitchenObjectType[] AvailablePlaceTypes { get; }
}
