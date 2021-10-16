using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        if (index<0)
        {
            index = SceneManager.GetActiveScene().buildIndex;
        }
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
