using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    BoxCollider2D collider;
    [SerializeField] AudioSource pickupSound;
    [SerializeField] AudioSource moveSound;
    [SerializeField] float normalSpeed = 5.0f;
    [SerializeField] float boostedSpeed = 10.0f;
    [SerializeField] private GameObject visualGameObjectFront;
    [SerializeField] private GameObject visualGameObjectBack;
    [SerializeField] private GameObject visualGameObjectLeft;
    [SerializeField] private GameObject visualGameObjectRight;

    private float currentSpeed; // Tracks the current speed
    private bool isSprinting; // Tracks if the player is sprinting

    [SerializeField] private bool hasKey;  // when the players pick up one key turn true
    [SerializeField] private bool hasHammer;  // when the players pick up one key turn true
    //GameObject doorObj;
    [SerializeField] Sprite newSprite;
    [SerializeField] Sprite sFront;
    [SerializeField] Sprite sBack;
    [SerializeField] Sprite sLeft;
    [SerializeField] Sprite sRight;
    void ChangeSprite()
    {
        spriteRenderer.sprite = newSprite;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        currentSpeed = normalSpeed; // Set the initial speed to normal speed
    }

    void Update()
    {
        Movement();
        ChangeSprite();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasKey = true;
                pickupSound.mute = false;
                pickupSound.Play();
                Destroy(collision.gameObject);
            }
           
        }
        if (collision.CompareTag("Hammer"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                hasHammer = true;
                pickupSound.mute = false;
                pickupSound.Play();
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Door"))
        {
            if (hasKey)
            {
                collision.gameObject.GetComponent<Door>().ToggleDoor();
                hasKey = false;

            }
            
        }
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            Loader.Load(Loader.Scene.LosingScene);

        }
        if (collision.CompareTag("Wall"))
        {
            if (hasHammer)
            {
                collision.gameObject.GetComponent<BreakingWall>().BreakWall();
                hasHammer = false;
               
            }
        }
    }
    public void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Check if only one axis is being used (horizontal or vertical)
        //if (moveHorizontal != 0 && moveVertical != 0)
        //{
        //    // If diagonal movement is detected, zero out the corresponding axis
        //    if (Mathf.Abs(moveHorizontal) > Mathf.Abs(moveVertical))
        //        moveVertical = 0;
        //    else
        //        moveHorizontal = 0;
        //}

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

        if (moveHorizontal < 0)
        {
            newSprite = sRight;
            transform.localScale = new Vector3(1,1,1);
        }
        else if (moveHorizontal > 0)
        {
            newSprite = sRight;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(moveVertical <0)
        {
            newSprite = sFront;
        }
        else if (moveVertical > 0)
        {
            newSprite = sBack;
        }
    }

}
