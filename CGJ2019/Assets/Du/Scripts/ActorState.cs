using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Left,
        Right,
        LeftWall,
        RightWall,
    }

    public State state;

    public GameObject leftGo;
    public GameObject rightGo;
    public GameObject leftWallGo;
    public GameObject rightWallGo;
    public GameObject IdleGo;

    void Start()
    {

    }

    public void SetState(State s)
    {
		if (state == s)
		{
			return;
		}
		state = s;
        leftGo.SetActive(false);
        rightGo.SetActive(false);
        leftWallGo.SetActive(false);
        rightWallGo.SetActive(false);
        IdleGo.SetActive(false);
        switch (s)
        {
            case State.Left:
                leftGo.SetActive(true);
                break;
            case State.Right:
                rightGo.SetActive(true);
                break;
            case State.LeftWall:
                leftWallGo.SetActive(true);
                break;
            case State.RightWall:
                rightWallGo.SetActive(true);
                break;
            case State.Idle:
                IdleGo.SetActive(true);
                break;
        }
    }
}
