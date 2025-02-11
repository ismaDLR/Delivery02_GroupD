using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<int> OnTriggerPlayer;

    private int value;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        value = 25;
    }

    // Update is called once per frame
    void Update()
    {
        
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
