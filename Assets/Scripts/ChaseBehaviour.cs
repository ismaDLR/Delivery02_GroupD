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

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        visionRange = VisionDetector.DetectionRange;
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {



        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        animator.SetBool("IsChasing", playerClose);



        // Move to player
        Vector2 dir = _player.position - animator.transform.position;
        animator.transform.position += (Vector3) dir.normalized * Speed * Time.deltaTime;

        OnDetectPlayer?.Invoke(_player);
    }

    private bool IsPlayerClose(Transform transform)
    {
        bool insideVision = VisionDetector.isDetected; // Si está dentro del cono de visión
        var dist = Vector3.Distance(transform.position, _player.position);

        if (insideVision)
        {
            detected = true; // Detecta al jugador dentro del cono de visión
        }
        else if (detected && dist < visionRange)
        {
            detected = true; // Si ya estaba detectado, solo sale cuando se aleje del radio completo
        }
        else
        {
            detected = false; // Si no está en visión y está fuera del radio, se deja de detectar
        }

        return detected;
    }
}
