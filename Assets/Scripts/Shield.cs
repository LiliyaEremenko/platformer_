using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Collectible
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == MainChar.Instance.gameObject)
        {
            collision.GetComponent<MainChar>().GetShield(count);
            Destroy(gameObject);
        }
    }
}
