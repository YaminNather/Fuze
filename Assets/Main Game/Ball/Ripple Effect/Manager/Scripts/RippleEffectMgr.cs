using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MainGameMgrStuff;
using UnityEngine;

namespace BallPawnStuff
{
    public class RippleEffectMgr : MonoBehaviour
    {
        #region Variables
        private ParticleSystem m_RipplePS;
        private Light m_Light;
        private Tweener m_Light2DT;
        #endregion

        private void Awake()
        {
            m_RipplePS = GetComponentInChildren<ParticleSystem>();
            m_Light = GetComponentInChildren<Light>(true);

            m_Light.gameObject.SetActive(false);
        }

        public void DOBurst_F()
        {
            m_RipplePS.Play();

            MainGameMgr.GetCamera_F().DOShake_F(1.0f, 1.0f, 0.5f);

            m_Light.gameObject.SetActive(true);
            if(m_Light2DT.IsActive()) m_Light2DT.Kill();
            m_Light2DT = DOTween.To(() => 0.0f, val => {}, 0.0f, 0.5f).OnComplete(() => m_Light.gameObject.SetActive(false));
        }

        public void ColorSet_F(Color color)
        {
            //Changing PS's color.
            ParticleSystem.MainModule psMainModule = m_RipplePS.main;
            psMainModule.startColor = color;

            //Changing Light2D's color.
            m_Light.color = color;
        }
        
        public void DOBurst_F(Vector3 pos, Color color, float size = 5.0f)
        {
            transform.position = pos;
            ColorSet_F(color);
            ParticleSystem.MainModule mainModule = m_RipplePS.main;
            mainModule.startSize = size;
            DOBurst_F();
        }
    }
}
