using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UIUtililities.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Pages.MainPage
{
    [RequireComponent(typeof(Image))]
    public class FlickerUIAnim : UIAnimationComponent
    {
        #region Variables

        [Space(20.0f)]
        
        [SerializeField] private int m_Count;
        [SerializeField] private float m_GapTime;
        
        private Image m_Image;

        #endregion

        public override void OnSceneLoaded_F() { }

        public override void EntryInitialize_F()
        {
            m_Image = GetComponent<Image>();
            m_Image.color = m_Image.color.With(a:0.0f);
        }

        public override Tween EntryTween_F()
        {
            Sequence r = DOTween.Sequence();

            float dur = (1.0f / m_Count) - m_GapTime;

            for (int i = 0; i < m_Count; i++)
            {
                AppendTurnOnTweener_F(r, dur);
                AppendTurnOffCallback_F(r);
                r.AppendInterval(m_GapTime);
            }

            r.AppendCallback(() => m_Image.color = m_Image.color.With(a:1.0f));
            return r;
        }

        public override void ExitInitialize_F()
        {
            throw new System.NotImplementedException();
        }

        public override Tween ExitTween_F()
        {
            throw new System.NotImplementedException();
        }

        private void AppendTurnOnTweener_F(Sequence seq, float dur)
        {
            dur = CalcActualTimeFromNormalizedTime_F(dur);
            seq.Append(DOTween.To(() => 0.0f,
                val => m_Image.color = m_Image.color.With(a: val),
                1.0f, dur));
        }

        private void AppendTurnOffCallback_F(Sequence seq)
        {
            seq.AppendCallback(() => m_Image.color = m_Image.color.With(a:0.0f));
        }

        private float CalcActualTimeFromNormalizedTime_F(float normalizedTime) => normalizedTime * Duration;
    }
}
