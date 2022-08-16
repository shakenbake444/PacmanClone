using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    [SerializeField]
    public Pacman pacman;


    public Transform pellets;
    public int score {get; private set;}
    public int lives {get; private set;}

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void SetScore(int Score)
    {
        this.score = score;
    }

    private void SetLives(int Lives)
    {
        this.lives = lives;
    }

    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghost.Length; i++ )
        {
            this.ghost[i].gameObject.SetActive(true);
        }

        this.pacman.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghost.Length; i++ )
        {
            this.ghost[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }
}
