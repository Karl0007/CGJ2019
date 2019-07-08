using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	static public GameManager Instance = null;
	private void Awake()
	{
		if (!Instance) Instance = this;
		DontDestroyOnLoad(this);
	}

	public GameObject Axe;
	public GameObject Apple;
	public GameObject ZhuRou;
	public GameObject Key;
	public GameObject Door;
	public GameObject ItemUI;
	public GameObject DogTalk;
	public GameObject Rou;
	public Sprite[] Sprites;

	void ChangeSprite(GameObject obj, Sprite spr)
	{
		//obj.GetComponent<SpriteRenderer>().sprite = spr;
		obj.GetComponent<Image>().sprite = spr;

	}

	public void ItemAction(PackageManager.Item item,bool res)
	{
		switch (item)
		{
			case PackageManager.Item.apple:
				if (res)
				{
					ChangeSprite(ItemUI, Sprites[1]);
					Destroy(Apple);
				}
				else
				{

				}
				break;
			case PackageManager.Item.Axe:
				if (res)
				{
					ChangeSprite(ItemUI, Sprites[0]);
					Destroy(Axe);
				}
				else
				{

				}
				break;
			case PackageManager.Item.piqiu:
				break;
			case PackageManager.Item.zhurou:
				if (res)
				{
					List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
					talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "什么？你要用苹果换肉？\n这世道变了啊，连兔子都吃肉了", "head/屠夫"));
					talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "我.....", "head/man", true));
					talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "算了，给你吧，哎……", "head/屠夫"));
					TalkRoot.ShowTalkList(talkInfoList);
					ChangeSprite(ItemUI, Sprites[2]);
					//Destroy(ZhuRou);
				}
				else
				{
					if (SingletonT<PackageManager>.Instance.Check(PackageManager.Item.Axe))
					{
						List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
						talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "你知道地底世界吗？", "head/man", true));
						talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "（发出剁肉的声音）\n地底世界？你怎么会问起这个？听说那里充满危险！\n", "head/屠夫"));
						talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "我.....\n（果然梦里的世界真的存在吗？）", "head/man", true));
						talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "你要太闲可以帮我去采点苹果，别想这些奇怪的东西！", "head/屠夫"));
						talkInfoList.Add(new TalkRoot.TalkInfo("小兔:", "emmmm.....", "head/man", true));
						TalkRoot.ShowTalkList(talkInfoList);
					}
				}
				break;
			case PackageManager.Item.key:
				if (res){
					Rou.SetActive(true);
					Destroy(DogTalk);
					Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 1f);

					ChangeSprite(ItemUI, Sprites[3]);
					var temp = Door.AddComponent<MoveManager>();
					temp.X = temp.T2 = 0;
					temp.Y = 2;
					temp.T = 3;
					//Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 3);
				}
				break;

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
