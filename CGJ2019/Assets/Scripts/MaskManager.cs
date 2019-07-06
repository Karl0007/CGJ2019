using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
	public Collider2D m_collider;
	public SpriteRenderer m_render;
	public bool Collider;
	public bool Render;
	public bool TouchCollider;
	public bool TouchRender;
	private bool exist;
	const int Mask = 1 << 10;
    // Start is called before the first frame update
    void Start()
    {
		m_render.maskInteraction = Render ? SpriteMaskInteraction.None : TouchRender ? SpriteMaskInteraction.VisibleInsideMask : SpriteMaskInteraction.VisibleOutsideMask;
    }

    // Update is called once per frame
    void Update()
    {
		exist = m_collider.IsTouchingLayers(Mask) && TouchCollider || Collider;
		m_collider.isTrigger = !exist;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{

	}
}
