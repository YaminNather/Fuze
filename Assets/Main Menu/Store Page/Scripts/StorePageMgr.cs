using System;
using System.Collections;
using System.Collections.Generic;
using UIUtililities.PageStuff;
using UnityEngine;

namespace MainMenu.Pages.StorePage
{
    public class StorePageMgr : Page
    {
        #region Variables
        [SerializeField] private BuyBtnsHolderMgr m_BuyBtnsHolder;
        #endregion
        
        public override void Open_F(Action onOpen_E = null)
        {
            if (!m_BuyBtnsHolder.GetIsButtonsLoaded_F())
                m_BuyBtnsHolder.SetupBuyBtns_F();

            base.Open_F(onOpen_E);
        }
    }
}
