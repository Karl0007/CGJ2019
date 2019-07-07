using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMove_oc : ObjectControl
{
    public Vector3 to;
    private Vector3 from;
    public float time;

    private LTDescr desc;
    void Start()
    {
        from = this.transform.localPosition;
    }

    public override void PlayerEnter()
    {
        base.PlayerEnter();
        desc = LeanTween.moveLocal(gameObject, to, time).setLoopPingPong();
    }

    public override void PlayerLeave()
    {
        base.PlayerLeave();
        LeanTween.cancel(desc.id);
    }
}
