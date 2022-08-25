
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    private GameManager gameManager;

    private void Awake()
    {
        //gameManager = FindObjectOfType<GameManager>().
    }

    private void Update()
    {
        //powerPellet.
    }

    public void Fright()
    {
        body.enabled = false;
    }

    public void Normal()
    {

    }
}
