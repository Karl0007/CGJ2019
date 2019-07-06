using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
	public float X, Y, T,T2;
	private float centerx, centery,time;

	// Start is called before the first frame update
    void Start()
    {
		time = T2;
		centerx = transform.position.x;
		centery = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;
		if (time > T) time = 0;
		transform.position = new Vector3(centerx + X*Mathf.Cos((time / (T)) * 3.14f * 2) ,centery + Y*Mathf.Sin((time / (T)) * 3.14f * 2), transform.position.z);
	}
}
