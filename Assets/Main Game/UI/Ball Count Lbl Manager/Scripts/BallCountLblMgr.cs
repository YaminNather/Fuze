using System.Collections;
using System.Collections.Generic;
using BallPawnStuff;
using MainGameMgrStuff;
using UnityEngine;
using UnityEngine.UI;

public class BallCountLblMgr : MonoBehaviour
{
    #region Variables
    private Text m_BallCountLbl;
    [SerializeField] private SerializedInt m_BallCount;
    #endregion

    private void Awake()
    {
        m_BallCountLbl = GetComponent<Text>();
        m_BallCount.m_OnChangedE += OnBallCountChanged_EF;
    }

    private void Start()
    {
        m_BallCountLbl.text = "x" + m_BallCount.GetValue_F();
    }

    public void SetColor_F(Color color) => m_BallCountLbl.color = color;

    private void OnBallCountChanged_EF(int ballCount)
    {
        m_BallCountLbl.text = $"{ballCount}x";
    }

    private void OnDestroy()
    {
        m_BallCount.m_OnChangedE -= OnBallCountChanged_EF;
    }
}
