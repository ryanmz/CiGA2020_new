using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunction : Singleton<CommonFunction>
{

    // 改变遮挡关系
    #region
    public void ChangeOrder(Collider2D self, Collider2D other)
    {
        if (other.transform.position.y < self.transform.position.y)
        {
            other.GetComponent<SpriteRenderer>().sortingOrder = 1;
            self.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            self.GetComponent<SpriteRenderer>().sortingOrder = 1;
            other.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

    }
    #endregion
}
