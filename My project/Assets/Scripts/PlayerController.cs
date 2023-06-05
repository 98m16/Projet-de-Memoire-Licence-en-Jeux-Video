using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 10.0f;


    public ParticleSystem explosionFx; 
    public ParticleSystem dirtSplatter;

    public AudioClip crashSound;
    public AudioClip gemSound;
    private AudioSource playerAudio;

    //private GameManager gameManager;
    private SpawnManager spawnManager;

    private float zBound = 12f; // if your player can go off the screen, write an if statement checkeing and resetting the position.

    private Rigidbody playerRb; //if using physics and initialize it in Start(); use either the translate method or AddForce to move your character

    float verticalInput, horizontalInput; // if using arrow keys

    //if basing movement off a key press, create the if-statement to test for the KeyCode.

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
        
    }

    // Move player with arrows keys
    void MovePlayer()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //playerRb.AddForce(Vector3.forward * speed * verticalInput);
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        playerRb.velocity = movement * speed;

        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            playerRb.MoveRotation(toRotation);
        }
    }

    // Prevent the player from leaving the top or bottom of the screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }

 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player has collided with enemy.");
            explosionFx.Play();
            dirtSplatter.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            spawnManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(gemSound, 1.0f);
            spawnManager.UpdateScore(5);
        }
        
    }
}
