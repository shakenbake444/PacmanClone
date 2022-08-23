
using UnityEngine;

public class Pellets : MonoBehaviour
{
    public int points = 10;
    protected virtual void Eat()
    {
        
        FindObjectOfType<GameManager>().PelletEaten(this);
        //Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Eat();
        }
        
    }
}
