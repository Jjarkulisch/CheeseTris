using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeldGrid : MonoBehaviour
{
    public ActivePiece activePiece;
    public PieceData pieceData;
    public Tilemap tilemap { get; private set; }
    private Vector3Int spawnPosition = new Vector3Int(-10, 5, 0);
    public bool empty;
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<ActivePiece>();
        empty = true;
    }

    public void Load(Board board, PieceData data)
    {
        empty = false;
        Tilemap tilemap = board.tilemap;
        Vector3Int spawnPosition = new Vector3Int(-10, 5, 0);
        this.pieceData = data;

        for (int i = 0; i < data.cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)data.cells[i] + spawnPosition;
            tilemap.SetTile(tilePosition, data.tile);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < pieceData.cells.Length; i++)
        {
            Vector3Int tilePosition = (Vector3Int)pieceData.cells[i] + spawnPosition;
            tilemap.SetTile(tilePosition, null);
        }
    }
}
