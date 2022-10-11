using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    private readonly string _mainScene = "Main";
    void Start()
    {
        StartCoroutine(ToMainScene());
    }

    IEnumerator ToMainScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(_mainScene);
    }
}
