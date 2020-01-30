using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    private Rigidbody playerRB;
    public float speed;
    private float horizontalInput;

    GameObject focalPoint;

    bool hasPowerUp;
    public float PowerUpStregth = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint"); ;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint. transform. forward * speed * forwardInput * Time.deltaTime, ForceMode.Force);
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
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
        }
    }
}
