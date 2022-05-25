using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -9.81f;
    Rigidbody rb;

    CharacterController controller;
    public audioManager audioManager;
    float cooldownTime = 0.5f;
    float currentTime;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    public string standingOn;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(PlayerMechanics.gameState == GameState.Game)
        {
            movement();
        }
    }
    public void movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                if (standingOn == "Grass")
                {
                    audioManager.playStepsSoundDirt();

                }
                else if (standingOn == "Stone")
                {
                    audioManager.playStepsSoundStone();

                }
                currentTime = cooldownTime;
            }
        }
      

        Vector3 movement = transform.right * horizontal + transform.forward * vertical;

        controller.Move(movement * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
  
}
