using System.Collections;
using System.Collections.Generic;
using BallPawnStuff;
using MainGameMgrStuff;
using UnityEngine;

namespace BallPawnStuff
{
    public class AnyColorBallPawn : NormalBallPawn
    {
        private float m_Hue;

        public override void Launch_F(Vector3 force)
        {
            base.Launch_F(force);
            StartCoroutine(destroy_IEF());

            IEnumerator destroy_IEF()
            {
                yield return new WaitForSeconds(2.0f);
                Destroy_F();

            }
        }

        private void Update()
        {
            m_Hue += Time.deltaTime;
            if (m_Hue > 1.0f) m_Hue = 0.0f;

            SetColor_F(Color.HSVToRGB(m_Hue, 1.0f, 1.0f));
        }

        //protected override void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.transform.TryGetComponent(out NormalBallPawn collidedBallPawn))
        //    {
        //        Vector3 collisionPoint = collision.contacts[0].point;
        //        int increment;
        //        int nextType = (int) collidedBallPawn.GetBallType_F() + 1;
        //        if (nextType < 3)
        //        {
        //            collidedBallPawn.ChangeToType_F((EBallTypes) nextType);
        //            increment = 1;
        //        }
        //        else
        //        {
        //            collidedBallPawn.SpawnTwoNewBalls_F(transform.position, collidedBallPawn.transform.position);
        //            collidedBallPawn.Explode_F(collisionPoint);
        //            increment = 3;
        //            MainGameMgr.GetInstance_F().AddBalls_F(3);
        //        }

        //        MainGameMgr.GetScoreMgr_F().ScoreAdd_F(increment);
        //        MainGameMgr.GetScoreMgr_F().IncrementLblDisplay_F(collisionPoint, collidedBallPawn.GetColor_F(), increment);

        //        MainGameMgr.GetRippleEffect_F().DOBurst_F(collisionPoint,
        //            collidedBallPawn.GetColor_F());

        //        Destroy_F();
        //    }
        //}

        protected override void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out NormalBallPawn collidedBallPawn))
            {
                Vector3 collisionPoint = collision.contacts[0].point;
                int nextType = (int)collidedBallPawn.GetBallType_F() + 1;

                collidedBallPawn.OnHitCorrectColor_F(collisionPoint, nextType);

                Destroy_F();
            }
        }
    }
}
