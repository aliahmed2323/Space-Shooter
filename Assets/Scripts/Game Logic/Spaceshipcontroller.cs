using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceshipcontroller : MonoBehaviour
{
    [Header("Ship Configuration")]
    public float shipHealth;
    public float shipSpeed;
    public float shootDelay;
    [Header("Ship Properties")]
    public float boundaryPoint;
    public GameObject shootPoint;
    public GameObject projectile;
    [Header("Player 2 Ship")]
    public bool isPlayer2;
    [Header("Misc")]
    public StateTransmitter st;
    public GameObject explosionEffect;

    bool canShoot = true;
    void Update()
    {
        // Check whether a person presses A or D to move
        if (!isPlayer2)
        {
            if (Input.GetKey(KeyCode.D))
            {
                movementManager(true);
                st.sendPos(true);
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementManager(false);
                st.sendPos(false);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                shoot();
               st.sendShoot();
            }
        }
        if (shipHealth <= 0)
        {
            GameObject pp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(pp, 0.6f);
            Destroy(this.gameObject);
        }
    }

    public void movementManager(bool right)
    {
        // Check which key a person pressed then move them likewise
        if (transform.position.x < boundaryPoint)
        {
            if (right)
            {
                Vector2 nextPos = new Vector2(transform.position.x + 1, transform.position.y);
                transform.position = Vector2.Lerp(transform.position, nextPos, shipSpeed * Time.deltaTime);
            }
        }

        if (transform.position.x > -boundaryPoint)
        {
            if (!right)
            {
                Vector2 nextPos = new Vector2(transform.position.x - 1, transform.position.y);
                transform.position = Vector2.Lerp(transform.position, nextPos, shipSpeed * Time.deltaTime);
            }
        }
    }

    public void shoot()
    {
        if (canShoot)
        {
            Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);
            canShoot = false;
            Invoke("enableShoot", shootDelay);
        }
    }

    void enableShoot()
    {
        canShoot = true;
    }
}
