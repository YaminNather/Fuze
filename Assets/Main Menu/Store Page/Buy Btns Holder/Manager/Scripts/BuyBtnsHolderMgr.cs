using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.Pages.StorePage
{
    public class BuyBtnsHolderMgr : MonoBehaviour
    {
        private bool m_IsButtonsLoaded;

        public void SetupBuyBtns_F()
        {
            BuyBtnMgr buttonOne = transform.GetChild(0).GetComponent<BuyBtnMgr>();
            for (int i = 0; i < GlobalMgr.GetBallVariantsCount_F(); i++)
            {
                BuyBtnMgr button;
                if (i == 0)
                    button = buttonOne;
                else
                {
                    button = Instantiate(buttonOne.gameObject).GetComponent<BuyBtnMgr>();
                    button.transform.parent = transform;
                    button.transform.localScale = buttonOne.transform.localScale;
                    button.transform.localPosition = buttonOne.transform.localScale;
                }
                
                button.SetBallVariant_F(GlobalMgr.GetBallVariant_F(i));
                button.Refresh_F();

                m_IsButtonsLoaded = true;
            }
        }

        public bool GetIsButtonsLoaded_F() => m_IsButtonsLoaded;
    }
}
