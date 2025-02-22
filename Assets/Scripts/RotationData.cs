using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RotationData
{
    public static readonly Dictionary<Type, Vector2Int[][]> Cells = new Dictionary<Type, Vector2Int[][]>()
    {
        {Type.I, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) },
         new Vector2Int[] {new Vector2Int(1, 2), new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(1, -1) },
         new Vector2Int[] {new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },
         new Vector2Int[] {new Vector2Int(0, 2), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) } } },
        {Type.J, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) },
         new Vector2Int[] {new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(-1, 0) },
         new Vector2Int[] {new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) } } },
        {Type.L, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) },
         new Vector2Int[] {new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(0, -1) } } },
        {Type.O, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) } } },
        {Type.S, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0) },
         new Vector2Int[] {new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1) },
         new Vector2Int[] {new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(0, -1) } } },
        {Type.T, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1) },
         new Vector2Int[] {new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(-1, 0) },
         new Vector2Int[] {new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) } } },
        {Type.Z, new Vector2Int[][]
        {new Vector2Int[] {new Vector2Int(-1, 1), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(1, 0) },
         new Vector2Int[] {new Vector2Int(1, 1), new Vector2Int(1, 0), new Vector2Int(0, 0), new Vector2Int(0, -1) },
         new Vector2Int[] {new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(-1, 0) },
         new Vector2Int[] {new Vector2Int(-1, -1), new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(0, 1) } } },
    };

    public static readonly Vector2Int[,] WallKicks = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
    };

    public static readonly Vector2Int[,] WallKicksI = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
        { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
    };
    public static readonly Vector2Int[,] WallKicks180 = new Vector2Int[,] {
        { new Vector2Int(0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 1), new Vector2Int( 1, 0), new Vector2Int(-1, 0) },
        { new Vector2Int(0, 0), new Vector2Int( 0,-1), new Vector2Int(-1,-1), new Vector2Int( 1,-1), new Vector2Int(-1, 0), new Vector2Int( 1, 0) },
        { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 2), new Vector2Int( 1, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 1) },
        { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 2), new Vector2Int(-1, 1), new Vector2Int( 0, 2), new Vector2Int( 0, 1) }
    };
}
