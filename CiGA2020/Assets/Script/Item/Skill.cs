using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public dDamage selfDamage;
    public dDirection currentDir;
    private Animator anim;
    private BoxCollider2D collider;


    // Start is called before the first frame update
    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.collider = this.GetComponent<BoxCollider2D>();
        this.collider.enabled = false;


    }

    void Update()
    {
        if (!this.collider.enabled)
        {
            AnimatorStateInfo animatorInfo;
            animatorInfo = anim.GetCurrentAnimatorStateInfo(0);
            if ((animatorInfo.normalizedTime >= 1.0f) && (animatorInfo.IsName("Born")))
            {
                this.collider.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == CommonFunction.Instance.tagPlayer)
        {
            this.OnDamage();
        }
    }

    void OnDamage()
    {
        Debug.Log("HA");
    }
}
