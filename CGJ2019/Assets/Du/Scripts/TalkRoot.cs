using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkRoot : MonoBehaviour
{
    private int showIndex = 0;
    class TalkInfo
    {
        public string name;
        public string content;
        public string imageSource;
        public bool bShowLeft;

        public TalkInfo(string name,string content,string imageSource, bool bShowLeft = false)
        {
            this.name = name;
            this.content = content;
            this.imageSource = imageSource;
            this.bShowLeft = bShowLeft;
        }
    }
    private List<TalkInfo> talkInfoList = new List<TalkInfo>();
    // Start is called before the first frame update
    void Start()
    {
        Init();
        talkInfoList.Add(new TalkInfo("杜1","Hello你好啊啊啊啊啊啊","head/man"));
        talkInfoList.Add(new TalkInfo("杜2","Hello你好啊啊啊啊啊啊", "head/man"));
        talkInfoList.Add(new TalkInfo("杜3","Hello你好啊啊啊啊啊啊", "head/woman",true));
        talkInfoList.Add(new TalkInfo("杜4","Hello你好啊啊啊啊啊啊", "head/man"));
        talkInfoList.Add(new TalkInfo("杜5","Hello你好啊啊啊啊啊啊", "head/woman",true));

        ShowTalk(talkInfoList[0]);
    }

    private void Init()
    {
        
    }

    public void Click()
    {
        showIndex++;
        if (showIndex < talkInfoList.Count)
        {
            ShowTalk(talkInfoList[showIndex]);
        } else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    void ShowTalk(TalkInfo info)
    {
        WillTool.GetChildInDepth<Text>("Name", transform).text = info.name;
        WillTool.GetChildInDepth<Text>("Content", transform).text = info.content;
        WillTool.GetChildInDepth<TypewriterEffect>("Content", transform).OnStart();

        GameObject imageTarget = null;
        GameObject imageTarget1 = WillTool.GetChildInDepth("Image1", this.gameObject);
        GameObject imageTarget2 = WillTool.GetChildInDepth("Image2", this.gameObject);
        imageTarget1.SetActive(false);
        imageTarget2.SetActive(false);
        if (info.bShowLeft)
        {
            imageTarget = imageTarget1;
        }
        else
        {
            imageTarget = imageTarget2;
        }
        imageTarget.SetActive(true);
        imageTarget.GetComponent<Image>().sprite = Resources.Load<Sprite>(info.imageSource);
    }
}
