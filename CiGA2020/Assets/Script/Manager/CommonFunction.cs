using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonFunction : Singleton<CommonFunction>
{
    public string tagPlayer = "Player";
    

    public Sprite LoadSprite(dDirection dir)
    {
        if (dir == dDirection.dNone)
        {
           return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Up)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Down)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dLeft_Left)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dRight_Right)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Left)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dUp_Right)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Left)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        else if (dir == dDirection.dDown_Right)
        {
            return Resources.Load("Temp/map1", typeof(Sprite)) as Sprite;
        }
        return null;
    }

    public GameObject LoadSkill(dDirection dir)
    {
        GameObject tem = (GameObject)Resources.Load("Prefabs/crackPrefab");
        return tem;
    }



}
