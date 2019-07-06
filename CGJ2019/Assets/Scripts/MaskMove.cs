using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskMove : MonoBehaviour
{
	public GameObject Player;
	public float X, Y, T;
	private float time;

	// Start is called before the first frame update
	void Start()
	{
		time = 0;
	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime;
		if (time > T) time = 0;
		transform.position = new Vector3(Player.transform.position.x + Random.Range(0.9f,1.1f) * X * Mathf.Cos((time / T) * 3.14f * 2), Player.transform.position.y + Y * Random.Range(0.9f, 1.1f) * Mathf.Sin((time / T) * 3.14f * 2), transform.position.z);
	}
}
