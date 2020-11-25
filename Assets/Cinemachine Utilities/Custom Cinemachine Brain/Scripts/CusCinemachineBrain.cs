using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace CinemachineUtilites
{
    public class CusCinemachineBrain : MonoBehaviour
    {
        #region Variables
        protected CinemachineBrain m_CinemachineBrain;
        protected CinemachineVirtualCamera m_CurCamera;
        protected Tweener ShakeT;

        #endregion

        protected virtual void Awake()
        {
            m_CinemachineBrain = GetComponent<CinemachineBrain>();
        }

        protected virtual void Start()
        {
            m_CurCamera = GetComponent<CinemachineBrain>().ActiveVirtualCamera as CinemachineVirtualCamera;
        }

        public virtual void BlendToCam_F(CinemachineVirtualCamera vCamera, float transitionTime = 1.0f)
        {
            if (vCamera == null) throw new UnityException("Argument vCamera cannot be null!!!");
            if (vCamera == m_CurCamera) return;

            m_CinemachineBrain.m_DefaultBlend.m_Time = transitionTime;
            StartCoroutine(blendToCam_IEF());

            IEnumerator blendToCam_IEF()
            {
                yield return null;
                m_CurCamera.m_Priority = 0;
                vCamera.m_Priority = 10;
                m_CurCamera = vCamera;
            }
        }

        public virtual void DOShake_F(float intensity, float frequency, float time)
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                m_CurCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

            if (ShakeT.IsActive()) ShakeT.Kill();
            ShakeT = DOTween.To(() => 0.0f, val => { }, 0.0f, time)
                .OnComplete(() => cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f);
        }





        public CinemachineVirtualCamera GetCurCamera_F() => m_CurCamera;
    }
}
