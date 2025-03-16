using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayScore : MonoBehaviour
{
    [SerializeField] 
    private Text _title;
    public GameObject display;

    void Start()
    {
        _title = display.GetComponent<Text>();
    }

    void Update()
    {
        _title.text = "SCORE: " + RoadGeneration.score;
    }
}
