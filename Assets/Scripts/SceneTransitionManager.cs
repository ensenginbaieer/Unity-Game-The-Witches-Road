using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;
    private string nextSceneName;

    public void StartFade(string sceneName)
    {
        nextSceneName = sceneName;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        Color color = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
