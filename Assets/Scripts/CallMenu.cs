using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallMenu : MonoBehaviour
{
    public void Menu() {
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
    }
}
