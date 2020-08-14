using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //数据管理
    #region
    public float speed;
    public dDirection currentDir = dDirection.dNone;
    #endregion

    //生命周期
    #region
    // Update is called once per frame
    void Update()
    {
        this.Controller();
    }
    #endregion


    //内部方法
    #region
    //角色控制
    private void Controller()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentDir = dDirection.dUp;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentDir = dDirection.dDown;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentDir = dDirection.dLeft;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentDir = dDirection.dRight;
        }
        else
        {
            currentDir = dDirection.dNone;
        }
        this.Move(currentDir);
    }


    //角色的位移
    private void Move(dDirection dir)
    {
        Vector2 v = new Vector2();
        if(dir == dDirection.dNone)
        {
            v = new Vector2(0.0f, 0.0f);
        }
        else if (dir == dDirection.dUp)
        {
            v = new Vector2(0.0f, speed) * Time.deltaTime;
        }
        else if (dir == dDirection.dDown)
        {
            v = new Vector2(0.0f, -speed) * Time.deltaTime;
        }
        else if (dir == dDirection.dLeft)
        {
            v = new Vector2(-speed, 0.0f) * Time.deltaTime;
        }
        else if (dir == dDirection.dRight)
        {
            v = new Vector2(speed, 0.0f)*Time.deltaTime;
        }

        this.transform.Translate(v);
    }
    #endregion

}
