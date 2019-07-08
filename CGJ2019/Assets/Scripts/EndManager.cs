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
			talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "兔兔我来啦......\n", "head/man", true));
			talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "谢谢你的好意，可是你不懂我想要什么...", "head/woman"));
			talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "这...为什么？\n", "head/man", true));
			talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "这一路，你避开的自认为的危险，只是你的一厢情愿\n", "head/woman"));
			talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "这一切本就没有好坏之分，切身体会你才会明白\n", "head/woman"));
			TalkRoot.ShowTalkList(talkInfoList,"Start");
			
		}
        else if (collision.tag == "Ci")
        {
            List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
			talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "兔兔我来啦...\n现在我才明白，心与心的形状虽然不同，却并不是非对错分明\n互相理解远比一厢情愿的付出重要", "head/man", true));
			talkInfoList.Add(new TalkRoot.TalkInfo("兔兔：", "谢谢你！", "head/woman"));
            TalkRoot.ShowTalkList(talkInfoList,"Start");
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
