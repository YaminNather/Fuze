using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using CinemachineUtilites;
using DG.Tweening;
using UnityEngine;

public class MainGameCameraMgr : CusCinemachineBrain
{
    #region Variables
    [SerializeField] private CinemachineVirtualCamera m_TopViewVCamera;
    [SerializeField] private CinemachineVirtualCamera m_GameVCamera;
    #endregion

    public void BlendToTopViewVCamera_F(float transitionTime = 1.0f) 
        => BlendToCam_F(m_TopViewVCamera, transitionTime);

    public void BlendToGameVCamera_F(float transitionTime = 1.0f) 
        => BlendToCam_F(m_GameVCamera, transitionTime);

    public void DoEndGameTransition_F()
    {
        BlendToTopViewVCamera_F(1.0f);
        Sequence seq = DOTween.Sequence();
        seq.Append(m_TopViewVCamera.transform.DOMoveY(2.25f, 2.0f));
        seq.AppendInterval(1.0f);
        seq.Append(m_TopViewVCamera.transform.DOMoveY(-1.0f, 1.0f));
    }
}
