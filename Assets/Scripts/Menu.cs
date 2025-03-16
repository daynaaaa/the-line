using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // KeepGoing button
    public void KeepGoing() {
        SceneManager.UnloadSceneAsync(0);
        Time.timeScale = 1;
    }

    // TryAgain button
    public void TryAgain() {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1);
        MoveByTouch.start = false;
    }
}
