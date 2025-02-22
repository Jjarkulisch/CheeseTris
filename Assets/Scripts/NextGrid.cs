using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NextGrid : MonoBehaviour
{
    private List<PieceData> nextPieceData;
    public Tilemap tilemap { get; private set; }
    public Board board { get; private set; }
    private Vector3Int[] spawnPositions = new Vector3Int[]
    {
        new Vector3Int(8,5,0),
        new Vector3Int(8,2,0),
        new Vector3Int(8,-1,0),
        new Vector3Int(8,-4,0),
        new Vector3Int(8,-7,0)
    };
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        board = GetComponent<Board>();
    }

    public void Load()
    {
        nextPieceData = board.bag;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            for (int c = 0; c < nextPieceData[i].cells.Length; c++)
            {
                Vector3Int tilePosition = (Vector3Int)nextPieceData[i].cells[c] + spawnPositions[i];
                tilemap.SetTile(tilePosition, nextPieceData[i].tile);
            }
        }
    }

    public void Clear()
    {
        nextPieceData = board.bag;

        for (int i = 0; i < spawnPositions.Length; i++)
        {
            for (int c = 0; c < nextPieceData[i].cells.Length; c++)
            {
                Vector3Int tilePosition = (Vector3Int)nextPieceData[i].cells[c] + spawnPositions[i];
                tilemap.SetTile(tilePosition, null);
            }
        }
    }
}
