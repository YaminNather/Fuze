using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerFramework
{
    public abstract class InputReciever : MonoBehaviour
    {
        #region Variables
        private InputActions m_InputActions;
        #endregion

        protected void SetupInput_F()
        {
            m_InputActions = new InputActions();
            m_InputActions.Enable();

            SetupInput_F(m_InputActions);
        }

        protected virtual void SetupInput_F(InputActions inputActions) {}

        protected void UnsetupInput_F()
        {
            if (m_InputActions == null) return;

            m_InputActions.Disable();
            m_InputActions = null;
        }
    }
}
