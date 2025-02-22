using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : ActivePiece
{
    public Tile tile;
    public ActivePiece tracking;
    public Tilemap tilemap { get; private set; }

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }
    public void Copy()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (board == null)
                board = tracking.board;

            cells[i] = tracking.cells[i];
        }
    }
    private void Drop()
    {
        Vector3Int position = tracking.position;

        int current = position.y;
        int bottom = -20 / 2 - 1;

        if (tracking.data.type == Type.I && tracking.rotation == 0)
            bottom--;
            
        board.Clear(tracking);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (board.PositionValid(tracking, position))
            {
                this.position = position;
            }
            else
            {
                break;
            }
        }

        board.Set(tracking);
    }

    private void Set()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, tile);
        }
    }
}