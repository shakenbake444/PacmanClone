
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement         movement;
    public GhostHome        ghostHome;
    public GhostChase       ghostChase;
    public GhostFrightened  ghostFrightened;
    public GhostScatter     ghostScatter;
    public GhostBehavior    initialBehavior;
    public Transform        target;
    public int points = 200;

    private void Awake()
    {
        movement        = GetComponent<Movement>();
        ghostHome       = GetComponent<GhostHome>();
        ghostChase      = GetComponent<GhostChase>();
        ghostScatter    = GetComponent<GhostScatter>();
        ghostFrightened = GetComponent<GhostFrightened>();

    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();

        this.ghostFrightened.Disable();
        this.ghostChase.Disable();
        this.ghostScatter.Enable(initialBehavior.duration);
        
        if (this.ghostHome != this.initialBehavior) {
            this.ghostHome.Disable();
        }

        if (this.initialBehavior == null) {
            this.initialBehavior.Enable(initialBehavior.duration);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
            if (this.ghostFrightened.enabled) {
                FindObjectOfType<GameManager>().GhostEaten(this);
            } else {
                FindObjectOfType<GameManager>().PacamanEaten();
            }
        }
    }
}
