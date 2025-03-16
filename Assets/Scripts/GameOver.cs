using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    // TryAgain button
    public void TryAgain() {
        SceneManager.LoadSceneAsync(1);
        MoveByTouch.start = false;
    }
}
