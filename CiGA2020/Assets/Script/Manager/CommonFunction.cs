﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommonFunction : Singleton<CommonFunction>
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
     public GameObject LoadSkill(dDirection dir)
    {
        GameObject tem = (GameObject)Resources.Load("Prefabs/crackPrefab");
        tem.GetComponent<Skill>().currentDir = dir;
        tem.GetComponent<SpriteRenderer>().sortingLayerName = "bg";
        return tem;
    }



}
