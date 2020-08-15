using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Skill : MonoBehaviour
{
    //技能公共变量
    #region
    //技能属性
    public dDamage selfDamage;
    public dDirection currentDir;
   
    //技能组件
    private BoxCollider2D collider;

    //技能动画
    protected Animator anim;
    protected AnimatorOverrideController animOverride;
    public AnimationClip[] list;
    #endregion

    //生命周期
    #region
    // 初始化数据
    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.collider = this.GetComponent<BoxCollider2D>();
        this.collider.enabled = false;
        this.animOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        this.anim.runtimeAnimatorController = this.animOverride;
        this.AnimSelect();
        this.anim.runtimeAnimatorController = animOverride;


    }
    //更新技能信息
    void Update()
    {
        if (!this.collider.enabled)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = this.anim.GetCurrentAnimatorStateInfo(0);
            if ((animatorInfo.normalizedTime >= 1.0f) && (animatorInfo.IsName("Born")))
            {

                this.collider.enabled = true;
            }
        }
    }

    #endregion

    //碰撞接口
    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CommonFunction.tagPlayer)
        {
            this.OnDamage();
        }
    }
    #endregion

    //内部方法
    #region
    //动画选择
    private void AnimSelect()
    {
        if (currentDir == dDirection.dDown_Down)
        {
            this.animOverride["0"] = this.list[0];
        }
        else if (currentDir == dDirection.dUp_Up)
        {
            this.animOverride["0"] = this.list[1];
        }
        else if (currentDir == dDirection.dLeft_Left)
        {
            this.animOverride["0"] = this.list[2];
        }
        else if (currentDir == dDirection.dRight_Right)
        {
            this.animOverride["0"] = this.list[2];
        }
        else if (currentDir == dDirection.dUp_Left)
        {
            this.animOverride["0"] = this.list[2];
        }
        else if (currentDir == dDirection.dUp_Right)
        {
            this.animOverride["0"] = this.list[2];
        }
        else if (currentDir == dDirection.dDown_Left)
        {
            this.animOverride["0"] = this.list[2];
        }
        else if (currentDir == dDirection.dDown_Right)
        {
            this.animOverride["0"] = this.list[2];
        }
    }

    //技能伤害判定
    void OnDamage()
    {
        Debug.Log("HA");
    }
    #endregion



}
