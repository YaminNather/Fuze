using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerFramework
{
    public abstract class PlayerController : InputReciever
    {
        #region Variables
        protected InputActions m_InputActions;
        protected Pawn m_PossessedPawn;
        #endregion

        protected virtual void OnEnable()
        {
            SetupInput_F();
        }

        public virtual void Possess_F(Pawn pawn)
        {
            if (pawn == null || m_PossessedPawn == pawn) return;

            if (m_PossessedPawn != null) Unpossess_F();

            m_PossessedPawn = pawn;
            m_PossessedPawn.OnPossessed_F(this);
        }

        public virtual void Unpossess_F()
        {
            if (m_PossessedPawn == null) return;

            m_PossessedPawn.OnUnpossessed_F();
            m_PossessedPawn = null;
        }

        protected virtual void OnDisable()
        {
            UnsetupInput_F();
        }

        public Pawn GetPossessedPawn_F() => m_PossessedPawn;
    }
}
