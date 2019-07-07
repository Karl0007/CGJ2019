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
					talkInfoList.Add(new TalkRoot.TalkInfo("", "", ""));
					talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "什么？！你要去那里？？\n算了，给你吧，哎……", "head/man"));
					TalkRoot.ShowTalkList(talkInfoList);
					ChangeSprite(ItemUI, Sprites[2]);
					//Destroy(ZhuRou);
				}
				else
				{
					List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
					talkInfoList.Add(new TalkRoot.TalkInfo("", "", ""));
					talkInfoList.Add(new TalkRoot.TalkInfo("屠夫：", "（发出剁肉的声音）\n世道变了，兔子竟然也要买肉吗\n不过你如果要的话我可以用苹果和我换", "head/man"));
					TalkRoot.ShowTalkList(talkInfoList);
				}
				break;
			case PackageManager.Item.key:
				Destroy(DogTalk);
				Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 1f);

				ChangeSprite(ItemUI, Sprites[3]);
				var temp = Door.AddComponent<MoveManager>();
				temp.X = temp.T2 = 0;
				temp.Y = 4;
				temp.T = 3;
				//Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 3);
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
