using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UIUtililities.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Pages.MainPage
{
    public class TitleEntryAnim : UIAnimationComponent
    {
        #region Variables
        private Image[] m_ChildImages;
        #endregion

        public override void OnSceneLoaded_F()
        {
            m_ChildImages = GetComponentsInChildren<Image>();
        }

        public override void EntryInitialize_F()
        {
            foreach (Image image in m_ChildImages)
                image.color = image.color.With(a: 0f);
        }

        public override Tween EntryTween_F()
        {
            Sequence seq = DOTween.Sequence();
            addFlickeringSequence_F(0.0f, 0.2f, 0.01f, 2, 0);
            addFlickeringSequence_F(0.4f, 0.7f, 0.01f, 3, 3);

            addTurnOnSequence_F(0.8f, 0.85f, 2);
            addTurnOnSequence_F(0.85f, 0.9f, 0);
            addTurnOnSequence_F(0.9f, 0.95f, 1);
            addTurnOnSequence_F(0.95f, 1.0f, 3);

            return seq;


            void addTurnOnSequence_F(float startTime, float endTime, int childIndex)
            {
                startTime = calcExactTimeFromNormalizedTime_F(startTime);
                endTime = calcExactTimeFromNormalizedTime_F(endTime);
                float duration = endTime - startTime;
                seq.Insert(startTime,
                    DOTween.To(() => 0.0f,
                        val => m_ChildImages[childIndex].color = m_ChildImages[childIndex].color.With(a: val),
                        1.0f, duration));
            }

            void addTurnOffSequence_F(float atTime, int childIndex)
            {
                atTime = calcExactTimeFromNormalizedTime_F(atTime);
                seq.InsertCallback(atTime,
                    () => m_ChildImages[childIndex].color = m_ChildImages[childIndex].color.With(a: 0f));
            }

            void addFlickeringSequence_F(float startTime, float endTime, float gapTime, int count, int childIndex)
            {
                float dur = (endTime - startTime) / count;
                dur -= gapTime;

                for (int i = 0; i < count; i++)
                {
                    float start = startTime + ((dur + gapTime) * i);
                    addTurnOnSequence_F(start, start + dur, childIndex);
                    addTurnOffSequence_F(start + dur, childIndex);
                }
            }

            float calcExactTimeFromNormalizedTime_F(float normalizedTime) => normalizedTime * Duration;
        }

        public override void ExitInitialize_F()
        {

        }

        public override Tween ExitTween_F()
        {
            throw new NotImplementedException();
        }
    }
}
