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

    //攻击数据
    public bool isAttack = false;

    //摔倒数据
    public float fallTime;
    public float fallSpeed;
    public float pauseFallTime;
    public float undmgTime;
    public bool fall;


    //死亡数据
    public bool die = false;


    //复活数据
    public float respawnTime;



    public float recordTime = 0.0f;
    public float recordUndmg = 0.0f;
    public float recordRespawn = 0.0f;

    //放置物品
    public bool isPut = false;
    #endregion

    public bool isPlayerOne = true;//判断1P还是2P

    public AnimationClip[] animListIdle;
    public AnimationClip[] animListRun;
    public AnimationClip[] animListAttack;
    public AnimationClip[] animListFall;
    public AnimationClip[] animListPut;
    public AnimationClip[] animListDie;
    protected Animator anim;
    protected AnimatorOverrideController animOverride;
    protected SpriteRenderer sprite;

    //游戏管理器
    public int boxCollections = 0;
    public int frameCollections = 0;
    private GameManager gameManager;


    //生命周期
    #region
    void Start()
    {
        this.recordUndmg = this.undmgTime;
        this.sprite = this.GetComponent<SpriteRenderer>();

        this.anim = this.GetComponent<Animator>();
        this.animOverride = new AnimatorOverrideController(anim.runtimeAnimatorController);
        this.anim.runtimeAnimatorController = this.animOverride;

        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    // Update is called once per frame
    void Update()
    {
        if (isPlayerOne)
        {
            this.ControllerA();
        }
        else
        {
            this.ControllerB();
        }
        this.UndmgTime();


    }
    #endregion

    #region
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == CommonFunction.tagItem)
        {
            this.boxCollections++;
            //Debug.Log(this.boxCollections);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Frame")
        {
            this.frameCollections++;
            Destroy(collision.gameObject);
        }

    }
    #endregion


    #region
    //角色控制
    private void ControllerA()
    {

        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        Debug.Log("Put"+ animStateInfo.IsName("Put"));
       
        if (!animStateInfo.IsName("Attack") && !animStateInfo.IsName("Fall") && !animStateInfo.IsName("Die")&&!animStateInfo.IsName("Put"))
        {

            this.ResetFall();//重置摔倒

            this.InputA();//角色动画输入

            this.AnimSetUp(this.currentDir);//角色动画选择
        }
        else if (animStateInfo.IsName("Attack") && animStateInfo.normalizedTime > 0.4 && this.isAttack)
        {
            this.GenerateCrack(this.transform, this.currentDir);
            this.isAttack = false;
        }
        else if (animStateInfo.IsName("Put") && animStateInfo.normalizedTime > 0.4 && this.isPut)
        {
            Debug.Log("In");
            this.GenerateBox(this.transform, this.currentDir);
            this.isPut = false;
            this.boxCollections--;
        }
        else if (animStateInfo.IsName("Fall"))
        {
            this.Fall();
        }
        else if (animStateInfo.IsName("Die"))
        {
            this.Die();
        }
    }

    private void ControllerB()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);

        if (!animStateInfo.IsName("Attack") && !animStateInfo.IsName("Fall") && !animStateInfo.IsName("Die") && !animStateInfo.IsName("Put"))
        {


            this.ResetFall();//重置摔倒

            this.InputB();//角色动画输入

            this.AnimSetUp(this.currentDir);//角色动画选择
        }
        else if (animStateInfo.IsName("Attack") && animStateInfo.normalizedTime > 0.4 && this.isAttack)
        {
            this.GenerateCrack(this.transform, this.currentDir);
            this.isAttack = false;
        }
        else if (animStateInfo.IsName("Put") && animStateInfo.normalizedTime > 0.4 && this.isPut)
        {
            this.GenerateBox(this.transform, this.currentDir);
            this.isPut = false;
            this.boxCollections--;
        }
        else if (animStateInfo.IsName("Fall"))
        {
            this.Fall();
        }
        else if (animStateInfo.IsName("Die"))
        {
            this.Die();
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
        else if (Input.GetKey(KeyCode.Z) || Input.GetKeyDown(KeyCode.Z))
        {
            this.PutA();
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
            this.Move(v, this.speed);
            anim.SetFloat("Speed", v.magnitude);
        }

        if (this.fall)
        {
            this.anim.SetTrigger("Fall");
            this.fall = false;
        }
        if (this.die)
        {
            this.anim.SetBool("Die", true);
        }
    }

    private void InputB()
    {
        Vector2 v = new Vector2();
        if (Input.GetKey(KeyCode.J) || Input.GetKeyDown(KeyCode.J))
        {
            this.AttackB(this.currentDir);
        }
        else if(Input.GetKey(KeyCode.K) || Input.GetKeyDown(KeyCode.K))
        {
            this.PutB();
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
            this.Move(v, this.speed);
            anim.SetFloat("Speed", v.magnitude);
        }

        if (this.fall)
        {
            anim.SetTrigger("Fall");
            this.fall = false;
        }
        if (this.die)
        {
            this.anim.SetBool("Die", true);
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
            this.Move(v, this.speed);
            this.isAttack = true;
            //this.GenerateCrack(this.transform, this.currentDir);
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
            this.Move(v, this.speed);
            this.isAttack = true;
            //this.GenerateCrack(this.transform, this.currentDir);
            return;
        }
    }
    public void PutA()
    {
        if (this.boxCollections < 1)
            return;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.IsName("Run"))
                anim.SetFloat("Speed", 1.0f);
            else if (animStateInfo.IsName("Idle"))
                anim.SetFloat("Speed", 0.0f);

            anim.SetTrigger("Put");
            Vector2 v = new Vector2(0, 0);
            this.Move(v, this.speed);
            this.isPut = true;
            //this.GenerateCrack(this.transform, this.currentDir);
            return;
        }
    }

    public void PutB()
    {
        if (this.boxCollections < 1)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (animStateInfo.IsName("Run"))
                anim.SetFloat("Speed", 1.0f);
            else if (animStateInfo.IsName("Idle"))
                anim.SetFloat("Speed", 0.0f);

            anim.SetTrigger("Put");
            Vector2 v = new Vector2(0, 0);
            this.Move(v, this.speed);
            this.isPut = true;
            //this.GenerateCrack(this.transform, this.currentDir);
            return;
        }
    }

    //角色的位移
    private void Move(Vector2 v, float tSpeed)
    {
        //Debug.Log(this.transform.position);
        v = v.normalized * tSpeed * Time.deltaTime;
        this.transform.Translate(v);
        if (this.transform.position.x >= this.gameManager.MaxRange.x * this.gameManager.cellSize)
        {
            this.transform.position = new Vector3(this.gameManager.MaxRange.x * this.gameManager.cellSize, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y >= this.gameManager.MaxRange.y * this.gameManager.cellSize)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.gameManager.MaxRange.y * this.gameManager.cellSize, this.transform.position.z);
        }
        if (this.transform.position.x <= this.gameManager.MinRange.x * this.gameManager.cellSize)
        {
            this.transform.position = new Vector3(this.gameManager.MinRange.x * this.gameManager.cellSize, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.y <= this.gameManager.MinRange.y * this.gameManager.cellSize)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.gameManager.MinRange.y * this.gameManager.cellSize, this.transform.position.z);
        }

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
            this.animOverride["Put"] = this.animListPut[0];
            this.animOverride["Die"] = this.animListDie[0];
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.animOverride["Fall"] = this.animListFall[1];
            this.animOverride["Put"] = this.animListPut[1];
            this.animOverride["Die"] = this.animListDie[1];
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dLeft_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.animOverride["Fall"] = this.animListFall[2];
            this.animOverride["Put"] = this.animListPut[2];
            this.animOverride["Die"] = this.animListDie[2];
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Left)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.animOverride["Fall"] = this.animListFall[3];
            this.animOverride["Put"] = this.animListPut[3];
            this.animOverride["Die"] = this.animListDie[3];
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Down)
        {
            this.animOverride["Idle"] = this.animListIdle[4];
            this.animOverride["Run"] = this.animListRun[4];
            this.animOverride["Attack"] = this.animListAttack[4];
            this.animOverride["Fall"] = this.animListFall[4];
            this.animOverride["Put"] = this.animListPut[4];
            this.animOverride["Die"] = this.animListDie[4];
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dDown_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[3];
            this.animOverride["Run"] = this.animListRun[3];
            this.animOverride["Attack"] = this.animListAttack[3];
            this.animOverride["Fall"] = this.animListFall[3];
            this.animOverride["Put"] = this.animListPut[3];
            this.animOverride["Die"] = this.animListDie[3];
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dRight_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[2];
            this.animOverride["Run"] = this.animListRun[2];
            this.animOverride["Attack"] = this.animListAttack[2];
            this.animOverride["Fall"] = this.animListFall[2];
            this.animOverride["Put"] = this.animListPut[2];
            this.animOverride["Die"] = this.animListDie[2];
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Right)
        {
            this.animOverride["Idle"] = this.animListIdle[1];
            this.animOverride["Run"] = this.animListRun[1];
            this.animOverride["Attack"] = this.animListAttack[1];
            this.animOverride["Fall"] = this.animListFall[1];
            this.animOverride["Put"] = this.animListPut[1];
            this.animOverride["Die"] = this.animListDie[1];
            this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        this.anim.runtimeAnimatorController = animOverride;
    }
    #endregion

    //角色动画进行
    private void AnimHandler()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animStateInfo.IsName("Attack") && animStateInfo.normalizedTime < 1.0f)
        {
            Vector2 v = new Vector2(0.0f, 0.0f);
            this.Move(v, this.speed);

        }
    }

    //角色摔倒
    private void Fall()
    {
        this.recordTime += Time.deltaTime;
        if (this.recordTime >= this.fallTime)
        {
            Vector2 v = new Vector2(0.0f, 0.0f);
            this.Move(v, this.fallSpeed);
            if (this.recordTime >= this.pauseFallTime)
            {
                this.anim.SetFloat("PauseTime", this.pauseFallTime);
                this.recordUndmg = 0.0f;
                Debug.Log("Fall()Fall()Fall()Fall()Fall()Fall()Fall()");
                Debug.Log(this.recordUndmg);
            }

        }
        else
        {
            Vector2 v = CommonFunction.Instance.TranslateDirection(this.currentDir);
            this.Move(v, this.fallSpeed);

        }

    }

    private void ResetFall()
    {
        this.recordTime = 0.0f;
        this.anim.SetFloat("PauseTime", 0.0f);
    }

    //角色死亡
    private void Die()
    {

        if (this.recordRespawn >= this.respawnTime && this.die)
        {
            this.gameManager.Respawn(this.gameObject);
            this.die = false;
            this.anim.SetBool("Die", false);
            this.recordRespawn = 0.0f;
        }
        else
        {
            this.recordRespawn += Time.deltaTime;
        }
    }




    public void UndmgTime()
    {

        this.recordUndmg += Time.deltaTime;
        if (this.recordUndmg >= this.undmgTime)
        {
            this.recordUndmg = this.undmgTime;
        }
    }

    // 生成裂缝
    #region
    public void GenerateCrack(Transform _pos, dDirection _dir)
    {
        //int cellSize = CommonFunction.Instance.cellSize;
        //int offset = (int)cellSize / 2;
        //int posX = (int)_pos.position.x;
        //int posY = (int)_pos.position.y - offset;
        //int x = posX / cellSize, y = posY / cellSize;

        StartCoroutine(WaitForGenerate(_pos, _dir));
    }



    IEnumerator WaitForGenerate(Transform _pos, dDirection _dir)
    {
        int cellSize = CommonFunction.Instance.cellSize;
        int offset = (int)cellSize / 2;
        int x = (int)_pos.position.x / cellSize;
        int y = (int)(_pos.position.y - offset) / cellSize;

        for (int i = 0; i < 3; i++)
        {
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

            Debug.Log("pass");
            Debug.Log("Cell: " + x + ", " + y);
            if (x < 0 || y < 0 || x >= CommonFunction.Instance.MapWidth || y >= CommonFunction.Instance.MapHeight)
            {
                break;
            }

            //Debug.Log(x + ", " + y);
            dCellType[,] tMap = GameObject.Find("GameManager").GetComponent<GameManager>().map;
            GameObject[,] objects = GameObject.Find("GameManager").GetComponent<GameManager>().obj;

            if (tMap[x, y] != dCellType.dCrack_2)
            {
                if (tMap[x, y] == dCellType.dCrack_1 || tMap[x, y] == dCellType.dNone)
                {
                    tMap[x, y]++;
                }
                else
                {
                    tMap[x, y] = dCellType.dCrack_1;
                }
            }
            GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(_dir, tMap[x, y]));
            crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);

            //crack.GetComponent<Skill>().cellType = tMap[x, y];
            if (objects[x, y] == null)
            {
                objects[x, y] = crack;
            }
            else
            {
                Destroy(objects[x, y]);
                objects[x, y] = crack;
            }

            yield return new WaitForSeconds(CommonFunction.Instance.crackInterval);
        }
    }
    #endregion


    //生成箱子
    #region
    public void GenerateBox(Transform _pos, dDirection _dir)
    {
        int cellSize = CommonFunction.Instance.cellSize;
        int offset = (int)cellSize / 2;
        int x = (int)_pos.position.x / cellSize;
        int y = (int)(_pos.position.y - offset) / cellSize;


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

        Debug.Log("pass");
        Debug.Log("Cell: " + x + ", " + y);
        if (x < 0 || y < 0 || x >= CommonFunction.Instance.MapWidth || y >= CommonFunction.Instance.MapHeight)
        {
            return;
        }

        //Debug.Log(x + ", " + y);
        dCellType[,] tMap = GameObject.Find("GameManager").GetComponent<GameManager>().map;
        GameObject[,] objects = GameObject.Find("GameManager").GetComponent<GameManager>().obj;

        if (tMap[x, y] == dCellType.dCrack_2)
        {
            tMap[x, y] = dCellType.dBlock;
            Destroy(objects[x, y]);
            GameObject fill = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(_dir, tMap[x, y]));
            fill.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);

        }
        else if (tMap[x, y] == dCellType.dNone)
        {
            tMap[x, y] = dCellType.dItem;
            GameObject tObj = GameObject.Instantiate((GameObject)(Resources.Load("Prefabs/ItemPrefab")));
            if (objects[x, y] == null)
            {
                objects[x, y] = tObj;
            }
            tObj.transform.position = new Vector3(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2, 0);
        }

        //GameObject crack = GameObject.Instantiate(CommonFunction.Instance.LoadSkill(_dir, tMap[x, y]));
        //crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);

        #endregion


    }
}
