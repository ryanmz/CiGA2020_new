using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //数据管理
    #region
    //位移数据
    public float speed;
    public dDirection currentDir;

    //摔倒数据
    
    public float fallTime;
    public float fallSpeed;
    public float pauseFallTime;
    public float undmgTime;
    public bool fall;


    private float recordTime = 0.0f;
    #endregion

    public bool isPlayerOne = true;//判断1P还是2P

    public AnimationClip[] animListIdle;
    public AnimationClip[] animListRun;
    public AnimationClip[] animListAttack;
    public AnimationClip[] animListFall;
    protected Animator anim;
    protected AnimatorOverrideController animOverride;
    protected SpriteRenderer sprite;


    //生命周期
    #region
    void Start()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();

        this.anim = this.GetComponent<Animator>();
        this.animOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        this.anim.runtimeAnimatorController = this.animOverride;

    }
    // Update is called once per frame
    void Update()
    {
        if(isPlayerOne)
        {
            this.ControllerA();
        }
        else
        {
            this.ControllerB();
        }

    }
    #endregion


    //内部方法
    #region
    //角色控制
    private void ControllerA()
    {
        
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Run"+animStateInfo.IsName("Run"));
        Debug.Log("Idle"+animStateInfo.IsName("Idle"));
        Debug.Log("Attack" + animStateInfo.IsName("Attack"));
        Debug.Log("Fall"+animStateInfo.IsName("Fall"));
        if (!animStateInfo.IsName("Attack") && !animStateInfo.IsName("Fall"))
        {
            this.InputA();//角色动画输入

            this.AnimSetUp(this.currentDir);//角色动画选择
        }
        if (animStateInfo.IsName("Fall"))
        {
            this.Fall();
        }
    }

    private void ControllerB()
    {
        //this.FallAnim(this.fall);
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (!animStateInfo.IsName("Attack")&&!animStateInfo.IsName("Fall"))
        {
            this.InputB();//角色动画输入

            this.AnimSetUp(this.currentDir);//角色动画选择
        }
        else if (animStateInfo.IsName("Fall"))
        {
            this.Fall();
        }


    }

    //角色方向
    private void InputA()
    {
        Vector2 v = new Vector2();
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.AttackA(this.currentDir);
        }
        else
        {
            if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))//向直上
            {
                this.currentDir = dDirection.dUp_Up;
                v = new Vector2(0.0f, 1.0f);
            }
            else if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))//向直下
            {
                this.currentDir = dDirection.dDown_Down;
                v = new Vector2(0.0f, -1.0f);
            }
            else if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)))//向直左
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
            this.Move(v,this.speed);
            anim.SetFloat("Speed", v.magnitude);
        }

        if (this.fall)
        {
            anim.SetTrigger("Fall");
            this.fall = false;
        }
    }

    private void InputB()
    {
        Vector2 v = new Vector2();
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.AttackA(this.currentDir);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow) && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))//向直上
            {
                this.currentDir = dDirection.dUp_Up;
                v = new Vector2(0.0f, 1.0f);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))//向直下
            {
                this.currentDir = dDirection.dDown_Down;
                v = new Vector2(0.0f, -1.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)))//向直左
            {
                this.currentDir = dDirection.dLeft_Left;
                v = new Vector2(-1.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)))//向直右
            {
                this.currentDir = dDirection.dRight_Right;
                v = new Vector2(1.0f, 0.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))//向左上
            {
                this.currentDir = dDirection.dUp_Left;
                v = new Vector2(-1.0f, 1.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))//向右上
            {
                this.currentDir = dDirection.dUp_Right;
                v = new Vector2(1.0f, 1.0f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))//向左下
            {
                this.currentDir = dDirection.dDown_Left;
                v = new Vector2(-1.0f, -1.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))//向右下
            {
                this.currentDir = dDirection.dDown_Right;
                v = new Vector2(1.0f, -1.0f);
            }
            else
            {
                v = new Vector2(0.0f, 0.0f);
            }
            this.Move(v,this.speed);
            anim.SetFloat("Speed", v.magnitude);
        }
 
        if (this.fall)
        {
            anim.SetTrigger("Fall");
            this.fall = false;
        }
    }

    //角色攻击
    private void AttackA(dDirection dir)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.IsName("Run"))
                anim.SetFloat("Speed", 1.0f);
            else if (animStateInfo.IsName("Idle"))
                anim.SetFloat("Speed", 0.0f);

            anim.SetTrigger("Attack");
            Vector2 v = new Vector2(0, 0);
            this.Move(v,this.speed);
           
            this.GenerateCrack(this.transform, this.currentDir);
            return;

        }
    }

    private void AttackB(dDirection dir)
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.IsName("Run"))
                anim.SetFloat("Speed", 1.0f);
            else if (animStateInfo.IsName("Idle"))
                anim.SetFloat("Speed", 0.0f);

            anim.SetTrigger("Attack");
            Vector2 v = new Vector2(0, 0);
            this.Move(v,this.speed);


            anim.SetFloat("Speed", 0.1f);
            this.GenerateCrack(this.transform, this.currentDir);
            return;
        }
    }

    //角色的位移
    private void Move(Vector2 v,float tSpeed)
    {
        
        v = v.normalized * tSpeed * Time.deltaTime;
        this.transform.Translate(v);
        
    }

    //角色位移动画
    private void AnimSetUp(dDirection dir)
    {
        if (dir == dDirection.dUp_Up)
        {
            this.animOverride["Idle"] = this.animListIdle[0];
            this.animOverride["Run"] = this.animListRun[0];
            this.animOverride["Attack"] = this.animListAttack[0];
            this.animOverride["Fall"] = this.animListFall[0];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if(dir == dDirection.dUp_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.animOverride["Fall"] = this.animListFall[1];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dLeft_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.animOverride["Fall"] = this.animListFall[2];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.animOverride["Fall"] = this.animListFall[3];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Down)
        {
            this.animOverride["Idle"] = this.animListIdle[4];
            this.animOverride["Run"] = this.animListRun[4];
            this.animOverride["Attack"] = this.animListAttack[4];
            this.animOverride["Fall"] = this.animListFall[4];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.animOverride["Fall"] = this.animListFall[3];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dRight_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.animOverride["Fall"] = this.animListFall[2];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.animOverride["Fall"] = this.animListFall[1];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }

        this.anim.runtimeAnimatorController = animOverride;
    }
    #endregion

    //角色动画进行
    private void AnimHandler()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("Attack")&&animStateInfo.normalizedTime<1.0f)
        {
            Vector2 v = new Vector2(0.0f, 0.0f);
            this.Move(v,this.speed);
            
        }
    }

    //角色摔倒
    private void Fall()
    {
        this.recordTime += Time.deltaTime;
        if (this.recordTime >= this.fallTime)
        {
            Vector2 v = new Vector2(0.0f,0.0f);
            this.Move(v, this.fallSpeed);
            if (this.recordTime >= this.pauseFallTime)
            {
                anim.SetFloat("PauseTime", this.pauseFallTime);
                this.recordTime = 0.0f;
            }

        }
        else
        {
            Vector2 v = CommonFunction.Instance.TranslateDirection(this.currentDir);
            this.Move(v, this.fallSpeed);

        }
    }
    // 生成裂缝
    #region
    public void GenerateCrack(Transform _pos, dDirection _dir)
    {
        int cellSize = CommonFunction.Instance.cellSize;
        int offset = (int)cellSize / 2;
        int posX = (int)_pos.position.x;
        int posY = (int)_pos.position.y - offset;
        //Debug.Log(posX.ToString() + ", " + posY.ToString());

        int x = posX / cellSize, y = posY / cellSize;

        //Debug.Log("Cell: " + x + ", " + y);
        for (int i = 0; i < 3; i++)
        {
            if (i != 0)
            {
                StartCoroutine(Wait(CommonFunction.Instance.crackInterval));
            }
            switch (_dir)
            {
                case dDirection.dUp_Up:
                    y++; break;
                case dDirection.dUp_Left:
                    x--; y++; break;
                case dDirection.dUp_Right:
                    x++; y++; break;
                case dDirection.dDown_Down:
                    y--; break;
                case dDirection.dDown_Left:
                    x--; y--; break;
                case dDirection.dDown_Right:
                    x++; y--; break;
                case dDirection.dLeft_Left:
                    x--; break;
                case dDirection.dRight_Right:
                    x++; break;
                default:
                    Debug.Log("Crack with No Dir!");
                    break;
            }

            if (x < 0 || y < 0 || x >= CommonFunction.Instance.MapWidth || y >= CommonFunction.Instance.MapHeight)
            {
                break;
            }

            //if (GameManager.Instance.map[x, y] == dCellType.dCrack_1 || GameManager.Instance.map[x, y] == dCellType.dNone)
            //{
            //    GameManager.Instance.map[x, y]++;
            //}
            GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(_dir));
            crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);
        }
    }
    #endregion


    IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
    }

}
