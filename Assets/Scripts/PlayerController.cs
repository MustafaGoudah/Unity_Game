using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private GameController gameController;
    private SpawnController SpawnController;
    public float gravity;
    public float timerValue;
    public float invincibleTimer;
    public bool invincible;
    public int boostMeter;
    public float score;
    public Camera mainCamera;
    public Camera SecondryCamera;
    public bool firstPerson;
    public bool thirdPesrson;
    public float laneWidth;
    private int laneIndex;

    public float playerSpeed;
    public float playerJumpHeight;
    public float changeLaneSpeed;
    private Vector3 velocity;





    void Start()
    {
        firstPerson = false;
        thirdPesrson = true;
        mainCamera.gameObject.SetActive(thirdPesrson);
        SecondryCamera.gameObject.SetActive(firstPerson);
        score = 0;
        invincible = false;
        characterController = GetComponent<CharacterController>();
        laneIndex = 0;
        gameController = GameController.Instance;
        SpawnController = SpawnController.Instance;
        timerValue = 60.0f;
        invincibleTimer = 10.0f;
        boostMeter = 0;
        laneWidth = 3.5f;

        playerSpeed = 30f;
        playerJumpHeight = 3f;
        changeLaneSpeed = 10f;


    }

    void move()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            laneIndex += (int)Input.GetAxisRaw("Horizontal");

        }

        if (characterController.isGrounded)
        {
            velocity = Vector3.forward * playerSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = Mathf.Sqrt(2 * gravity * playerJumpHeight);
            }
        }
        velocity.y -= gravity * Time.deltaTime;

        Vector3 moveAmount = velocity * Time.deltaTime;
        float targetX = laneIndex * laneWidth;
        float dirX = Mathf.Sign(targetX - transform.position.x);
        float deltaX = changeLaneSpeed * dirX * Time.deltaTime;

        // Correct for overshoot
        if (Mathf.Sign(targetX - (transform.position.x + deltaX)) != dirX)
        {
            float overshoot = targetX - (transform.position.x + deltaX);
            deltaX += overshoot;
        }
        moveAmount.x = deltaX;

        characterController.Move(moveAmount);
    }

void Update()
    {
        if (!(gameController.GameOver || gameController.GamePaused))
        {
            if (timerValue <= 0)
            {
                timerValue = 0;
                gameController.GameOver = true;
            }
            mainCamera.gameObject.SetActive(thirdPesrson);
            SecondryCamera.gameObject.SetActive(firstPerson);

            if (Input.GetKeyDown(KeyCode.C))
            {
                firstPerson = !firstPerson;
                thirdPesrson = !thirdPesrson;
            }

            score = (int)this.transform.position.z;
            if (timerValue > 0)
            {
                timerValue -= Time.deltaTime;
            }
            else
            {
                timerValue = 0.0f;
            }

            if (boostMeter == 50)
            {

                invincible = true;
                boostMeter = 0;
                Debug.Log("invincible now");
            }

            if (invincibleTimer > 0.0 && invincible)
            {
                invincibleTimer -= Time.deltaTime;
                Debug.Log(invincibleTimer);


            }
            if (invincibleTimer <= 0.0f && invincible)
            {
                invincible = false;
                invincibleTimer = 10.0f;
            }
            move();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Coin":
                OnCoinCollected();
                break;
            case "GreySphere":
                OnGreySphereCollected();
                break;
            case "Blue Sphere":
                OnBlueSphereCollected();
                break;
            case "RedSphere":
                gameController.GameOver = true;
                break;
        }



        other.gameObject.SetActive(false);

    }

    private void OnCoinCollected()
    {
        if (!invincible)
        {
            if (timerValue > 0)
            {
                timerValue += 2.0f;
            }
        }
    }

    private void OnGreySphereCollected()
    {
        if (!invincible)
        {
            if (timerValue > 0)
            {
                timerValue -= 10.0f;
            }
        }
    }

    private void OnBlueSphereCollected()
    {
        if (!invincible)
        {
            boostMeter += 10;
        }

    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (!(gameController.GameOver || gameController.GamePaused))
        {
            /*  if (characterController.isGrounded)
              {

                  float horizontalAxis = Input.GetAxis("Horizontal");

                  movement = new Vector3(horizontalAxis, 0.0f, forwardSpeed);
                  movement.x *= speed;
                  movement.y *= speed;
                  if (Input.GetButton("Jump"))
                  {

                      movement.y = jumpSpeed;

                  }


              }
              movement.y -= gravity * Time.deltaTime;

              characterController.Move(movement * Time.deltaTime);*/


        }



    }

}

