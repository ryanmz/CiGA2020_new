using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack1To0 : MonoBehaviour
{
    float timeToDie = 4.0f;
    float liveTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        liveTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        liveTime += Time.deltaTime;
        if (liveTime >= timeToDie)
        {
            int cellSize = CommonFunction.Instance.cellSize;
            int offset = (int)cellSize / 2;
            int x = (int)this.transform.position.x / cellSize;
            int y = (int)(this.transform.position.y - offset) / cellSize;

            dCellType[,] tMap = GameObject.Find("GameManager").GetComponent<GameManager>().map;
            GameObject[,] objects = GameObject.Find("GameManager").GetComponent<GameManager>().obj;

            tMap[x, y] = dCellType.dNone;

            objects[x, y] = null;

            Destroy(this.gameObject);
        }
    }
}
