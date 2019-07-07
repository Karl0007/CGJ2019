using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
	public Collider2D Player;
	public string[] Names;
	public string[] Talks;
	public string[] Pictrues;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Play")
		{
			List<TalkRoot.TalkInfo> talkInfoList = new List<TalkRoot.TalkInfo>();
			for (int i = 0; i < Names.Length; i++)
			{
				talkInfoList.Add(new TalkRoot.TalkInfo(Names[i], Talks[i], Pictrues[i]));
			}
			TalkRoot.ShowTalkList(talkInfoList);
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
