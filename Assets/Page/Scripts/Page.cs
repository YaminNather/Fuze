using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UIUtililities.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UIUtililities.PageStuff
{
    [RequireComponent(typeof(GraphicRaycaster))]
    public class Page : MonoBehaviour
    {
        #region Variables
        private UIAnimationComponent[] m_UIAnimationComponents;

        protected Action OnOpen_E;
        protected Action OnClose_E;

        public bool Clickable
        {
            get => GetComponent<GraphicRaycaster>().enabled;
            set => GetComponent<GraphicRaycaster>().enabled = value;
        }
        #endregion

        public virtual void Open_F(Action onOpen_E = null)
        {
            gameObject.SetActive(true);
            Clickable = false;

            m_UIAnimationComponents = GetComponentsInChildren<UIAnimationComponent>(true);

            Sequence Seq_0 = DOTween.Sequence();

            int aLength = m_UIAnimationComponents.Length;
            foreach (UIAnimationComponent uiac in m_UIAnimationComponents)
            {
                if (uiac.Type == UIAnimationComponent.Type_EN.Exit) continue;

                uiac.EntryInitialize_F();
                Seq_0.Insert(uiac.StartTime, uiac.EntryTween_F());
            }

            Seq_0.AppendCallback(() =>
            {
                Clickable = true;
                OnOpen_E?.Invoke();
                onOpen_E?.Invoke();
            });
        }

        public virtual void Close_F(Page page = null, Action onClose_E = null, Action onOpen = null)
        {
            Clickable = false;

            m_UIAnimationComponents = GetComponentsInChildren<UIAnimationComponent>(true);
            float closeTime = MaxTimeGet_F();


            Sequence Seq_0 = DOTween.Sequence();

            int aLength = m_UIAnimationComponents.Length;
            foreach (UIAnimationComponent uiac in m_UIAnimationComponents)
            {
                if (uiac.Type == UIAnimationComponent.Type_EN.Entry) continue;

                uiac.ExitInitialize_F();
                Seq_0.Insert(closeTime - uiac.EndTime, uiac.ExitTween_F());
            }

            Seq_0.AppendCallback(() =>
            {
                OnClose_E?.Invoke();
                onClose_E?.Invoke();
                gameObject.SetActive(false);
                Clickable = true;
                page?.Open_F(OnOpen_E);
            });
        }

        private float MaxTimeGet_F()
        {
            float r = 0;
            foreach (UIAnimationComponent uiac in GetComponentsInChildren<UIAnimationComponent>(true))
                if (uiac.EndTime > r)
                    r = uiac.EndTime;

            return r;
        }
    }
}
