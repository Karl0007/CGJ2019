using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;

public class WillTool{

    public static GameObject GetChildInDepth(string name, GameObject rootGO)
    {
        Transform transform = rootGO.transform;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == name)
            {
                return transform.GetChild(i).gameObject;
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = GetChildInDepth(name, transform.GetChild(i).gameObject);
            if (go != null)
            {
                return go;
            }
        }
        return null;
    }

    public static T GetChildInDepth<T>(string name, Transform transform) where T : Component
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name == name)
            {
                T t = transform.GetChild(i).GetComponent<T>();
                if (t != null)
                {
                    return t;
                }

            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            T tDepth = GetChildInDepth<T>(name, transform.GetChild(i));
            if (tDepth != null)
            {
                return tDepth;
            }
        }

        return null;
    }

    public static void SetLayer(string name, GameObject go)
    {
        go.layer = LayerMask.NameToLayer(name);

        foreach (Transform tran in go.GetComponentsInChildren<Transform>())
        {//遍历当前物体及其所有子物体  
            tran.gameObject.layer = LayerMask.NameToLayer(name);//更改物体的Layer层  
        }
    }


   

    public static Texture2D CondenceTexture(Texture2D sourceTex, int constrain)
    {
        int width = (sourceTex.height > sourceTex.width) ? (sourceTex.width * constrain) / sourceTex.height : constrain;
        int height = (sourceTex.height > sourceTex.width) ? constrain : (sourceTex.height * constrain) / sourceTex.width;

        float pW = (float)width / (float)sourceTex.width;
        float pH = (float)height / (float)sourceTex.height;
        Texture2D nTex = new Texture2D((int)width, (int)height);
        for (int i = 0; i < sourceTex.height * sourceTex.width; i += (int)(1 / pW))
        {
            int y = Mathf.FloorToInt(((float)i) / ((float)sourceTex.width));
            int x = Mathf.FloorToInt(((float)i - ((float)(y * sourceTex.width))));

            int nX = Mathf.FloorToInt((((float)x) * ((float)pW)));
            int nY = Mathf.FloorToInt((((float)y) * ((float)pH)));

            Color c = sourceTex.GetPixel(x, y);

            nTex.SetPixel(nX, nY, c);
        }

        nTex.Apply();

        return nTex;
    }
    
    public static bool s_ShowMouse = true;

    public static Color RandomColor()
    {
        //随机颜色的RGB值。即刻得到一个随机的颜色
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Color color = new Color(r, g, b);
        return color;
    }

    public static void LookAtTarget(Transform transform,GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        Vector3 selfPos = transform.position;
        if (targetPos != selfPos)
        {//targetPos == selfPos, no where to look
            Vector3 projectV = Vector3.Dot(targetPos - selfPos, transform.up) * transform.up;//project to up-axis
            Vector3 lookDir = targetPos - selfPos - projectV;
            if (lookDir != Vector3.zero)
            {//lookDir == 0,target is up the head ,no where to look 
                Vector3 crossV = Vector3.Cross(lookDir, transform.forward).normalized;
                if (crossV != Vector3.zero)
                {//crossV == 0 , target is looking at ,no need to rotate
                    if (crossV == transform.up)
                    {
                        transform.Rotate(transform.up, -Vector3.Angle(lookDir, transform.forward), Space.World);
                    }
                    else {
                        transform.Rotate(transform.up, Vector3.Angle(lookDir, transform.forward), Space.World);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 字符串转换成向量坐标
    /// </summary>
    /// <param name="vectorString"></param>
    /// <returns></returns>
    public static Vector3 StringToVector3(string vectorString)
    {
        if (!vectorString.StartsWith("(") || !vectorString.EndsWith(")"))
        {
            return Vector3.zero;
        }

        string subString = vectorString.Substring(1, vectorString.Length - 2);

        string[] splitStrings = subString.Split(',');
        return new Vector3(float.Parse(splitStrings[0]), float.Parse(splitStrings[1]), float.Parse(splitStrings[2]));

    }
}
