
using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    
    public float duration   = 8.0f;
    public int points     = 20;

    public void PowerPelletEat()
    {
       FindObjectOfType<GameManager>().PowerPelletEaten(this);
        //Destroy(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            PowerPelletEat();
        }
        
    }

}
