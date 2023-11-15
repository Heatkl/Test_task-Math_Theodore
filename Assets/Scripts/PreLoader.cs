using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoader : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(PreloadScene("SampleScene"));
    }


    //Coroutine for asynchronously loading a scene during preloader rendering
    IEnumerator PreloadScene(string level)
    {
        yield return new WaitForSeconds(3f); //Delay only for preloader demonstration
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        yield return async;
        Debug.Log("Loading complete");
    }
}
