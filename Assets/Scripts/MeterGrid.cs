using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeterGrid : MonoBehaviour
{
    public int fill = 0;
    private int maxFill = 26;
    public Tile tile;
    public Tilemap tilemap { get; private set; }
    public bool coolDown = false;
    private float coolDownDelay = 0.5f;
    private float coolDownTime;
    public Sounds sounds;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }
    private void Update()
    {
        if (coolDown)
            CoolDown();
    }
    public void Fill(int damage)
    {
        if (coolDown)
            return;

        fill += damage;
        if (fill >= maxFill)
        {
            coolDownTime = Time.time + coolDownDelay;
            coolDown = true;
            fill = maxFill;
            sounds.PlayStun();
        }

        Vector3Int position = new Vector3Int(0, 0);
        for (int i = 0; i < fill; i++)
        {
            tilemap.SetTile(position, tile);
            position += (Vector3Int)Vector2Int.up;
        }
    }
    public void Stun()
    {
        coolDownTime = Time.time + coolDownDelay;
        coolDown = true;
    }
    public void CoolDown()
    {
        Vector3Int position = new Vector3Int(0, fill);

        if (Time.time > coolDownTime)
        {
            tilemap.SetTile(position, null);
            coolDownTime = Time.time + coolDownDelay;
            fill--;
        }

        if (fill < 0)
            coolDown = false;
    }
}
