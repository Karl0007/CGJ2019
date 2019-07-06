using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

    public Sprite[] frames;
    public int lastFrameNo = 0;
    public bool destroy = false;
    public bool direction = true;
    public bool oneTime = false;
    public float speed = 1;

    private int index = 0;
    private int frameNumber = 3;

    private float myTime = 0;
    private int myIndex = 0;

    void Update()
    {
        frameNumber = frames.Length;

        if (!oneTime)
        {
            myTime += Time.deltaTime*(1/ speed);
            myIndex = (int)(myTime * (frameNumber - 1));
            index = myIndex % frameNumber;
        }
        //更换图片
        gameObject.GetComponent<SpriteRenderer>().sprite = frames[index];

        if (direction)
        {
            //设置tilling和offset属性
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else
        {
            //镜像效果
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

        }

        if (index == frameNumber - 1 && destroy)
        {
            Destroy(gameObject);
        }

        if (lastFrameNo != 0)
        {
            if (index == lastFrameNo - 1)
            {
                oneTime = true;
            }
        }
    }
}
