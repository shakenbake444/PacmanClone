
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[]          ghost;
    public SpriteRenderer[] ghostChildren;
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
    public NodeScript       _node;

    //public GhostFrightened ghostFrightened;
        

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

        //cameraAudioSource.PlayOneShot(gameStartMusic);
        
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
        
        ghost.ghostFrightened.white.enabled = false;
        Physics2D.IgnoreCollision(pacman.GetComponent<Collider2D>(), ghost.GetComponent<Collider2D>(), true);

        //FindHome(ghost);
        
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

        foreach (Ghost enemy in ghost)
        {
            if (enemy.ghostChase.enabled)
            {
                enemy.ghostChase.Disable();
                enemy.ghostFrightened.Enable(enemy.ghostFrightened.duration);
            } else {
                enemy.ghostFrightened.Enable(enemy.ghostFrightened.duration);
            }
        }
        
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

    private void FindHome(Ghost _ghost)
    {
        _ghost.ghostChase.enabled = false;
        _ghost.ghostScatter.enabled = false;
        //_ghost.movement.speed = 100f;

        
        // if (_node != null) 
        // {
        //     Debug.Log("inloop");
        //     Vector2 direction = Vector2.zero;
        //     float minDistance = float.MaxValue;

        //     foreach (Vector2 availableDirection in _node.availableDirections)
        //     {
        //         Vector3 newPosition =_ghost.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
        //         float distance = (_ghost.home - newPosition).sqrMagnitude;
                
        //         if (distance < minDistance)
        //         {
        //             direction = availableDirection;
        //             minDistance = distance;
        //         }

        //     }

        //     _ghost.movement.SetDirection(direction); 

        // }
        //Physics2D.IgnoreCollision(pacman.GetComponent<Collider2D>(), _ghost.GetComponent<Collider2D>(), false);
    }

    //public void StartCoroutine(Foo(p1,p2));
 
//     public void Foo(int param1, int param2)
//     {
//         yield WaitForSeconds(timer);
// //Do stuff
//     }
}
