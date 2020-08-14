using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 改变遮挡关系
    #region
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger!");
        if (collision.transform.position.y < this.transform.position.y)
        {
            collision.GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sortingOrder = 1;
            collision.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
    #endregion
}
