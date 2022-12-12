using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public CharacterController characterController;
    private float movementX;
    private float movementZ;
    private float speed = 0f;
    Vector3 velocity;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public float jumpHeight = 3f;
    private Animator animator;
    public bool moving;
    public float lastXpos;
    public float timer = 0;
    public bool canJump;
    public float jumpTimer;
    public AudioSource audioSource;
    public bool isStealing;
    public TextMeshProUGUI text;
    public float gravity = -9.81f;
    public int points = 0;
    public TextMeshProUGUI textTimer;
    public float gameTimer = 240;
    public Transform groundCheck;
    public AudioSource audioSource2;
    public AudioClip audioClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        isGrounded = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        text.text = "Score: " + points.ToString();

        if(points >= 1000)
        {
            SceneManager.LoadScene(1);
        }

        textTimer.text = "Time left: " + gameTimer.ToString();
        gameTimer -= Time.deltaTime;
        timer += Time.deltaTime;

        if(gameTimer <= 0)
        {
            SceneManager.LoadScene(2);
            Debug.Log("alisdhgaoishfgjas");
        }

        movementX = Input.GetAxis("Horizontal");
        movementZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * movementX + transform.forward * movementZ;

        characterController.Move(movement * speed * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.D)))))
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isIdle", false);
                speed = 10f;
                audioSource.enabled = true;
            if (timer >= 2.0f)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
                speed = 20f;
            }
        }
        else
        {
            animator.SetBool("isStealing", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
            timer = 0f;
            audioSource.enabled = false;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("esaidghasohfd");
            animator.SetBool("isStealing", true);
            isStealing = true;
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        if(characterController.isGrounded)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("isStealing", false);
            isStealing = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumpTimer += Time.deltaTime;
            animator.SetBool("isJumping", true);
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            velocity.y = Mathf.Sqrt(jumpHeight * -1 * gravity * 3.5f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            velocity.y = -5f;
            jumpTimer = 0f;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Guard")
        {
            SceneManager.LoadScene(2);
        }
        if (isStealing && collision.gameObject.tag == "littlepot")
        {
            audioSource2.PlayOneShot(audioClip);
            Destroy(collision.gameObject);
            points += 10;

        }
        if (isStealing && collision.gameObject.tag == "bigpot")
        {
            audioSource2.PlayOneShot(audioClip);
            Destroy(collision.gameObject);
            points += 20;
        }
        if (isStealing && collision.gameObject.tag == "silverbar")
        {
            audioSource2.PlayOneShot(audioClip);
            Destroy(collision.gameObject);
            points += 30;
        }
        if (isStealing && collision.gameObject.tag == "goldbar")
        {
            audioSource2.PlayOneShot(audioClip);
            Destroy(collision.gameObject);
            points += 40;
        }
    }
}

