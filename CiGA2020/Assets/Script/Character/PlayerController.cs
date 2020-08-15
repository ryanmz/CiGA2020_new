using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //数据管理
    #region
    public float speed;
    public dDirection currentDir;
    #endregion

    public bool isPlayerOne = true;//判断1P还是2P

    public AnimationClip[] animListIdle;
    public AnimationClip[] animListRun;
    public AnimationClip[] animListAttack;
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
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.AttackA(this.currentDir);
        }
        else
        {
            this.DirectA();
            this.Anim(this.currentDir);
        }
    }

    private void ControllerB()
    {
        if (Input.GetKey(KeyCode.J) || Input.GetKeyDown(KeyCode.J))
        {
            this.AttackB(this.currentDir);
        }
        else
        {
            this.DirectB();
            this.Anim(this.currentDir);
        }
    }

    //角色方向
    private void DirectA()
    {
            Vector2 v = new Vector2();
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
            this.Move(v, this.currentDir);
    }

    private void DirectB()
    {
        Vector2 v = new Vector2();
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
        this.Move(v, this.currentDir);
    }

    //角色攻击
    private void AttackA(dDirection dir)
    {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {

                anim.SetTrigger("Attack");
                Vector2 v = new Vector2(0, 0);
                this.Move(v, this.currentDir);

            this.GenerateCrack(this.transform, this.currentDir);
            return;

            }
    }

    private void AttackB(dDirection dir)
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            /*
            GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(dir));

            crack.transform.position = this.transform.position;
            */


            anim.SetTrigger("Attack");
            Vector2 v = new Vector2(0, 0);
            this.Move(v, this.currentDir);

            this.GenerateCrack(this.transform, this.currentDir);
            return;
        }
    }

    //角色的位移
    private void Move(Vector2 v,dDirection dir)
    {
        
        v = v.normalized * speed* Time.deltaTime;
        this.transform.Translate(v);
        anim.SetFloat("Speed", v.magnitude);
    }

    //角色动画
    private void Anim(dDirection dir)
    {
        if (dir == dDirection.dUp_Up)
        {
            this.animOverride["Idle"] = this.animListIdle[0];
            this.animOverride["Run"] = this.animListRun[0];
            this.animOverride["Attack"] = this.animListAttack[0];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if(dir == dDirection.dUp_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dLeft_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Down)
        {
            this.animOverride["Idle"] = this.animListIdle[4];
            this.animOverride["Run"] = this.animListRun[4];
            this.animOverride["Attack"] = this.animListAttack[4];
            this.transform.localScale = new Vector3(50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dRight_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.transform.localScale = new Vector3(-50.0f, 50.0f, 1.0f);
        }

        this.anim.runtimeAnimatorController = animOverride;
    }
    #endregion



    // 生成裂缝
    #region
    public void GenerateCrack(Transform _pos, dDirection _dir)
    {
        int cellSize = GameManager.Instance.cellSize;
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
                StartCoroutine(Wait(GameManager.Instance.crackInterval));
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

            if (x < 0 || y < 0 || x >= GameManager.Instance.MapWidth || y >= GameManager.Instance.MapHeight)
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
