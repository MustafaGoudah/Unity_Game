using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private GameController gameController;
    private SpawnController SpawnController;
    public float forwardSpeed;
    public float jumpSpeed =8.0f;
    public Vector3 movement;
    public float gravity = 20.0f;
    public float speed = 6.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameController = GameController.Instance;
        SpawnController = SpawnController.Instance;
        forwardSpeed = 0.5f;
    }
    private void OnTriggerEnter(Collider other)
    {
 
        other.gameObject.SetActive(false);
        
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            
            float horizontalAxis = Input.GetAxis("Horizontal");

            movement = new Vector3(horizontalAxis, 0.0f, forwardSpeed);
            movement *= speed;
            if (Input.GetButton("Jump"))
            {
                Debug.Log("jump");
                movement.y = jumpSpeed;
                
            }


        }
       movement.y -= gravity*Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);
    }
}
