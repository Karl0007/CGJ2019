using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkRoot : MonoBehaviour
{
    private static TalkRoot s_talkRoot;

    private int showIndex = 0;
    public class TalkInfo
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
        s_talkRoot = this;
        FirtInit();
    }

    private void OnGUI()
    {
       
    }

    private void FirtInit()
    {
        List<TalkInfo> talkInfoList = new List<TalkInfo>();
        talkInfoList.Add(new TalkInfo("杜1", "Hello你好啊啊啊啊啊啊", "head/man"));
        talkInfoList.Add(new TalkInfo("杜2", "Hello你好啊啊啊啊啊啊", "head/man"));
        talkInfoList.Add(new TalkInfo("杜3", "Hello你好啊啊啊啊啊啊", "head/woman", true));
        talkInfoList.Add(new TalkInfo("杜4", "Hello你好啊啊啊啊啊啊", "head/man"));
        talkInfoList.Add(new TalkInfo("杜5", "Hello你好啊啊啊啊啊啊", "head/woman", true));
        ShowTalkList(talkInfoList);
    }

    public static void ShowTalkList(List<TalkInfo> talkInfoList)
    {
        s_talkRoot.showIndex = 0;
        s_talkRoot.talkInfoList = talkInfoList;
        s_talkRoot.gameObject.SetActive(true);
        s_talkRoot.ShowTalk(talkInfoList[0]);
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
			PlayerManager.UnLock();

		}
	}

    void ShowTalk(TalkInfo info)
    {
		PlayerManager.Lock();
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

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			Click();
		}
	}
}
