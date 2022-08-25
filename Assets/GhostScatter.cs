
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.ghostChase.PublicEnable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
  
        NodeScript node = other.GetComponent<NodeScript>();

        if (node != null && this.enabled && !this.ghost.ghostFrightened.enabled) {
            
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction) {

                index++;

                if (index >= node.availableDirections.Count) {
                    index = 0; 
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]); 
        }
    }
    //public override void Enable()
    //{
    //    this.gameObject.SetActive(false);
    //}
}
