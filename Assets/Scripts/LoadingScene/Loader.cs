using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class Loader
{
    private class LoadingMonoBehaviour : MonoBehaviour {}
    public enum Scene
    {
        MainMenu,
        SampleScene,
        Home,
        LoadingScene,
    }

    private static Action onLoaderCallBack;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene scene)
    {

        onLoaderCallBack = () =>
        {
            GameObject laodingGameObject = new GameObject("loading GameObject");
            laodingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        SceneManager.LoadScene(Scene.LoadingScene.ToString());

    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }

    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallBack()
    {
        if(onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
}
