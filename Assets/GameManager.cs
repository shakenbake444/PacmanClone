
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghost;
    [SerializeField]
    public Pacman pacman;
    public Transform pellets;

    private GameObject[] pelletObj;
    public bool resetSequence; 

    public int pelletCount;

    public int score; 
    public int lives {get; private set;}
    public int ghostMultiplier {get; private set;} = 1;

    private void Awake()
    {
        pelletObj = GameObject.FindGameObjectsWithTag("Pellet_Tag");
        
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void SetScore(int score)
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

        Debug.Log("New Round Called");

        ResetState();
    }

    private void ResetState()
    {
        //for (int i = 0; i < this.ghost.Length; i++ )
        //{
        //    this.ghost[i].gameObject.SetActive(true);
        //}
        
        foreach (GameObject obj in pelletObj)
        {
            obj.SetActive(true);
        }

        pacman.gameObject.SetActive(true);
        Invoke(nameof(Flip), 3.1f);
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

    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        // TODO: change ghost state
        CancelInvoke();        
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        PelletEaten(pellet);

    }

    public void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    private void Update()
    {
        pelletCount = GameObject.FindGameObjectsWithTag("Pellet_Tag").Length;
        
        if (pelletCount < 1) {
            if (!resetSequence) {
                pacman.gameObject.SetActive(false);
                Invoke(nameof(NewRound), 3.0f);  
            }
        
            resetSequence = true;
        
        }

    }

    private void Flip()
    {
        resetSequence = !resetSequence;
    }
}
