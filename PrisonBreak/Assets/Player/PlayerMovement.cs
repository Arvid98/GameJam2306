using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Check if only one axis is being used (horizontal or vertical)
        if (moveHorizontal != 0 && moveVertical != 0)
        {
            // If diagonal movement is detected, zero out the corresponding axis
            if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
                moveVertical = 0;
            else
                moveHorizontal = 0;
        }

        rb.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }
}
