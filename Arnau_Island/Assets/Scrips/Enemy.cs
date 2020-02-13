using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRB;
    public float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - this.transform.position).normalized;
        enemyRB.AddForce(lookDirection  * speed * Time.deltaTime, ForceMode.Impulse);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
