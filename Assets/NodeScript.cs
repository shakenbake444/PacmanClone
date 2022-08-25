using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    public List<Vector2> availableDirections;
    public LayerMask obstacle;

    private void Start()
    {
        availableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.up);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacle);

        if (hit.collider == null) {
            this.availableDirections.Add(direction);
        }
    }
}
