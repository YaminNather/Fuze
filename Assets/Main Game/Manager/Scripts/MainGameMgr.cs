using System.Collections;
using UnityEngine;
using BallPawnStuff;
using CinemachineUtilites;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#endif

namespace MainGameMgrStuff
{
    [DefaultExecutionOrder(-10)]
    public partial class MainGameMgr : MonoBehaviour
    {
        #region Variables
        static private MainGameMgr s_Instance;
        private ColorsMgr m_ColorsMgr;
        private BallsSpawner m_BallsSpawner;
        private ScoreMgr m_ScoreMgr;

        private MainGameCameraMgr m_Camera;
        private BoardMgr m_Board;
        private RippleEffectMgr m_RippleEffect;
        private BallCountLblMgr m_BallCountLbl;

        private BallPlayerController m_BallPlayerController;
        [SerializeField] private Transform m_BallSpawnLocationTrans;

        [SerializeField] private SerializedInt m_BallCount;
        #endregion

        private void Awake()
        {
            s_Instance = this;
            m_ColorsMgr = GetComponent<ColorsMgr>();
            m_BallsSpawner = GetComponent<BallsSpawner>();
            m_Board = FindObjectOfType<BoardMgr>();
            m_ScoreMgr = GetComponent<ScoreMgr>();
            m_RippleEffect = FindObjectOfType<RippleEffectMgr>();
            m_Camera = FindObjectOfType<MainGameCameraMgr>();
            m_BallPlayerController = FindObjectOfType<BallPlayerController>();
            m_BallCountLbl = FindObjectOfType<BallCountLblMgr>();

            m_BallCount.SetValueToInitialValue_F();
        }

        private void Start()
        {
            if (!SceneManager.GetSceneByName("Global0_Scene").isLoaded)
            {
                GlobalSceneChecker globalSceneChecker = gameObject.AddComponent<GlobalSceneChecker>();
                globalSceneChecker.OnLoadCompleteE += () => StartCoroutine(Start_IEF());
                globalSceneChecker.LoadGlobalScene_F();
            }

            IEnumerator Start_IEF()
            {
                yield return null;

                //Setting Ball Texture.
                m_BallsSpawner.SetBallTexture_F(
                    GlobalMgr.GetBallVariant_F(Random.Range(0, GlobalMgr.GetBallVariantsCount_F())).GetBallSprite_F().texture
                    );

                //Setting random colors for existing balls.
                foreach (BallPawn ballPawn in FindObjectsOfType<BallPawn>())
                    ballPawn.SetColor_F(MainGameMgr.GetColorsMgr_F().GetRandomColor_F());

                //Move Camera from the top to game view.
                m_Camera.BlendToGameVCamera_F(2.0f);
                yield return new WaitForSeconds(2.0f);
                

                //Setup launch ball.
                SetupLaunchBall_F(0.0f);
            }
        }

        private void SetupLaunchBall_F(float delayTime) => StartCoroutine(SetupLaunchBall_IEF(delayTime));

        private IEnumerator SetupLaunchBall_IEF(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            Color color = m_ColorsMgr.GetRandomColor_F();

            BallPawn ballPawn;
            float ballSelect = Random.Range(0.0f, 1.0f);
            if (ballSelect < 0.8f)
            {
                ballPawn = m_BallsSpawner.SpawnNormalBall_F(
                    m_BallSpawnLocationTrans.position,
                    color
                );
            }
            else if (ballSelect >= 0.8f && ballSelect < 0.9f)
                ballPawn = m_BallsSpawner.SpawnBlackBall_F(m_BallSpawnLocationTrans.position);
            else
                ballPawn = m_BallsSpawner.SpawnAnyColorBallPawn_F(m_BallSpawnLocationTrans.position);

            ballPawn.m_OnLaunchE += OnLaunch_EF;
            
            m_BallPlayerController.Possess_F(ballPawn);

            m_BallCount.SetValue_F(m_BallCount.GetValue_F() - 1);

            m_Board.SetColor_F(color);
            m_BallCountLbl.SetColor_F(color);
        }

        private void OnLaunch_EF()
        {
            if (m_BallCount.GetValue_F() > 0)
                SetupLaunchBall_F(1.0f);
            else
                StartCoroutine(CheckForEndGame_IEF());
        }

        public void AddBalls_F(int count) => m_BallCount.SetValue_F(m_BallCount.GetValue_F() + count);

        private IEnumerator CheckForEndGame_IEF()
        {
            float timer = 3.0f;
            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;

                if (m_BallCount.GetValue_F() != 0)
                {
                    SetupLaunchBall_F(0.0f);
                    yield break;
                }

                yield return null;
            }

            EndGame_F();
        }

        private void EndGame_F()
        {
            StartCoroutine(endGame_IEF());

            IEnumerator endGame_IEF()
            {
                Debug.Log("Game Ended");
                StartCoroutine(destroyAllBalls_IEF());
                yield return new WaitForSeconds(0.2f);
                m_Camera.DoEndGameTransition_F();
                yield return new WaitForSeconds(4.0f);
                SceneManager.LoadScene("Main Menu0_Scene");
            }

            IEnumerator destroyAllBalls_IEF()
            {
                NormalBallPawn[] ballPawns = FindObjectsOfType<NormalBallPawn>();
                foreach (NormalBallPawn ballPawn in ballPawns)
                {
                    m_RippleEffect.DOBurst_F(ballPawn.transform.position, ballPawn.GetColor_F(),10.0f);
                    m_Board.SetColor_F(ballPawn.GetColor_F());
                    m_BallCountLbl.SetColor_F(ballPawn.GetColor_F());
                    m_ScoreMgr.ScoreAdd_F(3);
                    m_ScoreMgr.IncrementLblDisplay_F(ballPawn.transform.position, ballPawn.GetColor_F(),3);
                    ballPawn.Destroy_F();
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }



        static public MainGameMgr GetInstance_F() => s_Instance;
        static public ColorsMgr GetColorsMgr_F() => s_Instance.m_ColorsMgr;
        static public BallsSpawner GetBallsSpawner_F() => s_Instance.m_BallsSpawner;
        static public BoardMgr GetBoard_F() => s_Instance.m_Board;
        static public ScoreMgr GetScoreMgr_F() => s_Instance.m_ScoreMgr;
        static public RippleEffectMgr GetRippleEffect_F() => s_Instance.m_RippleEffect;
        static public MainGameCameraMgr GetCamera_F() => s_Instance.m_Camera;
        static public BallPlayerController GetBallPlayerController_F() => s_Instance.m_BallPlayerController;

        public int GetBallCount_F() => m_BallCount.GetValue_F();
    }

#if UNITY_EDITOR
    public partial class MainGameMgr : MonoBehaviour
    {
        [MenuItem("Custom/Cycle Play Mode Options")]
        private static void CyclePlayModeOptions_F() 
            => UnityEditor.EditorSettings.enterPlayModeOptionsEnabled = !UnityEditor.EditorSettings.enterPlayModeOptionsEnabled;

        [MenuItem("Custom/Scenes/Main Game")]
        private static void OpenMainGameScene_F()
            => UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/Main Game/Scenes/Main Game0_Scene.unity", OpenSceneMode.Single);
    }
#endif
}
