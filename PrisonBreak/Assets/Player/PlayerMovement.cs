using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D collider;
    [SerializeField] AudioSource pickupSound;
    [SerializeField] AudioSource moveSound;
    [SerializeField] float normalSpeed = 5.0f;
    [SerializeField] float boostedSpeed = 10.0f;

    private float currentSpeed; // Tracks the current speed
    private bool isSprinting; // Tracks if the player is sprinting

    [SerializeField] private bool hasKey;  // when the players pcik up one key turn true
    //GameObject doorObj;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        currentSpeed = normalSpeed; // Set the initial speed to normal speed
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Pickable"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                pickupSound.mute = false;
                pickupSound.Play();
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Door"))
        {
            if (hasKey)
            {
                //GameObject.FindWithTag("Door").

                collision.gameObject.GetComponent<Door>().ToggleDoor();
                hasKey = false;
                //GameObject.FindWithTag("Door").GetComponent<Door>().open;


                // collision.
            }
        }

    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Door")
    //    {
    //        if (hasKey)
    //        {
    //            //GameObject.FindWithTag("Door").

    //            collision.gameObject.GetComponent<Door>().ToggleDoor();

    //            //GameObject.FindWithTag("Door").GetComponent<Door>().open;


    //            // collision.
    //        }
    //    }
    //}
   

    public void Movement()
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

        rb.velocity = new Vector2(moveHorizontal * currentSpeed, moveVertical * currentSpeed);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = boostedSpeed; // Set the current speed to boosted speed
                moveSound.pitch = 1.3f; // Double the pitch (play at double speed)
                isSprinting = true; // Player is sprinting
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = normalSpeed; // Set the current speed back to normal speed
            moveSound.pitch = 1.0f; // Reset the pitch to normal
            isSprinting = false; // Player is not sprinting
        }

        if (!isSprinting && (moveHorizontal == 0 && moveVertical == 0))
        {
            moveSound.Pause(); // Pause the move sound if not sprinting and not moving
        }
        else
        {
            moveSound.UnPause(); // Unpause the move sound if sprinting or moving
        }
    }
}
