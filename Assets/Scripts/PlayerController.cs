using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private GameController gameController;
    private SpawnController SpawnController;
    public float forwardSpeed;
    public float jumpSpeed;
    public Vector3 movement;
    public float gravity;
    public float speed;
    public float timerValue;
    public float invincibleTimer;
    public bool invincible;
    public int boostMeter;
    public float score;
    public Camera mainCamera;
    public Camera SecondryCamera;
    public bool firstPerson;
    public bool thirdPesrson;




    void Start()
    {
        firstPerson = false;
        thirdPesrson = true;
        mainCamera.gameObject.SetActive(thirdPesrson);
        SecondryCamera.gameObject.SetActive(firstPerson);
        score = 0;
        gravity = 20.0f;
        jumpSpeed = 8.0f;
        speed = 100.0f;
        invincible = false;
        characterController = GetComponent<CharacterController>();
        gameController = GameController.Instance;
        SpawnController = SpawnController.Instance;
        forwardSpeed = 50f;
        timerValue = 60.0f;
        invincibleTimer = 10.0f;
       
        boostMeter = 0;
        
    }
    void Update()
    {
        if (!(gameController.GameOver || gameController.GamePaused))
        {

            mainCamera.gameObject.SetActive(thirdPesrson);
            SecondryCamera.gameObject.SetActive(firstPerson);

            if (Input.GetKeyDown(KeyCode.C))
            {
                firstPerson = !firstPerson;
                thirdPesrson = !thirdPesrson;
            }

            score = this.transform.position.z;
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
            if (characterController.isGrounded)
            {

                float horizontalAxis = Input.GetAxis("Horizontal");

                movement = new Vector3(horizontalAxis, 0.0f, forwardSpeed);
                movement.x *= speed;
                movement.y *= speed;
                if (Input.GetButton("Jump"))
                {
                    Debug.Log("jump");
                    movement.y = jumpSpeed;

                }


            }
            movement.y -= gravity * Time.deltaTime;

            characterController.Move(movement * Time.deltaTime);
        }
    }
}
