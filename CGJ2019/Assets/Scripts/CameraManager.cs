using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public GameObject m_player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		var tmp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		tmp += (m_player.transform.position - tmp)*ConstData.CameraSpd;
		tmp.z = -10;
		transform.position = tmp;
    }
}
