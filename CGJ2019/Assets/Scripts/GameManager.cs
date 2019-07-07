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
	public Sprite[] Sprites;

	void ChangeSprite(GameObject obj, Sprite spr)
	{
		//obj.GetComponent<SpriteRenderer>().sprite = spr;
		obj.GetComponent<Image>().sprite = spr;

	}

	public void ItemAction(PackageManager.Item item)
	{
		switch (item)
		{
			case PackageManager.Item.apple:
				ChangeSprite(ItemUI, Sprites[1]);
				Destroy(Apple);
				break;
			case PackageManager.Item.Axe:
				ChangeSprite(ItemUI, Sprites[0]);
				Destroy(Axe);
				break;
			case PackageManager.Item.piqiu:
				break;
			case PackageManager.Item.zhurou:
				ChangeSprite(ItemUI, Sprites[2]);
				Destroy(ZhuRou);
				break;
			case PackageManager.Item.key:
				ChangeSprite(ItemUI, Sprites[3]);
				Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 3);
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
