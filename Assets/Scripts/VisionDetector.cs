using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class VisionDetector : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;
    public bool isDetected;


    private Transform _player;


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.yellow;
        var direction = Quaternion.AngleAxis(VisionAngle / 2, transform.forward)
            * transform.right;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);
        var direction2 = Quaternion.AngleAxis(-VisionAngle / 2, transform.forward)
            * transform.right;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        //if (DetectPlayers().Length > 0) Debug.Log("Player detected");
       

        if (DetectPlayers().Length > 0)
        {
            isDetected = true;
            //  float angle = Mathf.Atan2(_player.position.y, _player.position.x) * Mathf.Rad2Deg;
            //  //animator.transform.rotation= new Quaternion(0,0, angle,0);
            //  
            //  float rotationSpeed = 5f; // Velocidad de rotación.
            //  Quaternion targetRotation = new Quaternion(0, 0, angle,0);
            //  transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isDetected = false;
        }

        

    }

    private Transform[] DetectPlayers()
    {
        List<Transform> players = new List<Transform>();

        if (PlayerInRange(ref players))
        {
            if (PlayerInAngle(ref players))
            {
                

                PlayerIsVisible(ref players);
                

            }
        }

        return players.ToArray();
    }

    private bool PlayerInRange(ref List<Transform> players)
    {
        bool result = false;
        Collider2D[] playerColliders = Physics2D.OverlapCircleAll(transform.position, DetectionRange, WhatIsPlayer);
        //Debug.Log($"OverlapCircleAll encontró {playerColliders.Length} jugadores.");

        if (playerColliders.Length != 0)
        {
            result = true;

            foreach (var item in playerColliders)
            {
                //Debug.Log($"Jugador detectado: {item.name}, Layer: {item.gameObject.layer}");
                players.Add(item.transform);
            }
        }

        return result;
    }

    private bool PlayerInAngle(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var angle = GetAngle(players[i]);
            //Debug.Log($"Jugador: {players[i].name}, Ángulo: {angle}, Límite: {VisionAngle / 2}");


            if (angle > VisionAngle / 2)
            {
                //Debug.Log($"Eliminado por ángulo: {players[i].name}");

                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private float GetAngle(Transform target)
    {
        Vector2 targetDir = target.position - transform.position;
        float angle = Vector2.Angle(targetDir, transform.right);

        return angle;
    }

    private bool PlayerIsVisible(ref List<Transform> players)
    {
        for (int i = players.Count - 1; i >= 0; i--)
        {
            var isVisible = IsVisible(players[i]);

            if (!isVisible)
            {
                players.Remove(players[i]);
            }
        }

        return (players.Count > 0);
    }

    private bool IsVisible(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(
           transform.position,
           dir,
           DetectionRange,
           WhatIsVisible
        );
        //Debug.DrawRay(transform.position, dir.normalized * DetectionRange, Color.red, 0.1f);
        //Debug.Log($"Raycast hacia {target.name}, golpeó: {(hit.collider != null ? hit.collider.name : "Nada")}");

        return (hit.collider.transform == target);
    }
}

