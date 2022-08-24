
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public Ghost ghost;
    public float duration;
    // Start is called before the first frame update
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }
    
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    // Update is called once per frame
    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
