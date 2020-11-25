using System.Collections;
using System.Collections.Generic;
using MainMenu.Pages.MainPage;
using UIUtililities.PageStuff;
using UnityEngine;

namespace MainMenu
{
    [DefaultExecutionOrder(-10)]
    public class MainMenuMgr : MonoBehaviour
    {
        #region Variables
        static private MainMenuMgr s_Instance;

        static private Vector2 s_DefScreenResolution;

        private Page m_MainPage;
        #endregion

        static MainMenuMgr()
        {
            s_DefScreenResolution = new Vector2(-1.0f, -1.0f);
        }

        private void Awake()
        {
            s_Instance = this;
            m_MainPage = GetComponentInChildren<MainPageMgr>(true);
        }

        private void Start()
        {
            m_MainPage.gameObject.SetActive(true);
            GetPage_F(EPages.Main).Open_F();

            if(s_DefScreenResolution == new Vector2(-1.0f, -1.0f))
                s_DefScreenResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

            const float divider = 1.5f;
            Debug.Log($"New Screen Resolution = {s_DefScreenResolution / divider}");
            Screen.SetResolution((int)(s_DefScreenResolution.x / divider), (int)(s_DefScreenResolution.y / divider), FullScreenMode.ExclusiveFullScreen);
        }

        static public Page GetPage_F(EPages ePage)
        {
            return ePage switch
            {
                EPages.Main => s_Instance.m_MainPage,
                _ => null
            };
        }

        static public MainMenuMgr GetInstance_F() => s_Instance;
        
        
        public enum EPages { Main }
    }
}
