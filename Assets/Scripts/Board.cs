using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    public int score = 0;
    public int B2BLvl = 0;
    public int B2BRaw = 0;
    public int combo = 0;
    public PieceData[] pieces;
    public List<PieceData> bag;
    public int cheeseQueue = 0;
    public TileBase cheeseTile;
    public StatCanvas statCanvas;
    public MeterGrid meterGrid;
    public NextGrid nextGrid { get; private set; }
    public Tilemap tilemap { get; private set; }
    public ActivePiece activePiece { get; private set; }
    public Vector3Int spawnPosition = new Vector3Int(-1,9,0);
    public RectInt bounds = new RectInt(-5,-11,10,20);
    public float cheeseDelay = 1f;
    public float cheeseTime;
    public float level = 5;
    public int levelDelay = 30;
    public float levelTime;
    public Sounds sounds;
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        activePiece = GetComponentInChildren<ActivePiece>();
        nextGrid = GetComponentInChildren<NextGrid>();

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].Initialize();
        }
    }

    private void Start()
    {
        GenerateBag(pieces);
        Spawn();
        nextGrid.Load();
        cheeseTime = Time.time + 10;
        levelTime = Time.time + levelDelay;
    }

    private void Update()
    {
        Clear(activePiece);
        
        activePiece.lockTime += Time.deltaTime;

        activePiece.HandleInput();

        if (Time.time > activePiece.stepTime)
            activePiece.Step();

        if (meterGrid.coolDown)
        {
            cheeseTime = Time.time + cheeseDelay;
        }
        else if (Time.time > cheeseTime)
        {
            cheeseQueue++;
            cheeseTime = Time.time + cheeseDelay;
        }

        if (Time.time > levelTime)
            RaiseLevel();
        
        Set(activePiece);
    }

    public void Spawn()
    {
        PieceData data = bag[0];
        activePiece.Initialize(this, spawnPosition, data);

        if (PositionValid(activePiece, spawnPosition) == false)
        {
            GameOver();
        }
        Set(activePiece);
        bag.RemoveAt(0);
        if (bag.Count < pieces.Length)
        {
            GenerateBag(pieces);
        }
    }

    public void Set(ActivePiece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(ActivePiece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public bool PositionValid(ActivePiece piece, Vector3Int position)
    {
        RectInt bounds = this.bounds;

        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public bool LineFull(int y)
    {
        bool lineFull = true;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            Vector3Int tilePosition = new Vector3Int(x, y);
            if (!tilemap.HasTile(tilePosition))
            {
                lineFull = false;
            }
        }
        return lineFull;
    }
    public void ClearLines()
    {
        int linesCleared = 0;
        bool isTspin = activePiece.data.type == Type.T ? IsTSpin() : false;


        for (int y = bounds.yMax; y >= bounds.yMin; y--)
            if (LineFull(y) == true)
            {
                linesCleared++;
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y);
                    tilemap.SetTile(tilePosition, null);
                }
                MoveAllLines(y, Vector2Int.down);
            }

        if (linesCleared != 0)
        {
            if (isTspin)
                sounds.PlayTspin();

            AwardScore(GetClearType(linesCleared, isTspin));
        }
            
        else
        {
            if (cheeseQueue != 0)
                SpawnCheese();
            if (combo > 3)
                sounds.PlayComboBreak();
            combo = 0;
        }
            

        statCanvas.Refresh();
    }
    private bool IsTSpin()
    {
        if (activePiece.prevMove != ActivePiece.MoveType.Rotate)
            return false;

        Vector2Int[] positionsToCheck = new Vector2Int[]
        { new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(-1, 1), new Vector2Int(-1, -1) };
        int cornersFull = 0;

        foreach (Vector2Int position in positionsToCheck)
            if (tilemap.HasTile((Vector3Int)position + activePiece.position))
                cornersFull++;
            

        return cornersFull > 2 ? true : false;
    }
    public DamageData.ClearType GetClearType(int linesCleared, bool isTspin)
    {
        switch (linesCleared)
        {
            case 1:
                return isTspin ? DamageData.ClearType.TSS: DamageData.ClearType.Single;
            case 2:
                return isTspin ? DamageData.ClearType.TSD : DamageData.ClearType.Double;
            case 3:
                return isTspin ? DamageData.ClearType.TST : DamageData.ClearType.Triple;
            case 4:
                return DamageData.ClearType.Quad;
        }
        return DamageData.ClearType.None;
    }
    public void AwardScore(DamageData.ClearType clearType)
    {
        score += DamageData.Score[clearType];
        sounds.PlayLineClear(combo > 15 ? 16 : combo);

        if ((int)clearType < 4)
        {
            meterGrid.Fill(DamageData.LinesSent[0][clearType][combo]);
            B2BRaw = B2BLvl = 0;
        } 
        else
        {
            meterGrid.Fill(DamageData.LinesSent[B2BLvl][clearType][combo]);

            if (clearType == DamageData.ClearType.Quad)
                sounds.PlayTetris();

            B2BRaw++;
            if (B2BRaw >= 1 && B2BRaw <= 2)
                B2BLvl = 1;
            else if (B2BRaw >= 3 && B2BRaw <= 7)
                B2BLvl = 2;
            else if (B2BRaw >= 8 && B2BRaw <= 23)
                B2BLvl = 3;
            else if (B2BRaw >= 24 && B2BRaw <= 66)
                B2BLvl = 4;
        }
        if (combo > 0)
            score += 50 * combo;
        if (combo < 21)
            combo++;
    }
    private void MoveAllLines(int row, Vector2Int direction)
    {
        if (direction == Vector2Int.down)
        {
            for (int y = row; y < bounds.yMax + 10; y++)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y + 1, 0);
                    TileBase above = tilemap.GetTile(tilePosition);

                    tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, above);
                }
            }
        }
        else
        {
            for (int y = bounds.yMax; y >= row; y--)
            {
                for (int x = bounds.xMin; x < bounds.xMax; x++)
                {
                    Vector3Int tilePosition = new Vector3Int(x, y - 1, 0);
                    TileBase below = tilemap.GetTile(tilePosition);

                    tilePosition = new Vector3Int(x, y, 0);
                    tilemap.SetTile(tilePosition, below);
                }
            }
        }
    }
    public void SpawnCheese()
    {
        int maxLines = 0;

        for (int i = 0; i < cheeseQueue; i++)
        {
            int gap = Random.Range(-5, 5);
            int lines = Random.Range(1, 5);

            if (lines > maxLines)
                maxLines = lines;

            for (int y = 0; y < lines; y++)
            {
                MoveAllLines(-11, Vector2Int.up);
                for (int x = -5; x < 5; x++)
                {
                    if (x != gap)
                        tilemap.SetTile(new Vector3Int(x, -11), cheeseTile);
                }
            }
        }

        sounds.PlaySlap();
        cheeseQueue = 0;
        cheeseDelay = level * maxLines;
        cheeseTime = Time.time + cheeseDelay;
    }

    public void RaiseLevel()
    {
        if (level < 1)
            return;

        level -= 0.5f;
        activePiece.stepDelay -= 0.1f;
        levelTime = Time.time + levelDelay;
    }

    public void GameOver()
    {
        string path = "data\\temp.txt";

        if (!Directory.Exists("data"))
            Directory.CreateDirectory("data");

        File.WriteAllText(path, score.ToString());
        SceneManager.LoadScene("GameOver");
        
    }

    public void GenerateBag(PieceData[] pieces)
    {
        for (int i = pieces.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            PieceData temp = pieces[i];
            pieces[i] = pieces[j];
            pieces[j] = temp;
        }
        foreach (PieceData piece in pieces)
        {
            bag.Add(piece);
        }
    }
}
