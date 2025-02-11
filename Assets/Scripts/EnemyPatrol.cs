using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform EdgedetectionPoint;
    public LayerMask WhatIsWall;
    public float Speed;

    void Update()
    {
        if (EdgeDetected()) Flip();

        Move();
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgedetectionPoint.position, Vector2.zero, 1.5f, WhatIsWall);

        return (hit.collider != null);
    }

    private void Move()
    {
        transform.Translate(transform.up * Speed * Time.deltaTime, Space.World);
    }

    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        
    }
}
