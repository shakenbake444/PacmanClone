
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
    public Vector3 startposition = new Vector3 (0f, -9.5f, 0f);
    public Quaternion startAngle;
    

    private void Awake()
    {
        pelletObj = GameObject.FindGameObjectsWithTag("Pellet_Tag");
        startAngle = pacman.transform.rotation;
        
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
        pacman.movement.SetDirection(Vector2.zero, false);
        pacman.transform.position = startposition;
        pacman.transform.localRotation = startAngle;
        
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
            resetSequence = false;
        }
    }

    private void Flip()
    {
        resetSequence = !resetSequence;
    }
}
