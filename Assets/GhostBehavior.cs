
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public Ghost ghost;
    public float duration;
    // Start is called before the first frame update
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
    }
    
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public void PublicEnable()
    {
        this.enabled = true;
    }

    // Update is called once per frame
    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }
}
