using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonFunction : MonoBehaviour
{
    static public string tagPlayer = "Player";

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
    static public GameObject LoadSkill(dDirection dir)
    {
        GameObject tem = (GameObject)Resources.Load("Prefabs/crackPrefab");
        tem.GetComponent<Skill>().currentDir = dir;
        tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
        return tem;
    }

    // 生成裂缝
    #region
    static public void GenerateCrack(Transform _pos, dDirection _dir)
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

            if (GameManager.Instance.map[x, y] == dCellType.dCrack_1 || GameManager.Instance.map[x, y] == dCellType.dNone)
            {
                GameManager.Instance.map[x, y]++;
            }
            GameObject crack = GameObject.Instantiate(LoadSkill(_dir));
            crack.transform.position = new Vector3(x * cellSize + offset, y * cellSize + offset, 0);
        }
    }
    #endregion
    IEnumerator Wait(float _time)
    {
        yield return new WaitForSeconds(_time);
    }


}
