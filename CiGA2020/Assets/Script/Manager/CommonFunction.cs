using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonFunction : Singleton<CommonFunction>
{
    static public string tagPlayer = "Player";
    static public string tagItem = "Item";
    public int MapWidth = 20;               // 地图宽度（格子数）
    public int MapHeight = 11;              // 地图高度（格子数）
    public int cellSize = 64;               // 格子单位长度


    public float crackInterval = 0.5f;      // 裂缝生成时间间隔
    public float itemInterval = 5.0f;
    public float frameInterval = 10.0f;

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

    // 载入裂缝
    public GameObject LoadSkill(dDirection dir, dCellType _t)
    {
        GameObject tem;
        if (_t == dCellType.dCrack_1)
        {
            tem = (GameObject)Resources.Load("Prefabs/crackPrefab");
            tem.GetComponent<Skill>().currentDir = dir;
            tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
            return tem;
        }
        else if (_t == dCellType.dCrack_2)
        {
            tem = (GameObject)Resources.Load("Prefabs/crackPrefab_2");
            tem.GetComponent<Skill>().currentDir = dir;
            tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
            return tem;
        }else if(_t == dCellType.dBlock)
        {
            tem = (GameObject)Resources.Load("Prefabs/crackPrefab_3");
            tem.GetComponent<Skill>().currentDir = dir;
            tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
            return tem;
        }
        else
        {
            return null;
        }
    }


    public Vector2 TranslateDirection(dDirection dir)
    {
        Vector2 v = new Vector2();
        if (dir == dDirection.dDown_Down)
        {
            v = new Vector2(0.0f, -1.0f);
        }
        else if (dir == dDirection.dDown_Left)
        {
            v = new Vector2(-1.0f, -1.0f);
        }
        else if (dir == dDirection.dLeft_Left)
        {
            v = new Vector2(-1.0f, 0.0f);
        }
        else if (dir == dDirection.dUp_Left)
        {
            v = new Vector2(-1.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Up)
        {
            v = new Vector2(0.0f, 1.0f);
        }
        else if (dir == dDirection.dUp_Right)
        {
            v = new Vector2(1.0f, 1.0f);
        }
        else if (dir == dDirection.dRight_Right)
        {
            v = new Vector2(1.0f, 0.0f);
        }
        else if (dir == dDirection.dDown_Right)
        {
            v = new Vector2(1.0f, -1.0f);
        }
        return v.normalized;
    }



    public void MainMenu()
    {
        Application.LoadLevel(0);
    }

    public void TutorialScene()
    {
        Application.LoadLevel(1);
    }

    public void CreditScene()
    {
        Application.LoadLevel(2);
    }
    public void ReStart()
    {
        Application.LoadLevel(3);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
