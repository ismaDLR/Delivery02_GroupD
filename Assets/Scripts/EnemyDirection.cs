using UnityEngine;

public class EnemyDirection : MonoBehaviour
{
    private void OnEnable()
    {
        ChaseBehaviour.OnDetectPlayer += ChangeDirection;
        PatrolBehaviour.OnEnterState += SetEnemyDirection;
    }

    private void OnDisable()
    {
        ChaseBehaviour.OnDetectPlayer -= ChangeDirection;
        PatrolBehaviour.OnEnterState -= SetEnemyDirection;
    }

    private void ChangeDirection(Transform player)
    {
        if (Mathf.Abs(transform.rotation.y) == 1)
        {
            if (player.position.x - transform.position.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            if (player.position.x - transform.position.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void SetEnemyDirection()
    {
        if (gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
