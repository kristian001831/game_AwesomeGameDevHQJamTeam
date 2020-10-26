using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private int _cardScene;

    public void StartButton()
    {
        SceneManager.LoadScene(_cardScene);
    }

    public void QuitButton()
    {
        Application.Quit();
        Quit();
    }

    public void Quit()
    {        
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
