using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//自定义比较器，通过 Transform.position.y 给 List 排序
public class StageObjectComparer : IComparer<ZSortObject>
{
    public int Compare(ZSortObject a, ZSortObject b)
    {
        if (a.transform.position.y - b.transform.position.y > 0f)
        {
            return 1;
        }
        else if (a.transform.position.y - b.transform.position.y < 0f)
        {
            return -1;
        }
        else
        {
            //曾经被这里坑成狗。。。
            return 0;
        }
    }
}

//主类 ZSortObject ， 场景内所有挂载了该脚本的2D物体 都将按照y轴位置 自动调整遮挡关系，遮挡关系通过z轴位置来调整
public class ZSortObject : MonoBehaviour
{

    public static List<ZSortObject> StageObjectList = new List<ZSortObject>(); //存放所有需要排序的物体，注意是静态的
    //public float StageZGap = 0.01f; //两个相邻物体之间的z轴距离
    //public float StageZMin = 0f; //最靠前的物体的z轴位置
    public float SortRant = 0.03f; //每隔多久排序一次

    private static StageObjectComparer Soc = new StageObjectComparer(); //排序器的实例化
    private float SortTime = 0f; //距离下次排序的时间

    void OnEnable()
    {
        //每次恢复意识都把自己写进 List
        if (!StageObjectList.Contains(this))
        {
            StageObjectList.Add(this);
        }
    }

    void OnDisabled()
    {
        //每次失去意识都把自己踢出 List
        StageObjectList.Remove(this);
    }

    void Update()
    {

        //如果需要执行排序（列表里有物体 且当前实例是列表的第一个物体）
        if (StageObjectList.Count > 0 && StageObjectList[0] == this)
        {
            //如果到了该排序的时间
            if (SortTime < 0f)
            {
                SortObjects();
                SortTime += SortRant; //不要写成 SortTime = SortRant，会扔掉小于0的部分
            }
            else
            {
                SortTime -= Time.deltaTime;
            }
        }
    }

    //每次执行这个函数，都会将场景中所有的物体排序一次
    void SortObjects()
    {

        //用List类的自定义排序方法
        ZSortObject.StageObjectList.Sort(Soc);

        int _count = ZSortObject.StageObjectList.Count;

        for (int i = 0; i < _count; i++)
        {
            ZSortObject.StageObjectList[i].GetComponent<SpriteRenderer>().sortingOrder = _count - i;
            //让所有注册到 List 中的物体的 position.z 按照它在List中的顺序排列
            /*
            ZSortObject.StageObjectList[i].transform.Translate(
            0f, 0f,
            //Lerp 是个好东西 ~(ˉ▽￣～)
            Mathf.Lerp(
            StageZMin,
            StageZMin + StageZGap * _count,
            (float)i / (float)_count) - ZSortObject.StageObjectList[i].transform.position.z
            );
            */
        }
    }
}
