using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Type
{
    I,
    S,
    T,
    J,
    L,
    O,
    Z
}

[System.Serializable]
public struct PieceData
{
    public Type type;
    public Tile tile;
    public Vector2Int[] cells { get; private set; }

    public void Initialize()
    {
        cells = RotationData.Cells[type][0];
    }
}
