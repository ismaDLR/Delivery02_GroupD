using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Action<int> OnSetPointText;
    public int Points;
    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            soundManager.SeleccionAudio(0, 0.1f);
            OnSetPointText?.Invoke(Points);
            Destroy(gameObject);
        }
    }
}
