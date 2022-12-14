using UnityEngine;

public class Pacman : MonoBehaviour
{
    public AnimatedSprite deathSequence;
    public SpriteRenderer spriteRenderer;
    // try to remove new
    public new Collider2D collider;
    public Movement movement;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, transform.forward);

    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        
    }
}