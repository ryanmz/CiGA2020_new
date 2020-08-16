using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToCrack2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            int cellSize = CommonFunction.Instance.cellSize;
            int offset = (int)cellSize / 2;
            int x = (int)this.transform.position.x / cellSize;
            int y = (int)(this.transform.position.y - offset) / cellSize;

            dCellType[,] tMap = GameObject.Find("GameManager").GetComponent<GameManager>().map;
            GameObject[,] objects = GameObject.Find("GameManager").GetComponent<GameManager>().obj;

            tMap[x, y] = dCellType.dCrack_2;

            GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(dDirection.dNone, tMap[x, y]));
            crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);
            objects[x, y] = crack;

            Destroy(this.gameObject);
        
        }
    }
}
