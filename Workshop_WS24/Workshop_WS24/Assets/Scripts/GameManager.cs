using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    public int score = 0;
    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game started");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {

        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public void EndGame()
    {
        isGameOver = true;
        Debug.Log("Game over!");
    }
}
