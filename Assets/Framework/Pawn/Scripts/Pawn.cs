using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerFramework
{
    public abstract class Pawn : InputReciever
    {
        #region Variables
        protected PlayerController m_PlayerController;
        #endregion

        public virtual void OnPossessed_F(PlayerController playerController)
        {
            m_PlayerController = playerController;
            SetupInput_F();
        }

        public virtual void OnUnpossessed_F()
        {
            m_PlayerController = null;
            UnsetupInput_F();
        }

        protected void OnDisable()
        {
            if(m_PlayerController != null)
                m_PlayerController.Unpossess_F();
        }

        public PlayerController GetPlayerController_F() => m_PlayerController;
        public void SetPlayerController_F(PlayerController playerController) => m_PlayerController = playerController;
    }
}
