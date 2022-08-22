
using UnityEngine;

public class PowerPellet : Pellets
{
    
    public float duration = 8.0f;

    protected override void Eat()
    {
        FindObjectOfType<GameManager>().PelletEaten(this);
        //Destroy(this.gameObject);
    }

}
