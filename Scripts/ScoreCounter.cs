using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ScoreCounter : MonoBehaviour
{
    [Header("Dynamic")]
    public static int score = 0;
    private Text uiText;

    [SerializeField]
    private bool inGame;
    // Start is called before the first frame update
    void Start()
    {
        if (inGame)
        {
            uiText = GetComponent<Text>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            uiText.text = "Score: " + score.ToString("#,0");
        }
    }
}
