
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public float speedmult;
    public Vector2 initdirect;
    public LayerMask obstlayer;
    public Rigidbody2D rb;
    public Vector2 direction {get; private set;}
    public Vector2 nextdirection {get; private set;}
    public Vector3 startingposition {get; private set;}
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startingposition = transform.position;
    }

    private void Start()
    {
            ResetState();
    }

    private void Update()
    {
        if (nextdirection != Vector2.zero)
        {
            SetDirection(nextdirection);
        }
    }

    public void ResetState()
    {
        direction = initdirect;
        nextdirection = Vector2.zero;
        transform.position = startingposition;
        rb.isKinematic = false;
        enabled = true;
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translate = direction * speed * speedmult * Time.fixedDeltaTime;
        rb.MovePosition(position + translate);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if(forced || !Occupied(direction))
        {
            this.direction = direction;
            nextdirection = Vector2.zero;
        } else {
            nextdirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, obstlayer);
        return hit.collider != null;
    }

    public void Enable()
    {
        this.speed = 8.0f;
    }

    public void Disable()
    {
        this.speed = 0.0f;
    }

}
