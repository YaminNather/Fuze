using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GlobalMgr : MonoBehaviour
{

    private void Awake()
    {
        s_Instance = this;
        m_GlobalData = FindObjectOfType<GlobalData>();
    }



    static private GlobalMgr s_Instance;

    private GlobalData m_GlobalData;

    static public GlobalMgr GetInstance_F() => s_Instance;

    static public GlobalData GetGlobalData_F() => s_Instance.m_GlobalData;
}

#if UNITY_EDITOR
public partial class GlobalMgr : MonoBehaviour
{
    private const string m_GLOBALSCENEPATH = "Assets/Global Scene/Scenes/Global0_Scene.unity";

    //[MenuItem("Custom/Scenes/Global Scene/Single", true)]
    //private static bool LoadGlobalSceneSingleValidator_F()
    //{
    //    bool isLoaded = SceneManager.GetSceneByName(m_GLOBALSCENEPATH).isLoaded;
    //    Debug.Log($"Is Global Scene loaded? {isLoaded}");
    //    return !isLoaded;
    //}

    [MenuItem("Custom/Scenes/Global Scene/Single")]
    private static void LoadGlobalSceneSingle_F() => EditorSceneManager.OpenScene(m_GLOBALSCENEPATH);

    //[MenuItem("Custom/Scenes/Global Scene/Additive", true)]
    //private static bool LoadGlobalSceneAdditiveValidator_F() => !SceneManager.GetSceneByName(m_GLOBALSCENEPATH).isLoaded;

    [MenuItem("Custom/Scenes/Global Scene/Additive")]
    private static void LoadGlobalSceneAdditive_F() => EditorSceneManager.OpenScene(m_GLOBALSCENEPATH, OpenSceneMode.Additive);
}
#endif