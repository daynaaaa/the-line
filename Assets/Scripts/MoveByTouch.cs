using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoveByTouch : MonoBehaviour
{
    public static bool start;
    public GameObject player;
    public Text _title;
    void Update() {
        // check for touch input
        if (Input.touchCount > 0 && start) {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPosition.y < 1000)
                player.transform.position = new Vector2(touchPosition.x, player.transform.position.y);
        }
    }
    public void Move() {
        start = true;
        _title.text = "";
    }
}
