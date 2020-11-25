using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardMgr : MonoBehaviour
{
    #region Variables
    private MeshRenderer m_MeshRenderer;

    private Color m_Color;
    private Tweener m_ColorT;

    public System.Action<Color> m_OnColorChangedE;
    #endregion

    private void Awake()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();
    }

    public Color GetColor_F() => m_Color;

    public void SetColor_F(Color color)
    {
        //m_MeshRenderer.material.SetColor("_BaseColor", color);

        if (m_ColorT.IsActive()) m_ColorT.Kill();
        m_ColorT = m_MeshRenderer.material.DOColor(color, "_BaseColor", 0.5f);

        m_Color = color;

        m_OnColorChangedE?.Invoke(color);
    }
}
