using System.Collections;
using System.Collections.Generic;
using MainMenu.Pages.MainPage;
using MainMenu.Pages.StorePage;
using UIUtililities.PageStuff;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    [DefaultExecutionOrder(-10)]
    public partial class MainMenuMgr : MonoBehaviour
    {
        #region Variables
        static private MainMenuMgr s_Instance;

        static private Vector2 s_DefScreenResolution;

        [Header("Pages")]
        private Page m_MainPage;
        private Page m_StorePage;
        #endregion

        static MainMenuMgr()
        {
            s_DefScreenResolution = new Vector2(-1.0f, -1.0f);
        }

        private void Awake()
        {
            s_Instance = this;
            m_MainPage = GetComponentInChildren<MainPageMgr>(true);
            m_StorePage = GetComponentInChildren<StorePageMgr>(true);
        }

        private void Start()
        {
            if (!SceneManager.GetSceneByName("Global0_Scene").isLoaded)
            {
                GlobalSceneChecker globalSceneChecker = gameObject.AddComponent<GlobalSceneChecker>();
                globalSceneChecker.OnLoadCompleteE += start_EF;
                globalSceneChecker.LoadGlobalScene_F();
            }

            void start_EF()
            {
                m_MainPage.gameObject.SetActive(true);
                GetPage_F(EPages.Main).Open_F();

                if (s_DefScreenResolution == new Vector2(-1.0f, -1.0f))
                    s_DefScreenResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);

                const float divider = 1.5f;
                Debug.Log($"New Screen Resolution = {s_DefScreenResolution / divider}");
                Screen.SetResolution((int) (s_DefScreenResolution.x / divider), (int) (s_DefScreenResolution.y / divider),
                    FullScreenMode.ExclusiveFullScreen);
            }
        }

        static public Page GetPage_F(EPages ePage)
        {
            return ePage switch
            {
                EPages.Main => s_Instance.m_MainPage,
                EPages.Store => s_Instance.m_StorePage,
                _ => null
            };
        }

        static public MainMenuMgr GetInstance_F() => s_Instance;
        
        
        public enum EPages { Main, Store }
    }

    public partial class MainMenuMgr : MonoBehaviour
    {
        [MenuItem("Custom/Scenes/Main Menu")]
        private static void OpenMainMenuScene_F()
            => UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Main Menu/Scenes/Main Menu0_Scene.unity", OpenSceneMode.Single);
    }
}
