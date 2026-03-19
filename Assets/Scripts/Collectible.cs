using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectible : MonoBehaviour
{
    public int count;
    public Action onCollectingCoin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.tag == "Collectible") && (collision.gameObject == MainChar.Instance.gameObject))
        {
            collision.GetComponent<MainChar>().ScoreUp(count);
            Destroy(gameObject);
        }
        else if ((gameObject.tag == "Shield") && (collision.gameObject == MainChar.Instance.gameObject))
        {
            collision.GetComponent<MainChar>().GetShield(count);
            Destroy(gameObject);
        }
    }
}
