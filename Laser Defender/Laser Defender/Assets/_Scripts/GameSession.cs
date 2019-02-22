using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    private GameObject instance;
    private int score = 0;

    [SerializeField] Text scoreText;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        this.score += value;
    }

    private void Update()
    {
        scoreText.text = this.score.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
