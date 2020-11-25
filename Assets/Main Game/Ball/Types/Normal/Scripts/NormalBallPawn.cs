using System.Collections;
using System.Collections.Generic;
using MainGameMgrStuff;
using PlayerFramework;
using UnityEngine;

namespace BallPawnStuff
{
    public class NormalBallPawn : BallPawn
    {
        [SerializeField] private Material m_BlinkingMat;

        private EBallTypes m_BallType;

        protected override void Awake()
        {
            base.Awake();

            m_BallType = EBallTypes.Normal;
        }

        protected void SpawnTwoNewBalls_F(Vector3 pos0, Vector3 pos1)
        {
            MainGameMgr.GetBallsSpawner_F().SpawnNormalBall_F(pos0, m_Color);
            MainGameMgr.GetBallsSpawner_F().SpawnNormalBall_F(pos1, m_Color);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.TryGetComponent(out NormalBallPawn collidedBallPawn))
            {
                if (m_Color != collidedBallPawn.GetColor_F()) return;
                if (m_Rigidbody.velocity.sqrMagnitude <= collidedBallPawn.m_Rigidbody.velocity.sqrMagnitude) return;

                Debug.DrawRay(transform.position,m_Rigidbody.velocity.normalized * 5.0f, Color.white, 10.0f);
                
                collidedBallPawn.Destroy_F();

                Vector3 collisionPoint = collision.contacts[0].point;
                int nextType = (Mathf.Max((int)m_BallType, (int)collidedBallPawn.m_BallType) + 1);
                
                OnHitCorrectColor_F(collisionPoint, nextType);
            }
        }

        public void OnHitCorrectColor_F(Vector3 collisionPoint, int nextType)
        {
            int increment;
            if (nextType < 3)
            {
                ChangeToType_F((EBallTypes)nextType);
                increment = 1;
            }
            else
            {
                Vector3 collisionPointToPlayerVector = transform.position - collisionPoint;
                SpawnTwoNewBalls_F(transform.position, collisionPoint + (-collisionPointToPlayerVector));
                Explode_F(collisionPoint);
                increment = 3;
                MainGameMgr.GetInstance_F().AddBalls_F(3);
            }

            MainGameMgr.GetScoreMgr_F().ScoreAdd_F(increment);
            MainGameMgr.GetScoreMgr_F().IncrementLblDisplay_F(collisionPoint, GetColor_F(), increment);

            MainGameMgr.GetRippleEffect_F().DOBurst_F(collisionPoint,
                GetColor_F());
        }

        private void ChangeToType_F(EBallTypes ballType)
        {
            Debug.Log($"Changing to Type {ballType}");

            MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
            switch (ballType)
            {
                case EBallTypes.Normal:
                    meshRenderer.transform.localScale = Vector3.one * 0.2f;
                    m_BallType = EBallTypes.Normal;
                    break;

                case EBallTypes.Large:
                    meshRenderer.transform.localScale = Vector3.one * 0.5f;
                    m_BallType = EBallTypes.Large;
                    break;

                case EBallTypes.Blinking:
                    meshRenderer.transform.localScale = Vector3.one * 0.5f;
                    meshRenderer.material = m_BlinkingMat;
                    SetColor_F(m_Color);
                    m_BallType = EBallTypes.Blinking;
                    break;


                default:
                    break;
            }
        }




        public EBallTypes GetBallType_F() => m_BallType;

        public enum EBallTypes { Normal, Large, Blinking }
    }
}
