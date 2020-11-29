using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UIUtililities.PageStuff;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace MainMenu.Pages.MainPage
{
    public class MainPageMgr : Page
    {
        #region Variables
        [Header("Button References")]
        [SerializeField] private RectTransform m_BallImageRTrans;
        [SerializeField] private Transform[] m_ButtonToRotateTranss;

        [SerializeField] private Image[] m_TitleImages;
        private Sequence m_TitleAnimSeq;
        #endregion

        public override void Open_F(Action onOpen_E = null)
        {
            onOpen_E += StartBallMovement_F;
            onOpen_E += StartTitleFlicker_F;
            base.Open_F(onOpen_E);
        }

        public override void Close_F(Page page = null, Action onClose_E = null, Action onOpen = null)
        {
            if(m_TitleAnimSeq.IsActive()) m_TitleAnimSeq.Kill();
            base.Close_F(page, onClose_E, onOpen);
        }

        private void StartBallMovement_F()
        {
            Sequence seq = DOTween.Sequence();
            float ballMovementTime = 3.0f;
            Vector3 rotationEndValue = Vector3.forward * (Mathf.Sign(m_BallImageRTrans.anchoredPosition.x) * -1.0f * -360.0f * 1.0f);

            //Ball Animation.
            seq.Append(m_BallImageRTrans.DOAnchorPosX(m_BallImageRTrans.anchoredPosition.x * -1.0f, ballMovementTime));
            seq.Insert(0.0f, m_BallImageRTrans.DORotate(rotationEndValue, ballMovementTime, RotateMode.FastBeyond360));

            //Buttons Animation.
            foreach (Transform buttonTrans in m_ButtonToRotateTranss)
                seq.Insert(0.0f, buttonTrans.DORotate(rotationEndValue, ballMovementTime, RotateMode.FastBeyond360));

            //Setting for next BallMovement.
            seq.AppendInterval(Random.Range(5.0f, 10.0f));
            seq.AppendCallback(StartBallMovement_F);
        }

        private void StartTitleFlicker_F()
        {
            if(m_TitleAnimSeq.IsActive()) m_TitleAnimSeq.Kill();

            Image image = m_TitleImages[Random.Range(0, m_TitleImages.Length)];
            m_TitleAnimSeq = DOTween.Sequence();
            m_TitleAnimSeq.AppendCallback(() => image.color = image.color.With(a:0f));
            m_TitleAnimSeq.AppendInterval(Random.Range(0.0f, 0.1f));
            m_TitleAnimSeq.Append(image.DOFade(1.0f, 0.1f));
            m_TitleAnimSeq.AppendInterval(Random.Range(1.0f, 2.0f));
            m_TitleAnimSeq.AppendCallback(StartTitleFlicker_F);
        }

        public void PlayBtn_BEF() => SceneManager.LoadScene("Main Game0_Scene");

        public void StoreBtn_BEF() => MainMenuMgr.GetPage_F(MainMenuMgr.EPages.Store).Open_F();
    }
}
