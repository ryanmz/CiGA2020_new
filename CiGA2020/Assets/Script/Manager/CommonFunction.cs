using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunction : Singleton<CommonFunction>
{
    public string tagPlayer = "Player";

    // 可调整参数
    #region
    public int MapWidth = 30;               // 地图宽度（格子数）
    public int MapHeight = 16;              // 地图高度（格子数）
    public int cellSize = 64;               // 格子单位长度


    public float crackInterval = 2f;      // 裂缝生成时间间隔
    #endregion

    public Sprite LoadSprite(dDirection dir)
    {
        if (dir == dDirection.dNone)
        {
           return Resources.Load("Character/Idle/hero_idle_left", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Up)
        {
            return Resources.Load("Character/Idle/hero_idle_back", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Down)
        {
            return Resources.Load("Character/Idle/hero_idle_front", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dLeft_Left)
        {
            return Resources.Load("Character/Idle/hero_idle_left", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dRight_Right)
        {
            return Resources.Load("Character/Idle/hero_idle_left", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Left)
        {
            return Resources.Load("Character/Idle/hero_idle_topleft", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Right)
        {
            return Resources.Load("Character/Idle/hero_idle_topleft", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Left)
        {
            return Resources.Load("Character/Idle/hero_idle_bottomleft", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Right)
        {
            return Resources.Load("Character/Idle/hero_idle_bottomleft", typeof(Sprite)) as Sprite;
        }
        return null;
    }

    // *需要增加二级裂缝
    public GameObject LoadSkill(dDirection dir)
    {
        GameObject tem = (GameObject)Resources.Load("Prefabs/crackPrefab");
        tem.GetComponent<Skill>().currentDir = dir;
        tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
        return tem;
    }

    // 生成裂缝
    #region
    public void GenerateCrack(Transform _pos, dDirection _dir)
    {
        int offset = (int)cellSize / 2;
        int posX = (int)_pos.position.x;
        int posY = (int)_pos.position.y - offset;
        Debug.Log(posX.ToString() + ", " + posY.ToString());

        int x = posX / cellSize, y = posY / cellSize;

        Debug.Log("Cell: " + x + ", " + y);
        for (int i = 0; i < 3; i++)
        {
            if (i != 0)
            {
                this.Wait(crackInterval);
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

            if (x < 0 || y < 0 || x >= MapWidth || y >= MapHeight)
            {
                break;
            }

            GameObject crack = GameObject.Instantiate(this.LoadSkill(_dir));
            crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);
        }
    }
    #endregion

    IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
    }
}
