using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public Collider2D m_collider;
	public Collider2D m_rightCollider;
	public Collider2D m_leftCollider;
	public Collider2D m_downCollider;
	public Rigidbody2D m_rigidbody;

	private bool m_canmove;
	private bool m_doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	//玩家移动
	private void MoveUpdate()
	{
		const int Wall = 1 << 8; 
		bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
		bool up = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
		bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
		{
			m_canmove = true;
		}

		if (left && m_canmove)
		{
			//向左移动
			m_rigidbody.AddForce(new Vector2(-ConstData.PlayerAcc,0), ForceMode2D.Force);
			//最大速度
			if (m_rigidbody.velocity.x < -ConstData.PlayerMaxVel)
			{
				m_rigidbody.velocity = new Vector2(-ConstData.PlayerMaxVel, m_rigidbody.velocity.y);
			}
			//爬墙
			if (m_leftCollider.IsTouchingLayers(Wall) && !m_downCollider.IsTouchingLayers(Wall))
			{
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
			}else
			//爬墙跳
			if (m_rightCollider.IsTouchingLayers(Wall) && right)
			{
				m_canmove = false;
				m_doubleJump = false;
				m_rigidbody.AddForce(new Vector2(-ConstData.PlayerRef, ConstData.PlayerJump * 1.3f), ForceMode2D.Impulse);
			}
			else
			if (m_leftCollider.IsTouchingLayers(Wall) && left)
			{
				m_canmove = false;
				m_doubleJump = false;
				m_rigidbody.AddForce(new Vector2(ConstData.PlayerRef, ConstData.PlayerJump * 1.3f), ForceMode2D.Impulse);
			}else
			//双跳
			if (m_doubleJump)
			{
				m_doubleJump = false;
				m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x, 0);
				m_rigidbody.AddForce(new Vector2(0, ConstData.PlayerJump), ForceMode2D.Impulse);
			}

		}
	}

	// Update is called once per frame
	void Update()
    {
		MoveUpdate();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == 12) Debug.Log("gameover");

	}

}
