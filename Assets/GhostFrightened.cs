
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    public Pacman         pacman;

    private void OnDisable()
    {
        this.body.enabled = true;
        this.white.enabled = false;
        Physics2D.IgnoreCollision(pacman.GetComponent<Collider2D>(), ghost.GetComponent<Collider2D>(), false);

    }
    private void OnEnable()
    {   
        body.enabled = false;
        white.enabled = true;
        ghost.ghostScatter.enabled = true;
        ghost.ghostChase.enabled = false;

        CancelInvoke();
        Invoke(nameof(Normal), ghost.ghostFrightened.duration);
        Invoke(nameof(SwitchState), ghost.ghostScatter.duration + 0.5f);
    }

    private void Update()
    {
        if (ghost.ghostChase.enabled)
        {
            ghost.ghostScatter.Enable(ghost.ghostScatter.duration);
            ghost.ghostChase.Disable();
        }
    }

    public void Normal()
    {   
        body.enabled = true;
        white.enabled = false;
        ghost.ghostScatter.enabled = true;    

    }

    public void SwitchState()
    {
        ghost.ghostScatter.Disable();    
        ghost.ghostChase.Enable(ghost.ghostChase.duration);    
        ghost.ghostFrightened.Disable();
    }


}
