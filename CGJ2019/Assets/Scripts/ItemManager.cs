using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public PackageManager.Item m_type;
    public GameObject Icon;
    public GameObject Door;
    const int Player = 9;
    public string ShowTalkItem;
    public Vector3 showTalkOffset;

    private GameObject showTalkGo;

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

    bool Action()
    {
        if (!Check()) return false;
        switch (m_type)
        {
            case PackageManager.Item.Axe:
                SingletonT<PackageManager>.Instance.Add(PackageManager.Item.Axe);
                Destroy(gameObject);
                break;
            case PackageManager.Item.apple:
                SingletonT<PackageManager>.Instance.Exchange(PackageManager.Item.Axe, PackageManager.Item.apple);
                Destroy(gameObject);
                break;
            case PackageManager.Item.zhurou:
                SingletonT<PackageManager>.Instance.Exchange(PackageManager.Item.apple, PackageManager.Item.zhurou);
				Destroy(gameObject);
				break;
            case PackageManager.Item.key:
                SingletonT<PackageManager>.Instance.Delete(PackageManager.Item.zhurou);
                Door.transform.position = new Vector3(Door.transform.position.x, Door.transform.position.y + 3);
                //Destroy(gameObject);
                break;
            case PackageManager.Item.piqiu:
                SingletonT<PackageManager>.Instance.Add(PackageManager.Item.piqiu);
                Destroy(gameObject);
                break;
            default:
                return false;
        }

        return true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
        Debug.Log(Check());
        if (collision.gameObject.layer == Player && Check())
        {
            Icon.SetActive(true);
        }

        ShowTalk(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Player && Check())
        {
            Icon.SetActive(false);
        }

        ShowTalk(false);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Action())
            {
                PlayAudio.Play(SingletonTMono<PlayerManager>.Instance.gameObject, "Sound/交互成功提示音");
            }
           
            //switch ()
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowTalk(bool isShow)
    {
        if(!isShow)
        {
            if(this.showTalkGo != null)
            {
                showTalkGo.SetActive(false);
            }

            return;
        }

        GameObject go = showTalkGo;
        if (go == null)
        {
            go = Instantiate<GameObject>(Resources.Load<GameObject>("ShowTalk"), transform);
            this.showTalkGo = go;
        }

        this.showTalkGo.SetActive(true);
        go.transform.localPosition = this.showTalkOffset;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one*2;
        WillTool.GetChildInDepth<SpriteRenderer>("Target", go.transform).sprite = Resources.Load<Sprite>(ShowTalkItem);

    }
}
