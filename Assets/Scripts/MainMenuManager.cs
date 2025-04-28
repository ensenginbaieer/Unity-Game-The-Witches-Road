using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public SceneTransitionManager transitionManager;
    public void StartGame()
    {
        transitionManager.StartFade("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game"); 
    }
}
