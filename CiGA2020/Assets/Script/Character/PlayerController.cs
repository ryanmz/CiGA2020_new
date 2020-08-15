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
        this.ControllerA();

    }
    #endregion


    //内部方法
    #region
    //角色控制
    private void ControllerA()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.AttackA(this.currentDir);
        }
        else
        {
            this.DirectA();
        }
    }

    //角色方向
    private void DirectA()
    {
        Vector2 v = new Vector2();
        if (Input.GetKey(KeyCode.W)&& !(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)))//向直上
        {
            this.currentDir = dDirection.dUp_Up;
            v = new Vector2(0.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))//向直下
        {
            this.currentDir = dDirection.dDown_Down;
            v = new Vector2(0.0f, -1.0f);
        }
        else if (Input.GetKey(KeyCode.A)&& !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))//向直左
        {
            this.currentDir = dDirection.dLeft_Left;
            v = new Vector2(-1.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))//向直右
        {
            this.currentDir = dDirection.dRight_Right;
            v = new Vector2(1.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))//向左上
        {
            this.currentDir = dDirection.dUp_Left;
            v = new Vector2(-1.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))//向右上
        {
            this.currentDir = dDirection.dUp_Right;
            v = new Vector2(1.0f, 1.0f);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))//向左下
        {
            this.currentDir = dDirection.dDown_Left;
            v = new Vector2(-1.0f, -1.0f);
        }
        else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))//向右下
        {
            this.currentDir = dDirection.dDown_Right;
            v = new Vector2(1.0f, -1.0f);
        }
        else
        {
            v = new Vector2(0.0f, 0.0f);
        }
        this.Move(v,this.currentDir);
    }

    //角色攻击
    private void AttackA(dDirection dir)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            /*
            GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(dir));
            
            crack.transform.position = this.transform.position;
            */

            Vector2 v = new Vector2(0, 0);
            this.Move(v, this.currentDir);

            CommonFunction.Instance.GenerateCrack(this.transform, this.currentDir);
            return;
            
        }
    }

    //角色的位移
    private void Move(Vector2 v,dDirection dir)
    {
        this.sprite.sprite = CommonFunction.Instance.LoadSprite(dir);
        v = v.normalized * speed;
        this.transform.Translate(v * Time.deltaTime);
    }
    #endregion

}
