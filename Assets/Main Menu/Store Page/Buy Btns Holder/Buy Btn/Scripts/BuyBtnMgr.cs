using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyBtnMgr : MonoBehaviour
{

    public void Refresh_F()
    {
        if (true) // Supposed to be checking if ball owned or not here.
        {
            m_BallIcon.color = Color.black;
            m_BallTextureIcon.sprite = m_BallVariant.GetBallSprite_F();
            m_CurrencyValueLbl.gameObject.SetActive(false);
            m_CurrencyIconImage.gameObject.SetActive(false);
        }
        else
        {
            m_BallIcon.color = Color.grey;
            m_BallIcon.sprite = null;
            m_CurrencyValueLbl.gameObject.SetActive(true);
            m_CurrencyIconImage.gameObject.SetActive(true);
        }
    }


    [SerializeField] private Image m_BallIcon;
    [SerializeField] private Image m_BallTextureIcon;
    [SerializeField] private Text m_CurrencyValueLbl;
    [SerializeField] private Image m_CurrencyIconImage;

    private BallVariant m_BallVariant;

    public BallVariant GetBallVariant_F() => m_BallVariant;
    public void SetBallVariant_F(BallVariant value) => m_BallVariant = value;
}
