using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivePiece : MonoBehaviour
{
    public Board board { get; protected set; }
    public HeldGrid heldGrid { get; private set; }
    public PieceData data { get; protected set; }
    public Vector3Int[] cells { get; protected set; }
    public Vector3Int position { get; protected set; }
    public int rotation = 0;
    public MoveType prevMove;
    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public bool held = false;
    public float stepDelay = 1f;
    public float moveDelay = 0.1f;
    public float softDropDelay = 0.1f;
    public float lockDelay = 0.5f;
    public float SDF;
    public float ARR;
    public float DAS;
    public float stepTime;
    public float moveTime;
    public float lockTime;
    public float softDropTime;
    private float heldTime = 0f;
    private float holdDelay = 2f;
    public Sounds sounds;

    public void Initialize(Board board, Vector3Int position, PieceData data)
    {
        this.board = board;
        this.data = data;
        this.position = position;

        rotation = 0;
        stepTime = Time.time + stepDelay;
        moveTime = Time.time + moveDelay;
        lockTime = 0f;

        if (cells == null)
            cells = new Vector3Int[data.cells.Length];

        for (int i = 0; i < data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)data.cells[i];
        }
    }
    private void Start()
    {
        heldGrid = GetComponent<HeldGrid>();
        sounds = GetComponent<Sounds>();
        keys.Add("LeftInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftInput", "LeftArrow")));
        keys.Add("RightInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightInput", "RightArrow")));
        keys.Add("SoftDropInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SoftDropInput", "DownArrow")));
        keys.Add("HardDropInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("HardDropInput", "C")));
        keys.Add("RotateRightInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateRightInput", "UpArrow")));
        keys.Add("RotateLeftInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RotateLeftInput", "X")));
        keys.Add("Rotate180Input", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Rotate180Input", "Z")));
        keys.Add("SwapHeldInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("SwapHeldInput", "LeftShift")));
        keys.Add("ForfeitInput", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForfeitInput", "Escape")));
        SDF = PlayerPrefs.GetFloat("SDF");
        ARR = PlayerPrefs.GetFloat("ARR");
        DAS = PlayerPrefs.GetFloat("DAS");
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(keys["HardDropInput"]))
        {
            HardDrop();
        }
        else if (Input.GetKeyDown(keys["RotateRightInput"]))
        {
            Rotate(1);
        }
        else if (Input.GetKeyDown(keys["RotateLeftInput"]))
        {
            Rotate(-1);
        }
        else if (Input.GetKeyDown(keys["Rotate180Input"]))
        {
            Rotate(2);
        }
        else if (Input.GetKeyDown(keys["SwapHeldInput"]) && !held)
        {
            if (heldGrid.empty == true)
            {
                board.nextGrid.Clear();
                Hold(board.bag[0]);
                board.bag.RemoveAt(0);
                if (board.bag.Count < board.pieces.Length)
                {
                    board.GenerateBag(board.pieces);
                }
                board.nextGrid.Load();
            }
            else
            {
                heldGrid.Clear();
                Hold(heldGrid.pieceData);
            }

        }
        else if (Input.GetKeyDown(keys["ForfeitInput"]))
        {
            heldTime = Time.time + holdDelay;
        }

        if (Input.GetKey(keys["ForfeitInput"]))
        {
            if (Time.time > heldTime)
                board.GameOver();
        }

        if (Input.GetKeyUp(keys["ForfeitInput"]))
        {
            heldTime = heldTime = Time.time + holdDelay;
        }

        if (Input.GetKeyDown(keys["RightInput"]))
        {
            MoveLR(Vector2Int.right, MoveType.LR);
            moveTime = Time.time + (DAS / 1000);
        }
        if (Input.GetKeyDown(keys["LeftInput"]))
        {
            MoveLR(Vector2Int.left, MoveType.LR);
            moveTime = Time.time + (DAS / 1000);
        }

        if (Input.GetKey(keys["RightInput"]))
        {
            if (Time.time > moveTime)
            {
                MoveLR(Vector2Int.right, MoveType.LR);
            }

        }

        if (Input.GetKey(keys["LeftInput"]))
        {
            if (Time.time > moveTime)
            {
                MoveLR(Vector2Int.left, MoveType.LR);
            }
        }

        if (Input.GetKey(keys["SoftDropInput"]))
        {
            if (Time.time > softDropTime)
            {
                SoftDrop();
            }
        }
    }
    public bool MoveLR(Vector2Int translation, MoveType type)
    {
        Vector3Int newPosition = position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        if (board.PositionValid(this, newPosition))
        {
            position = newPosition;

            if (type == MoveType.LR)
            {
                moveTime = Time.time + (moveDelay / ARR);
                sounds.PlayClick();
            }
            else if (type == MoveType.SoftDrop)
            {
                softDropTime = Time.time + (softDropDelay / SDF);
            }
            
            lockTime = 0f;

            return true;
        }

        return false;

    }
    void Rotate(int rotateBy)
    {
        int[] fromTo = new int[2];
        fromTo[0] = rotation;
        int newIndex = (rotation + rotateBy) % 4;
        rotation = newIndex < 0 ? 3 : newIndex;
        fromTo[1] = rotation;

        for (int i = 0; i < 4; i++)
        {
            this.cells[i] = (Vector3Int)RotationData.Cells[this.data.type][rotation][i];
        }

        if (!TestWallKicks(fromTo))
        {
            for (int i = 0; i < 4; i++)
            {
                rotation = fromTo[0];
                this.cells[i] = (Vector3Int)RotationData.Cells[this.data.type][rotation][i];
            }
        }
        else
        {
            sounds.PlaySnap();
            prevMove = MoveType.Rotate;
        }
    }
    public bool TestWallKicks(int[] fromTo)
    {
        int wallKickIndex;

        if (fromTo[0] == 0 && fromTo[1] == 1)
            wallKickIndex = 0;
        else if (fromTo[0] == 1 && fromTo[1] == 0)
            wallKickIndex = 1;
        else if (fromTo[0] == 1 && fromTo[1] == 2)
            wallKickIndex = 2;
        else if (fromTo[0] == 2 && fromTo[1] == 1)
            wallKickIndex = 3;
        else if (fromTo[0] == 2 && fromTo[1] == 3)
            wallKickIndex = 4;
        else if (fromTo[0] == 3 && fromTo[1] == 2)
            wallKickIndex = 5;
        else if (fromTo[0] == 3 && fromTo[1] == 0)
            wallKickIndex = 6;
        else if (fromTo[0] == 0 && fromTo[1] == 3)
            wallKickIndex = 7;
        else if (fromTo[0] == 0 && fromTo[1] == 2)
            wallKickIndex = 8;
        else if (fromTo[0] == 2 && fromTo[1] == 0)
            wallKickIndex = 9;
        else if (fromTo[0] == 1 && fromTo[1] == 3)
            wallKickIndex = 10;
        else if (fromTo[0] == 3 && fromTo[1] == 1)
            wallKickIndex = 11;
        else
            wallKickIndex = -1;

        if (wallKickIndex == -1)
        {
            return board.PositionValid(this, position);
        }
        if (wallKickIndex > 7)
        {
            wallKickIndex -= 8;
            for (int i = 0; i < 6; i++)
            {
                Vector2Int translation = RotationData.WallKicks180[wallKickIndex, i];

                if (MoveLR(translation, MoveType.Rotate))
                {
                    return true;
                }
            }
        }

        if (data.type == Type.I)
            for (int i = 0; i < 5; i++)
            {
                Vector2Int translation = RotationData.WallKicksI[wallKickIndex, i];

                if (MoveLR(translation , MoveType.Rotate))
                {
                    return true;
                }
            }
        else
            for (int i = 0; i < 5; i++)
            {
                Vector2Int translation = RotationData.WallKicks[wallKickIndex, i];

                if (MoveLR(translation, MoveType.Rotate))
                {
                    return true;
                }
            }
        return false;
    }

    public void Hold(PieceData nextPiece)
    {
        heldGrid.Load(board, data);
        data = nextPiece;
        position = board.spawnPosition;
        
        for (int i = 0; i < data.cells.Length; i++)
        {
            cells[i] = (Vector3Int)data.cells[i];
        }

        held = true;
        stepTime = Time.time + stepDelay;
        moveTime = Time.time + moveDelay;
        lockTime = 0f;
        rotation = 0;
        sounds.PlayBreak();
        board.Set(this);
    }
    public void SoftDrop()
    {
        if (MoveLR(Vector2Int.down, MoveType.SoftDrop))
        {
            stepTime = Time.time + stepDelay;
        }
    }
    public void HardDrop()
    {
        while (MoveLR(Vector2Int.down, MoveType.HardDrop))
        {
            prevMove = MoveType.HardDrop;
            continue;
        }

        Lock();
    }
    public void Step()
    {
        stepTime = Time.time + stepDelay;
        if (MoveLR(Vector2Int.down, MoveType.Step))
            prevMove = MoveType.Step;

        if (lockTime >= lockDelay)
            Lock();
    }
    public void Lock()
    {
        held = false;
        rotation = 0;
        board.nextGrid.Clear();
        sounds.PlayThump();
        board.Set(this);
        board.ClearLines();
        board.Spawn();
        board.nextGrid.Load();
    }
    public enum MoveType
    {
        None,
        LR,
        Rotate,
        Step,
        HardDrop,
        SoftDrop
    }
}
