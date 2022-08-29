using UnityEngine;
using System.Collections;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();

    }

    private void OnDisable()
    {
        StartCoroutine(ExitTransition());
    }

    private IEnumerator ExitTransition()
    {   
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.rb.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = transform.position;
        float duration  = 0.5f; 
        float elapsed   = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost.movement.rb.isKinematic = false;
        this.ghost.movement.enabled = true;

    }

}
