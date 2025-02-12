using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public static Action<int> OnPlayerMove;
    public bool IsMoving => _isMoving;

    [SerializeField]
    private float Speed = 5.0f;

    private Vector3 initialPosition;
    private float distance = 0f;
    private bool _isMoving;
    Rigidbody2D _rigidbody;

    private int score = 2000;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }


    public void OnMove(InputValue value)
    {
        var inputVal = value.Get<Vector2>();

        Vector2 velocity = inputVal * Speed;
        _rigidbody.linearVelocity = velocity;
        _rigidbody.freezeRotation = true;

        _isMoving = (velocity.magnitude > 0.01f);

        if (_isMoving) LookAt((Vector2)transform.position + velocity);
        else transform.rotation = Quaternion.identity;

        distance += Vector3.Distance(transform.position, initialPosition);
        initialPosition = transform.position;

        OnPlayerMove?.Invoke((int)distance);
    }

    public void OnSaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        score = PlayerPrefs.GetInt("Score");
    }

    private void LookAt(Vector2 targetPosition)
    {
        Vector3 relative = transform.InverseTransformPoint(targetPosition);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Ending");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("Ending");
        }
    }
}