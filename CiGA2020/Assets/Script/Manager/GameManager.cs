using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // 可调整参数
    #region
    public int MapWidth = 20;               // 地图宽度（格子数）
    public int MapHeight = 11;              // 地图高度（格子数）
    public int cellSize = 64;               // 格子单位长度

    public bool canGenerateItem1 = true;
    public float itemGenerate1Timer = 0;
    public float frameGenerateTimer = 0;


    public Vector2 MinRange;
    public Vector2 MaxRange;

    //public float crackInterval = 2f;      // 裂缝生成时间间隔
    #endregion

    public dCellType[,] map;
    public GameObject[,] obj;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0F;
        obj = new GameObject[MapWidth, MapHeight];
        map = new dCellType[MapWidth, MapHeight];
        for (int i = 0; i <MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                obj[i, j] = null;
                map[i, j] = dCellType.dNone;
            }
        }


        // new


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        itemGenerate1Timer += Time.deltaTime;
        if (itemGenerate1Timer >= CommonFunction.Instance.itemInterval)
        {
            this.GenerateItem2();
            itemGenerate1Timer = 0;
        }

        frameGenerateTimer += Time.deltaTime;
        if (frameGenerateTimer >= CommonFunction.Instance.frameInterval)
        {
            this.GenerateFrame();
            frameGenerateTimer = 0;
        }
    }


    void GenerateItem2()
    {

        dCellType tempT = dCellType.dBlock;
        int tx = 0, ty = 0;

        while (tempT != dCellType.dItem)
        {
            tx = Random.Range(0, MapWidth - 1);
            ty = Random.Range(0, MapHeight - 1);
            //Debug.Log(tx + ", " + ty);

            if (map[tx, ty] == dCellType.dNone)
            {
                tempT = dCellType.dItem;
            }
        }

        map[tx, ty] = dCellType.dItem;
        GameObject tObj = GameObject.Instantiate((GameObject)(Resources.Load("Prefabs/ItemPrefab")));
        if (obj[tx, ty] == null)
        {
            obj[tx, ty] = tObj;
        }
        tObj.transform.position = new Vector3(tx * cellSize + cellSize / 2, ty * cellSize + cellSize / 2, 0);

    }

    public void GenerateFrame()
    {
        dCellType tempT = dCellType.dBlock;
        int tx = 0, ty = 0;

        while (tempT != dCellType.dFrame)
        {
            tx = Random.Range(0, MapWidth - 1);
            ty = Random.Range(0, MapHeight - 1);
            //Debug.Log(tx + ", " + ty);

            if (map[tx, ty] == dCellType.dNone)
            {
                tempT = dCellType.dFrame;
            }
        }

        map[tx, ty] = dCellType.dFrame;
        GameObject tObj = GameObject.Instantiate((GameObject)(Resources.Load("Prefabs/FramePrefab")));
        if (obj[tx, ty] == null)
        {
            obj[tx, ty] = tObj;
        }
        tObj.transform.position = new Vector3(tx * cellSize + cellSize / 2, ty * cellSize + cellSize / 2, 0);

    }

    public void Respawn(GameObject player)
    {
        dCellType tempT = dCellType.dFrame;
        int tx = 0, ty = 0;

        while (tempT != dCellType.dNone)
        {
            tx = Random.Range(0, MapWidth - 1);
            ty = Random.Range(0, MapHeight - 1);
            //Debug.Log(tx + ", " + ty);

            if (map[tx, ty] == dCellType.dFrame)
            {
                tempT = dCellType.dFrame;
            }
            else if (map[tx, ty] == dCellType.dBlock)
            {
                tempT = dCellType.dBlock;
            }
            else if (map[tx, ty] == dCellType.dCrack_1)
            {
                tempT = dCellType.dCrack_1;
            }
            else if (map[tx, ty] == dCellType.dCrack_2)
            {
                tempT = dCellType.dCrack_2;
            }
            else if (map[tx, ty] == dCellType.dItem)
            {
                tempT = dCellType.dItem;
            }
            else if (map[tx, ty] == dCellType.dNone)
            {
                tempT = dCellType.dNone;
            }
        }


        player.transform.position = new Vector3(tx * cellSize + cellSize / 2, ty * cellSize + cellSize / 2, 0);
    }
}
