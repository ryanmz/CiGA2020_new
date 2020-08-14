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


    private SpriteRenderer sprite;

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();

    }
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
        if (Input.GetKey(KeyCode.W)&& !(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)))//向直上
        {
            currentDir = dDirection.dUp_Up;
        }
        else if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))//向直下
        {
            currentDir = dDirection.dDown_Down;
        }
        else if (Input.GetKey(KeyCode.A)&& !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))//向直左
        {
            currentDir = dDirection.dLeft_Left;
        }
        else if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))//向直右
        {
            currentDir = dDirection.dRight_Right;
        }else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))//向左上
        {
            currentDir = dDirection.dUp_Left;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))//向右上
        {
            currentDir = dDirection.dUp_Right;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))//向左下
        {
            currentDir = dDirection.dDown_Left;
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))//向右下
        {
            currentDir = dDirection.dDown_Right;
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
        else if (dir == dDirection.dUp_Up)
        {
            v = new Vector2(0.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Down)
        {
            v = new Vector2(0.0f, -1.0f);
        }
        else if (dir == dDirection.dLeft_Left)
        {
            v = new Vector2(-1.0f, 0.0f);
        }
        else if (dir == dDirection.dRight_Right)
        {
            v = new Vector2(1.0f, 0.0f);
        }
        else if (dir == dDirection.dUp_Left)
        {
            v = new Vector2(-1.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Right)
        {
            v = new Vector2(1.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Left)
        {
            v = new Vector2(-1.0f, -1.0f);
        }
        else if (dir == dDirection.dDown_Right)
        {
            v = new Vector2(1.0f, -1.0f);
        }
        this.sprite.sprite = CommonFunction.Instance.LoadSprite(dir);
        v = v.normalized*speed;
        this.transform.Translate(v * Time.deltaTime);
    }
    #endregion

}
