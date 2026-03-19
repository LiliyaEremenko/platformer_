using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmovingEnemy : Entity
{
    private void Start()
    {
        lives = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == MainChar.Instance.gameObject)
        {
            MainChar.Instance.GetDamage();
        }
    }
}
