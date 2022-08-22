
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connecetion;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 currentPos = other.transform.position;
        currentPos.x = connecetion.position.x;
        other.transform.position = currentPos;
    }
}
