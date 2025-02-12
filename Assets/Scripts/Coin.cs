using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<int> OnTriggerPlayer;

    private int value;

    void Start()
    {
        value = 25;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnTriggerPlayer?.Invoke(value);
            Destroy(gameObject);
        }
    }
}