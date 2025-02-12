using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolBehaviour : StateMachineBehaviour
{
    public static Action OnEnterState;
    public float StayTime;
    public float Speed;
    public VisionDetector VisionDetector;

    private float _timer;
    private float visionRange;
    private Transform _player;
    private Vector3 _target;
    private PatrolTargets PatrolTargets;
    private int numTarget = 0;
    private Vector3 position;
    private bool detected;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();
        PatrolTargets = GameObject.FindObjectOfType<PatrolTargets>();
        visionRange = VisionDetector.DetectionRange;
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        position = PatrolTargets._targets[numTarget].transform.position;
        _target = position;
        
        OnEnterState?.Invoke();
        animator.transform.Rotate(0, 180, 0);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();
        
        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", !timeUp);

        if ((int)animator.transform.position.x == (int)position.x)
        {
            if (numTarget < PatrolTargets._targets.Length - 1)
            {
                numTarget++;
                position = PatrolTargets._targets[numTarget].transform.position;
            }
            else
            {
                numTarget = 0;
                position = PatrolTargets._targets[numTarget].transform.position;
            }
        }
        // Move
        Vector2 dir = _target - animator.transform.position;
        animator.transform.position += (Vector3) dir.normalized * 0.5f * Time.deltaTime;
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime * PatrolTargets.Speed;
        return (_timer > StayTime);
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
