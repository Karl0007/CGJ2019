using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public GameObject m_player;
	public static float CameraSize;

    // Start is called before the first frame update
    void Start()
    {
		CameraSize = 7;
    }

    // Update is called once per frame
    void Update()
    {
		var tmp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		tmp += (m_player.transform.position - tmp)*ConstData.CameraSpd;
		tmp.z = -10;
		Camera.main.orthographicSize += (CameraSize - Camera.main.orthographicSize) * 0.06f;
		transform.position = tmp;
    }

}
