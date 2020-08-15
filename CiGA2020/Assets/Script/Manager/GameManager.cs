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

    public bool canGenerateItem1 = true;
    public float itemGenerate1Timer = 0;


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


        // new


    }

    // Update is called once per frame
    void Update()
    {
        if(canGenerateItem1)
        {
            this.GenerateItem2();
            canGenerateItem1 = false;
        }
        else
        {
            itemGenerate1Timer += Time.deltaTime;
            if(itemGenerate1Timer>=CommonFunction.Instance.itemInterval)
            {
                canGenerateItem1 = true;
                itemGenerate1Timer = 0;
            }
        }
    }

    // 生成道具
    #region
    //public void GenerateItem()
    //{
    //    Debug.Log("In_1");
    //    StartCoroutine(WaitForItem());
    //}

    //IEnumerator WaitForItem()
    //{
    //    dCellType tempT = dCellType.dBlock;
    //    int tx = 0, ty = 0;

    //    Debug.Log("In_2");
    //    while (true)
    //    {
    //        while (tempT != dCellType.dItem)
    //        {
    //            tx = Random.Range(0, MapWidth - 1);
    //            ty = Random.Range(0, MapHeight - 1);
    //            Debug.Log(tx + ", " + ty);

    //            if (map[tx, ty] == dCellType.dNone)
    //            {
    //                tempT = map[tx, ty];
    //            }
    //        }

    //        map[tx, ty] = dCellType.dItem;
    //        GameObject tObj = GameObject.Instantiate((GameObject)(Resources.Load("Prefab/ItemPrefab")));
    //        if (obj[tx, ty] == null)
    //        {
    //            obj[tx, ty] = tObj;
    //        }
    //        tObj.transform.position = new Vector3(tx * cellSize + cellSize / 2, ty * cellSize + cellSize / 2, 0);


    //        yield return new WaitForSeconds(CommonFunction.Instance.itemInterval);
    //    }
    //}

    void GenerateItem2()
    {

        dCellType tempT = dCellType.dBlock;
        int tx = 0, ty = 0;

        while (tempT != dCellType.dItem)
        {
            tx = Random.Range(0, MapWidth - 1);
            ty = Random.Range(0, MapHeight - 1);
            Debug.Log(tx + ", " + ty);

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
    #endregion

}
