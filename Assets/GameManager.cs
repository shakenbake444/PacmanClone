
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[]          ghost;
    [SerializeField]
    public Pacman           pacman;
    public Transform        pellets;

    public PowerPellet      powerPellet;

    private GameObject[]    pelletObj;
    public bool             resetSequence; 

    public int              pelletCount;
    public int              score; 
    public int              lives;
    public int              ghostMultiplier {get; private set;} = 1;
    public Vector3          startposition = new Vector3 (0f, -9.5f, 0f);
    public Quaternion       startAngle;

    public AudioSource      pacmanAudioSource;
    public AudioSource      cameraAudioSource;
    public AudioClip[]      pelletEatenClip;
    public AudioClip        gameStartMusic;
    public bool             pelletEatenSoundIndex;
    public PlayPowerPelletSound playPowerPelletSound;

    private void Awake()
    {
        pelletObj = GameObject.FindGameObjectsWithTag("Pellet_Tag");
        startAngle = pacman.transform.rotation;
        NewGame();

        playPowerPelletSound = FindObjectOfType<PlayPowerPelletSound>();
        
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);

        cameraAudioSource.PlayOneShot(gameStartMusic);
        
        //NewRound();
    }

    private void SetScore(int Score)
    {
        this.score = Score;
    }

    private void SetLives(int Lives)
    {
        lives = Lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        SetScore(this.score + ghost.points * ghostMultiplier);
        ghostMultiplier++;
    }

    public void PacamanEaten()
    {
        lives--;

        if (lives < 1){
            GameOver();
        }

        pacman.gameObject.SetActive(false);

        Invoke(nameof(ResetPacman), 3.0f);

    }

    private void NewRound()
    {
        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghost.Length; i++ )
        {
            this.ghost[i].ResetState();
        }
        
        foreach (GameObject obj in pelletObj)
        {
            obj.SetActive(true);
        }
        
        ResetPacman();
        
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

        if (pelletEatenSoundIndex) {
            pacmanAudioSource.PlayOneShot(pelletEatenClip[0]);
        } else {
            pacmanAudioSource.PlayOneShot(pelletEatenClip[1]);
        }
        
        pelletEatenSoundIndex = !pelletEatenSoundIndex;
        // hello 
    }


    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        //playPowerPelletSound.PlayPowerPelletSoundMethod();
        PlaySoundSingleton.Instance.PlaySound();
        CancelInvoke(); 
        
        powerPellet.gameObject.SetActive(false);
        SetScore(score + powerPellet.points);
        // TODO: change ghost state

        Invoke(nameof(ResetGhostMultiplier), powerPellet.duration);
        
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

        if (cameraAudioSource.isPlaying) {
            pacman.movement.Disable();
            foreach (Ghost enemy in ghost) {
                enemy.movement.Disable();
            }            
        } else {
            pacman.movement.Enable();
            foreach (Ghost enemy in ghost) {
                enemy.movement.Enable();
            }
        }
    }

    private void Flip()
    {
        resetSequence = !resetSequence;
    }

    private void ResetPacman()
    {
        pacman.gameObject.SetActive(true);
        pacman.movement.SetDirection(Vector2.zero, false);
        pacman.transform.position = startposition;
        pacman.transform.localRotation = startAngle;
    }

}
