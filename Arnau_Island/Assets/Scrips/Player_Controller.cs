using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed;
    //private float horizontalInput;

    GameObject focalPoint;
    GameObject powerupIndicator;

    bool hasPowerUp;
    public float PowerUpStregth = 15;

    private AudioSource playerAudio;

    public AudioClip crashSound;
    public AudioClip EPICcrashSound;
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point"); ;

        powerupIndicator = GameObject.Find("Powerupindicator");
        powerupIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime, ForceMode.Force);

        powerupIndicator.transform.position = transform.position;
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            powerupIndicator.SetActive(true);
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }


    private void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemtRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 pushAwayEnemy = (other.gameObject.transform.position - transform.position);
            Debug.Log("Player collided with" + other.gameObject + "with powerup set to" + hasPowerUp);
            enemtRB.AddForce(pushAwayEnemy * PowerUpStregth, ForceMode.Impulse);

            playerAudio.PlayOneShot(EPICcrashSound);
        }

        else if (other.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(crashSound);
        }
    }
}
