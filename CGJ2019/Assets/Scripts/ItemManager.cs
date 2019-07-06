using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

	public PackageManager.Item m_type;
	public GameObject Icon;
	public GameObject Door;
	const int Player = 9;

    // Start is called before the first frame update
    void Start()
    {
    }

	bool Check()
	{
		switch (m_type)
		{
			case PackageManager.Item.Axe:
				return true;
			case PackageManager.Item.apple:
				return SingletonT<PackageManager>.Instance.Check(PackageManager.Item.Axe);
			case PackageManager.Item.zhurou:
				return SingletonT<PackageManager>.Instance.Check(PackageManager.Item.apple);
			case PackageManager.Item.key:
				return SingletonT<PackageManager>.Instance.Check(PackageManager.Item.zhurou);
			case PackageManager.Item.piqiu:
				return true;
			default:
				return false;
		}
	}

	void Action()
	{
		if (!Check()) return;
		switch (m_type)
		{
			case PackageManager.Item.Axe:
				SingletonT<PackageManager>.Instance.Add(PackageManager.Item.Axe);
				Destroy(gameObject);
				return;
			case PackageManager.Item.apple:
				SingletonT<PackageManager>.Instance.Exchange(PackageManager.Item.Axe,PackageManager.Item.apple);
				Destroy(gameObject);
				return;
			case PackageManager.Item.zhurou:
				SingletonT<PackageManager>.Instance.Exchange(PackageManager.Item.apple, PackageManager.Item.zhurou);
				Destroy(gameObject);
				return;
			case PackageManager.Item.key:
				SingletonT<PackageManager>.Instance.Delete(PackageManager.Item.zhurou);
				Door.transform.position = new Vector3(Door.transform.position.x,Door.transform.position.y + 3);
				//Destroy(gameObject);
				return;
			case PackageManager.Item.piqiu:
				SingletonT<PackageManager>.Instance.Add(PackageManager.Item.piqiu);
				Destroy(gameObject);
				return;
			default:
				return;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject.layer);
		Debug.Log(Check());
		if (collision.gameObject.layer == Player && Check())
		{
			Icon.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == Player && Check())
		{
			Icon.SetActive(false);
		}
	}


	private void OnTriggerStay2D(Collider2D collision)
	{
		if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			Action();
			//switch ()
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
