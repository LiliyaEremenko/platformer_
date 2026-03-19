using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Entity
{
    public float speed;
    public Vector3[] positions;
    private int currentTarget;

    private void Start()
    {
        lives = 2;
    }

    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, positions[currentTarget], speed);

        if (transform.position == positions[currentTarget])
        {
            if (currentTarget < positions.Length - 1)
            {
                currentTarget++;
                GetComponentInChildren<SpriteRenderer>().flipX = true;
            }
            else
            {
                currentTarget = 0;
                GetComponentInChildren<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == MainChar.Instance.gameObject)
        {
            MainChar.Instance.GetDamage();
        }
    }
}
