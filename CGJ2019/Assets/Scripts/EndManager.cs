using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "H")
        {
            List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
            talkInfoList.Add(new TalkRoot.TalkInfo("", "", ""));
            talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "有些事情只是一厢情愿\n纵使历经千辛万苦\n终究你还是不明白", "head/woman"));
            TalkRoot.ShowTalkList(talkInfoList);
        }
        else if (collision.tag == "Ci")
        {
            List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
            talkInfoList.Add(new TalkRoot.TalkInfo("", "", ""));
            talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "汝之蜜糖，吾之砒霜\n你是第一个明白这个道理的人\n世间并不是非黑即白\n另一个想法之下便是另一个世界", "head/woman"));
            TalkRoot.ShowTalkList(talkInfoList);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
