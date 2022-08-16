 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float speed = 1;
    Rigidbody2D rb;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        // clamp speed
        m_Input.x = Mathf.Clamp(m_Input.x, -0.25f, 0.25f);
        m_Input.y = Mathf.Clamp(m_Input.y, -0.25f, 0.25f);

        rb.MovePosition(transform.position + m_Input * speed);

        

        
        

    
    }
}
