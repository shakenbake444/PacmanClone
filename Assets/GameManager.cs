
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    [SerializeField]
    public Pacman pacman;
    public Transform pellets;

    public int score; 
    public int lives {get; private set;}
    public int ghostMultiplier {get; private set;} = 1;

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

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + ghost.points * ghostMultiplier);
        ghostMultiplier++;
    }

    public void PacamanEaten()
    {

    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
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

    public void PelletEaten(Pellets pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(score + pellet.points);

        if(!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        // TODO: change ghost state
        CancelInvoke();        
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        PelletEaten(pellet);

    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    public void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }
}
