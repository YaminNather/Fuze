using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSceneChecker : MonoBehaviour
{
    public System.Action OnLoadCompleteE;

    public void LoadGlobalScene_F()
    {
        StartCoroutine(loadGlobalScene_IEF());

        IEnumerator loadGlobalScene_IEF()
        {
            AsyncOperation loadSceneAsyncOp = SceneManager.LoadSceneAsync("Global0_Scene", LoadSceneMode.Additive);
            while (!loadSceneAsyncOp.isDone) yield return null;
            OnLoadCompleteE?.Invoke();
            Destroy(this);
        }
    }
}
