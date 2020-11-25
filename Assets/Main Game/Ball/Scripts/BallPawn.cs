using System;
using System.Collections;
using System.Collections.Generic;
using MainGameMgrStuff;
using UnityEngine;
using PlayerFramework;
using UnityEngine.InputSystem;


namespace BallPawnStuff
{
    public class BallPawn : Pawn
    {
        #region Variables
        protected Rigidbody m_Rigidbody;
        protected InputActions m_InputActions;

        protected Color m_Color;

        public System.Action m_OnLaunchE;

        protected const int DEFLAYER = 6;
        protected const int LAUNCHINGLAYER = 7;
        protected const int LAUNCHAREALAYER = 9;
        #endregion

        protected virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void LateUpdate()
        {
            if (m_Rigidbody.velocity.y > 0.0f) 
                m_Rigidbody.velocity = m_Rigidbody.velocity.With(y:0.0f);
        }

        public virtual void Launch_F(Vector3 force)
        {
            m_Rigidbody.AddForce(force);
            MainGameMgr.GetRippleEffect_F().DOBurst_F(transform.position, m_Color);
            m_OnLaunchE?.Invoke();
            //m_PlayerController.Unpossess_F();
        }

        public virtual void SetColor_F(Color color)
        {
            GetComponentInChildren<MeshRenderer>().material.SetColor("_BaseColor", color);
            GetComponentInChildren<Light>().color = color;

            m_Color = color;
        }

        private void SetLayer_F(int layer)
        {
            foreach (Transform trans in GetComponentsInChildren<Transform>())
                trans.gameObject.layer = layer;
        }

        public void SetLayerToDef_F() => SetLayer_F(DEFLAYER);

        public void SetLayerToLaunch_F() => SetLayer_F(LAUNCHINGLAYER);

        public void SetKinematicState_F(bool value)
        {
            m_Rigidbody.isKinematic = value;
        }

        public virtual void SetToLaunchState_F()
        {
            SetKinematicState_F(true);
            SetLayerToLaunch_F();
        }
        
        public void Explode_F(Vector3 pos)
        {
            Debug.Log("Exploding");
            float radius = 100.0f;
            Collider[] results = Physics.OverlapSphere(pos, radius);
            foreach(Collider c in results)
            {
                BallPawn ballPawn = c.GetComponentInParent<BallPawn>();
                if(ballPawn != null)
                    ballPawn.m_Rigidbody.AddExplosionForce(100.0f, pos, radius);
            }
            Destroy_F();
        }

        public void Explode_F() => Explode_F(transform.position);

        public void Destroy_F() => Destroy(gameObject);

        public void DestroyOnGameEnd_F()
        {
            Destroy_F();
        }

        public Color GetColor_F() => m_Color;

        private void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.layer == LAUNCHAREALAYER)
            {
                Debug.Log($"Exited Launch Area; Launch Area Name = {collider.transform.name}");
                SetLayerToDef_F();
            }
        }





        public Rigidbody GetRigidbody_F() => m_Rigidbody;
    }
}
