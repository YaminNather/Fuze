using System.Collections;
using System.Collections.Generic;
using MainGameMgrStuff;
using PlayerFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BallPawnStuff
{
    public class BallPlayerController : PlayerController
    {
        #region Variables
        [SerializeField] private Transform m_LaunchDirIndicatorTrans;

        private int m_DragPhase;
        private Vector2[] m_DragPoints;
        [SerializeField] private float m_LaunchForceMag;

        private int m_BallCount;
        #endregion

        protected void Awake()
        {
            m_LaunchDirIndicatorTrans.gameObject.SetActive(false);

            m_DragPhase = -1;
            m_DragPoints = new Vector2[2];
        }

        private void Update()
        {
            if (m_PossessedPawn != null)
            {
                m_LaunchDirIndicatorTrans.GetComponent<MeshRenderer>().material.SetColor(
                    "_BaseColor",
                    GetPossessedPawnAsBallPawn_F().GetColor_F());

                if (m_DragPhase == 0)
                {
                    m_DragPoints[1] = m_InputActions.DefActionMap.ClickPos.ReadValue<Vector2>();
                    Vector3 dragDirection = GetDragDirection_F();
                    m_LaunchDirIndicatorTrans.rotation = Quaternion.LookRotation((dragDirection != Vector3.zero) ? dragDirection : Vector3.forward, Vector3.up);
                    Debug.DrawRay(m_PossessedPawn.transform.position, dragDirection, Color.green);
                }
            }
        }

        public override void Possess_F(Pawn pawn)
        {
            BallPawn ballPawn = pawn as BallPawn;
            if (ballPawn == null) return;
            
            base.Possess_F(pawn);

            ballPawn.SetToLaunchState_F();
        }

        public override void Unpossess_F()
        {
            base.Unpossess_F();

            m_LaunchDirIndicatorTrans.gameObject.SetActive(false);
        }

        protected override void SetupInput_F(InputActions inputActions)
        {
            base.SetupInput_F(inputActions);

            m_InputActions = inputActions;
            inputActions.DefActionMap.Click.performed += StartDrag_IEF;
            inputActions.DefActionMap.Click.canceled += EndDrag_IEF;
        }

        private void StartDrag_IEF(InputAction.CallbackContext ctx)
        {
            Debug.Log("Clicked");
            if (m_PossessedPawn == null || m_DragPhase != -1) return;

            m_LaunchDirIndicatorTrans.gameObject.SetActive(true);
            m_DragPoints[0] = m_InputActions.DefActionMap.ClickPos.ReadValue<Vector2>();
            m_DragPhase = 0;
        }

        private void EndDrag_IEF(InputAction.CallbackContext ctx)
        {
            Debug.Log("Click Released");
            if (m_PossessedPawn == null || m_DragPhase != 0) return;

            m_DragPoints[1] = m_InputActions.DefActionMap.ClickPos.ReadValue<Vector2>();
            m_DragPhase = -1;

            BallPawn ballPawn = GetPossessedPawnAsBallPawn_F();
            ballPawn.SetKinematicState_F(false);
            ballPawn.Launch_F(GetDragDirection_F() * m_LaunchForceMag);
            Unpossess_F();
        }

        private Vector3 GetDragDirection_F()
        {
            float sens;

#if UNITY_EDITOR
            sens = 0.1f;
#else
            sens = 0.4f;
#endif
            //return Quaternion.Euler(90.0f, 0.0f, 0.0f) * (m_DragPoints[0] - m_DragPoints[1]).normalized;
            return Quaternion.Euler(0.0f, (m_DragPoints[0].x - m_DragPoints[1].x) * sens, 0.0f) * Vector3.forward;
        }

        private BallPawn GetPossessedPawnAsBallPawn_F() => (BallPawn)m_PossessedPawn;
    }
}
