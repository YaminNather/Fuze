using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameMgrStuff
{
    public class ScoreMgr : ScoreMgrBase
    {
        #region Variables
        [Space(30)] private Tweener m_ScoreLblAnimT;
        [SerializeField] private AnimationCurve m_ScoreLblAnimAC;
        private Tweener ColorChangingT;

        [SerializeField] private Text m_IncrementLbl;
        private int m_IncrementCount;
        private Sequence m_IncrementSeq;
        #endregion

        protected override void Awake()
        {
            base.Awake();

            m_IncrementLbl.gameObject.SetActive(false);

            m_ScoreLblAnimT = DOTween.To(() => 1.0f,
                    val => m_ScoreLbl.transform.localScale = Vector3.one * val,
                    2.0f, 0.5f).SetEase(m_ScoreLblAnimAC)
                .SetAutoKill(false);

            MainGameMgr.GetBoard_F().m_OnColorChangedE += SetScoreLblColorGradually_F;
        }

        protected override void ScoreLblUpdate_F()
        {
            base.ScoreLblUpdate_F();
            m_ScoreLblAnimT.Restart();
        }

        public void IncrementLblDisplay_F(Vector3 pos, Color color, int count)
        {
            if(m_IncrementSeq.IsActive()) m_IncrementSeq.Kill();

            m_IncrementLbl.gameObject.SetActive(true);
            m_IncrementLbl.transform.localPosition = Vector3.zero;
            m_IncrementLbl.transform.localScale = Vector3.zero;
            m_IncrementLbl.color = color;
            m_IncrementLbl.transform.parent.position = m_IncrementLbl.transform.parent.position.With(x:pos.x, z:pos.z);
            m_IncrementLbl.text = $"+{count}";
            m_IncrementSeq = DOTween.Sequence();
            m_IncrementSeq.Append(m_IncrementLbl.transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutBack));
            m_IncrementSeq.AppendInterval(1.0f);
            m_IncrementSeq.Append(m_IncrementLbl.transform.DOScale(0.0f, 0.3f).SetEase(Ease.InBack));
            m_IncrementSeq.Insert(0.0f, m_IncrementLbl.transform.DOMoveY(0.8f, 1.8f));
            m_IncrementSeq.AppendCallback(() => m_IncrementLbl.gameObject.SetActive(false));
        }

        public void SetScoreLblColor_F(Color color)
        {
            if (ColorChangingT.IsActive()) ColorChangingT.Kill();
            m_ScoreLbl.color = color;
        }

        public void SetScoreLblColorGradually_F(Color color)
        {
            if(ColorChangingT.IsActive()) ColorChangingT.Kill();
            ColorChangingT = m_ScoreLbl.DOColor(color, 0.5f);
        }
    }
}
