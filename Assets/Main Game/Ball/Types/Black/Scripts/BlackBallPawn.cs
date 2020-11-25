using System.Collections;
using System.Collections.Generic;
using BallPawnStuff;
using MainGameMgrStuff;
using UnityEngine;

public class BlackBallPawn : BallPawn
{

    protected override void Awake()
    {
        base.Awake();

        SetColor_F(Color.gray);
    }

    public override void Launch_F(Vector3 force)
    {
        base.Launch_F(force);

        StartCoroutine(Destroy_IEF());
        
        IEnumerator Destroy_IEF()
        {
            yield return new WaitForSeconds(2.0f);
            MainGameMgr.GetRippleEffect_F().DOBurst_F(transform.position, m_Color);
            Destroy_F();
        }
    }

    public override void SetColor_F(Color color)
    {
        color = Color.grey;
        base.SetColor_F(color);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out NormalBallPawn ballPawn))
        {
            Explode_F(collision.transform.position);
            MainGameMgr.GetRippleEffect_F().DOBurst_F(transform.position, m_Color);
            ballPawn.Explode_F();
        }
    }
}
