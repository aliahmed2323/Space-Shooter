using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float life;
    public float damage;
    public bool isPlayer2;

    private void Start()
    {
        Destroy(this.gameObject, life);
    }
    private void Update()
    {
        if(!isPlayer2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + 1), speed * Time.deltaTime);
        }

        if (isPlayer2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x, transform.position.y + -1), speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Spaceshipcontroller>().shipHealth -= damage;
            Destroy(this.gameObject);
        }
    }
}
