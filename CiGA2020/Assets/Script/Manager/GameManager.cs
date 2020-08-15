using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // 可调整参数
    #region
    public int MapWidth = 30;               // 地图宽度（格子数）
    public int MapHeight = 16;              // 地图高度（格子数）
    public int cellSize = 64;               // 格子单位长度


    //public float crackInterval = 2f;      // 裂缝生成时间间隔
    #endregion

    public dCellType[,] map;
    public GameObject[,] obj;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
