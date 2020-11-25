using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameMgrStuff
{
    public abstract class ScoreMgrBase : MonoBehaviour
    {
        #region Variables

        [Header("Score Label Stuff")] [SerializeField]
        protected Text m_ScoreLbl;

        protected int m_Score;

        public Action<int> m_OnScoreUpdateE;

        #endregion

        protected virtual void Awake()
        {
            m_ScoreLbl.text = "0";
        }

        public virtual void ScoreAdd_F(int amount)
        {
            m_Score += amount;

            ScoreLblUpdate_F();
            m_OnScoreUpdateE?.Invoke(m_Score);
        }

        protected virtual void ScoreLblUpdate_F()
        {
            m_ScoreLbl.text = m_Score + "";
        }
    }
}
