using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Collider2D m_collider;
    public Collider2D m_rightCollider;
    public Collider2D m_leftCollider;
    public Collider2D m_downCollider;
    public Rigidbody2D m_rigidbody;
    public ActorState m_actorState;

    public GameObject MyCi;
    public GameObject MyHeart;

    public int m_bloodNum = 3;
    public float m_hurtProtectTime = 3;

    private float m_heartCD;
    private bool m_canmove;
    private bool m_doubleJump;
    private static bool m_lock;

    private GameObject m_BloodRoot;
    private GameObject m_BlackRoot;

    private float m_canCreat = 0;

    const int Heart = 12;

    public bool m_CanBeHurt = true;

    [Header("受伤回弹的参数")]
    public float springBackFactor = 5f;


    // Start is called before the first frame update
    void Start()
    {

        m_canCreat = 0;

        m_actorState = GetComponentInChildren<ActorState>();
        m_BloodRoot = GameObject.Find("Bloods");
        m_BlackRoot = GameObject.Find("Blacks");

        ShowBlood();
        m_canmove = true;
        m_lock = false;
    }

    static public void Lock()
    {
        m_lock = true;
    }

    static public void UnLock()
    {
        m_lock = false;
    }

    //玩家移动
    private void MoveUpdate()
    {
        if (m_lock)
        {
            return;
        }
        const int Wall = 1 << 8;
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || m_downCollider.IsTouchingLayers(Wall))
        {
            m_canmove = true;
        }
        if (Mathf.Abs(m_rigidbody.velocity.x) > 0.01f)
        {
            if (m_rigidbody.velocity.x > 0)
            {
                m_actorState.SetState(ActorState.State.Right);
            }
            else
            {
                m_actorState.SetState(ActorState.State.Left);
            }
        }
        else
        {
            m_actorState.SetState(ActorState.State.Idle);
        }

        if (left && m_canmove)
        {
            m_actorState.SetState(ActorState.State.Left);
            //向左移动
            m_rigidbody.AddForce(new Vector2(-ConstData.PlayerAcc, 0), ForceMode2D.Force);
            //最大速度
            if (m_rigidbody.velocity.x < -ConstData.PlayerMaxVel)
            {
                m_rigidbody.velocity = new Vector2(-ConstData.PlayerMaxVel, m_rigidbody.velocity.y);
            }
            //爬墙
            if (m_leftCollider.IsTouchingLayers(Wall) && !m_downCollider.IsTouchingLayers(Wall))
            {
                m_actorState.SetState(ActorState.State.LeftWall);
                m_rigidbody.AddForce(new Vector2(0, ConstData.PlayerFri), ForceMode2D.Force);
            }
        }

        if (right && m_canmove)
        {
            //向右移动
            m_rigidbody.AddForce(new Vector2(ConstData.PlayerAcc, 0), ForceMode2D.Force);
            //最大速度
            if (m_rigidbody.velocity.x > ConstData.PlayerMaxVel)
            {
                m_rigidbody.velocity = new Vector2(ConstData.PlayerMaxVel, m_rigidbody.velocity.y);
            }
            //爬墙
            if (m_rightCollider.IsTouchingLayers(Wall) && !m_downCollider.IsTouchingLayers(Wall))
            {
                m_actorState.SetState(ActorState.State.RightWall);
                m_rigidbody.AddForce(new Vector2(0, ConstData.PlayerFri), ForceMode2D.Force);
            }
        }

        //跳跃
        if (up)
        {
            //平地
            if (m_downCollider.IsTouchingLayers(Wall))
            {
                m_doubleJump = true;
                m_rigidbody.AddForce(new Vector2(0, ConstData.PlayerJump), ForceMode2D.Impulse);
                PlayAudio.Play(this.gameObject, "Sound/跳跃3");
            }
            else
            //爬墙跳
            if (m_rightCollider.IsTouchingLayers(Wall) && right)
            {
                PlayAudio.Play(this.gameObject, "Sound/跳跃2");
                m_canmove = false;
                m_doubleJump = false;
                m_rigidbody.AddForce(new Vector2(-ConstData.PlayerRef, ConstData.PlayerJump * 1.3f), ForceMode2D.Impulse);
            }
            else
            if (m_leftCollider.IsTouchingLayers(Wall) && left)
            {
                PlayAudio.Play(this.gameObject, "Sound/跳跃2");
                m_canmove = false;
                m_doubleJump = false;
                m_rigidbody.AddForce(new Vector2(ConstData.PlayerRef, ConstData.PlayerJump * 1.3f), ForceMode2D.Impulse);
            }
            else
            //双跳
            if (m_doubleJump)
            {
                PlayAudio.Play(this.gameObject, "Sound/跳跃3");
                m_doubleJump = false;
                m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 0);
                m_rigidbody.AddForce(new Vector2(0, ConstData.PlayerJump), ForceMode2D.Impulse);
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_heartCD > 0) m_heartCD -= Time.deltaTime;
        if (m_canCreat > 0) m_canCreat -= Time.deltaTime;

        MoveUpdate();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_lock) return;
        if (m_canCreat <= 0 && collision.tag == "CreatHeart" && (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && m_CanBeHurt)
        {
            if (m_bloodNum > 0)
            {
                m_canCreat = 1;
                m_bloodNum--;
                Instantiate(MyHeart).SetActive(true);
            }
            if (m_bloodNum < 0)
            {
                m_canCreat = 1;
                m_bloodNum++;
                Instantiate(MyCi).SetActive(true);
            }

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraSmall")
        {
            CameraManager.CameraSize = 7;
        }
    }

    private void Hurt(Collider2D collision)
    {
        m_CanBeHurt = false;
        //Debug.Log("!!!!!!!!!!!OnTrigher 12");
        m_bloodNum--;

        Invoke("EndProtectTime", this.m_hurtProtectTime);
        Vector3 off = (this.transform.position - collision.transform.position).normalized * springBackFactor;
        m_rigidbody.velocity = Vector2.zero;
        m_rigidbody.AddForce(new Vector2(off.x, off.y), ForceMode2D.Impulse);
        PlayAudio.Play(this.gameObject, "Sound/受伤");
        ShowBlood();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CreatHeart")
        {

        }

        if (collision.tag == "CameraSmall")
        {
            CameraManager.CameraSize = 16;
        }

        if (collision.tag == "Heart" && m_heartCD <= 0)
        {
            m_heartCD = 1;
            m_bloodNum++;
            m_bloodNum = Mathf.Min(10, m_bloodNum);
            ShowBlood();
        }

        if (collision.gameObject.layer == Heart && m_CanBeHurt)
        {
            Hurt(collision);
        }
        ShowBlood();
    }

    public void EndProtectTime()
    {
        m_CanBeHurt = true;
    }

    private void ShowBlood()
    {

        for (int i = 1; i <= 10; i++)
        {
            if (i <= this.m_bloodNum)
            {
                WillTool.GetChildInDepth("Blood" + i, m_BloodRoot).SetActive(true);
            }
            else
            {
                WillTool.GetChildInDepth("Blood" + i, m_BloodRoot).SetActive(false);
            }

        }



        for (int i = 1; i <= 10; i++)
        {
            if (i <= Mathf.Abs(this.m_bloodNum))
            {
                WillTool.GetChildInDepth("Blood" + i, m_BlackRoot).SetActive(true);
            }
            else
            {
                WillTool.GetChildInDepth("Blood" + i, m_BlackRoot).SetActive(false);
            }

            if (this.m_bloodNum < 0)
            {
                m_BloodRoot.SetActive(false);
                m_BlackRoot.SetActive(true);
                WillTool.GetChildInDepth("Blood" + i, m_BloodRoot).SetActive(false);
            }
            else
            {
                m_BloodRoot.SetActive(true);
                m_BlackRoot.SetActive(false);
            }
        }

    }

}
