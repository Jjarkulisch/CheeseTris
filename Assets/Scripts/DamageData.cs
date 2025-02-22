using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageData
{
    public static readonly Dictionary<int, Dictionary<ClearType, int[]>> LinesSent = new Dictionary<int, Dictionary<ClearType, int[]>>()
    {
        {0, new Dictionary<ClearType, int[]>()
        {{ClearType.Single, new int[]{ 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 } },
        {ClearType.Double, new int[] { 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6 } },
        {ClearType.Triple, new int[] { 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12 } },
        {ClearType.Quad, new int[]   { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 } },
        {ClearType.TSS, new int[]    { 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11, 12 } },
        {ClearType.TSD, new int[]    { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 } },
        {ClearType.TST, new int[]    { 6, 7, 9, 10, 12, 13, 15, 16, 18, 19, 21, 22, 24, 25, 27, 28, 30, 31, 33, 34, 36 } } }
        },
        {1, new Dictionary<ClearType, int[]>()
        {{ClearType.Quad, new int[]  { 5, 6, 7, 8, 10, 11, 12, 13, 15, 16, 17, 18, 20, 21, 22, 23, 25, 26, 27, 28, 30 } },
        {ClearType.TSS, new int[]    { 3, 3, 4, 5, 6, 6, 7, 8, 9, 9, 10, 11, 12, 12, 13, 14, 15, 15, 16, 17, 18 } },
        {ClearType.TSD, new int[]    { 5, 6, 7, 8, 10, 11, 12, 13, 15, 16, 17, 18, 20, 21, 22, 23, 25, 26, 27, 28, 30 } },
        {ClearType.TST, new int[]    { 7, 8, 10, 12, 14, 15, 17, 19, 21, 22, 24, 26, 28, 29, 31, 33, 35, 36, 38, 40, 42 } } }
        },
        {2, new Dictionary<ClearType, int[]>()
        {{ClearType.Quad, new int[]  { 6, 7, 9, 10, 12, 13, 15, 16, 18, 19, 21, 22, 24, 25, 27, 28, 30, 31, 33, 34, 36 } },
        {ClearType.TSS, new int[]    { 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 } },
        {ClearType.TSD, new int[]    { 6, 7, 9, 10, 12, 13, 15, 16, 18, 19, 21, 22, 24, 25, 27, 28, 30, 31, 33, 34, 36 } },
        {ClearType.TST, new int[]    { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48 } } }
        },
        {3, new Dictionary<ClearType, int[]>()
        {{ClearType.Quad, new int[]  { 7, 8, 10, 12, 14, 15, 17, 19, 21, 22, 24, 26, 28, 29, 31, 33, 35, 36, 38, 40, 42 } },
        {ClearType.TSS, new int[]    { 5, 6, 7, 8, 10, 11, 12, 13, 15, 16, 17, 18, 20, 21, 22, 23, 25, 26, 27, 28, 30 } },
        {ClearType.TSD, new int[]    { 7, 8, 10, 12, 14, 15, 17, 19, 21, 22, 24, 26, 28, 29, 31, 33, 35, 36, 38, 40, 42 } },
        {ClearType.TST, new int[]    { 9, 11, 13, 15, 18, 20, 22, 24, 27, 29, 31, 33, 36, 38, 40, 42, 45, 47, 49, 51, 54 } } }
        },
        {4, new Dictionary<ClearType, int[]>()
        {{ClearType.Quad, new int[]  { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48 } },
        {ClearType.TSS, new int[]    { 6, 7, 9, 10, 12, 13, 15, 16, 18, 19, 21, 22, 24, 25, 27, 28, 30, 31, 33, 34, 36 } },
        {ClearType.TSD, new int[]    { 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48 } },
        {ClearType.TST, new int[]    { 10, 12, 15, 17, 20, 22, 25, 27, 30, 32, 35, 37, 40, 42, 45, 47, 50, 52, 55, 57, 60 } } }
        },
    };
    public static readonly Dictionary<ClearType, int> Score = new Dictionary<ClearType, int>()
    {
        {ClearType.Single, 100},
        {ClearType.Double, 300},
        {ClearType.Triple, 500},
        {ClearType.Quad, 800},
        {ClearType.TSS, 800},
        {ClearType.TSD, 1200},
        {ClearType.TST, 1600},
        {ClearType.FC, 3500}
    };

    public enum ClearType
    {
        None,
        Single,
        Double,
        Triple,
        Quad,
        TSS,
        TSD,
        TST,
        FC
    }
}