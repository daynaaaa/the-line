using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayBestScore : MonoBehaviour
{
    [SerializeField] 
    private Text _title;
    public GameObject displayBest;

    void Start()
    {
        _title = displayBest.GetComponent<Text>();
    }

    void Update()
    {
        _title.text = "" + RoadGeneration.score;
    }
}
