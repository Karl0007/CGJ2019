using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkRoot : MonoBehaviour
{
    private static TalkRoot s_talkRoot;

    private int showIndex = 0;
	private static string nextScene;
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
	private TypewriterEffect Typewriter;
    // Start is called before the first frame update
    void Start()
    {
        s_talkRoot = this;
		Typewriter = WillTool.GetChildInDepth<Text>("Content", transform).GetComponent<TypewriterEffect>();
        FirtInit();
    }

    private void OnGUI()
    {
       
    }

    private void FirtInit()
    {
        List<TalkInfo> talkInfoList = new List<TalkInfo>();
        talkInfoList.Add(new TalkInfo("小兔:", "唔……只是一场梦么……\n（揉揉耳朵）\n兔兔……难到真的存在另一个世界吗？", "head/man",true));
        talkInfoList.Add(new TalkInfo("小兔:", "这个斧子是……啊！莫非梦中的一切都是真的？\n", "head/man",true));
        talkInfoList.Add(new TalkInfo("小兔:", "我要出发了！兔兔还在地底等我！\n（↓：交互 ←,→：移动 ↑：跳跃）\n（→↑,←↑：爬墙跳跃 ↑↑：双跳）", "head/man",true));
		ShowTalkList(talkInfoList);
    }

    public static void ShowTalkList(List<TalkInfo> talkInfoList, string scene=null)
    {
		nextScene = scene;
        s_talkRoot.showIndex = 0;
        s_talkRoot.talkInfoList = talkInfoList;
        s_talkRoot.gameObject.SetActive(true);
        s_talkRoot.ShowTalk(talkInfoList[0]);
    }

    public void Click()
    {
		if (!Typewriter.isActive)
		{
			showIndex++;
			if (showIndex < talkInfoList.Count)
			{
				ShowTalk(talkInfoList[showIndex]);
			}
			else
			{
				this.gameObject.SetActive(false);
				PlayerManager.UnLock();
				if (nextScene != null)
				{
					SceneManager.LoadScene(nextScene);
					nextScene = null;
				}
			}
		}
		else
		{
			Typewriter.OnFinish();
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
        if(string.IsNullOrEmpty(info.imageSource))
        {

        }else
        {
            imageTarget.SetActive(true);
            imageTarget.GetComponent<Image>().sprite = Resources.Load<Sprite>(info.imageSource);

        }
    }

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			Click();
		}
	}
}
