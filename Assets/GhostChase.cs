using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.ghostScatter.PublicEnable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        NodeScript node = other.GetComponent<NodeScript>();

        if (node != null && this.enabled && !this.ghost.ghostFrightened.enabled) {
            
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;
                
                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }

            }

            this.ghost.movement.SetDirection(direction); 

        }
    }
}
