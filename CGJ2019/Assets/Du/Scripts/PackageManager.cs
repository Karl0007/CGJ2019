using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PackageManager
{

    class ItemData
    {
        public int num;
        public string name;
        public string id;
    }

    private Dictionary<Item, ItemData> itemDic = new Dictionary<Item, ItemData>();
    public enum Item
    {
        apple,
        Axe,
        piqiu,
        zhurou,
        key,
    }


    public void Clear()
    {
        itemDic.Clear();
    }

    public void Add(Item i)
    {
        if (itemDic.ContainsKey(i))
        {
            itemDic[i].num += 1;
        }
        else
        {
            ItemData data = new ItemData();
            data.num = 1;
            data.name = "Item:" + i.ToString();
            itemDic[i] = data;
        }
    }

    public bool Delete(Item i)
    {
        if (itemDic.ContainsKey(i))
        {
            itemDic[i].num -= 1;

            if(itemDic[i].num < 0)
            {
                return false;
            }else
            {
                return true;
            }
            
        }else
        {
            return false;
        }
    }

    public bool Check(Item i)
    {
        if (itemDic.ContainsKey(i))
        {

            if (itemDic[i].num <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    public void Exchange(Item i1, Item i2)
    {
        Add(i2);
        Delete(i1);
    }


}
