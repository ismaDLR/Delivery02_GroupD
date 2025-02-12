using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChaseBehaviour : StateMachineBehaviour
{
    public static Action<Transform> OnDetectPlayer;
    public float Speed = 2;
    public VisionDetector VisionDetector;

    private bool detected;
    private Transform _player;
    private float visionRange;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        visionRange = VisionDetector.DetectionRange;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);

        // Move to player
        Vector2 dir = _player.position - animator.transform.position;
        animator.transform.position += (Vector3) dir.normalized * Speed * Time.deltaTime;

        OnDetectPlayer?.Invoke(_player);
    }

    private bool IsPlayerClose(Transform transform)
    {
        bool insideVision = VisionDetector.isDetected;
        var dist = Vector3.Distance(transform.position, _player.position);

        if (insideVision)
        {
            detected = true;
        }
        else if (detected && dist < visionRange)
        {
            detected = true;
        }
        else
        {
            detected = false;
        }

        return detected;
    }
}
