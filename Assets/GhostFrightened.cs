
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    // private void OnDisable()
    // {
    //     ghost.ghostScatter.enabled = true;
    // }
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
            ghost.ghostScatter.PublicEnable();
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
        ghost.ghostChase.PublicEnable();    
        ghost.ghostFrightened.Disable();
    }


}
