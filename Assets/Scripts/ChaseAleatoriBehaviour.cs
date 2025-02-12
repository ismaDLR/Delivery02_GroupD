using UnityEngine;

public class ChaseAleatoriBehaviour : StateMachineBehaviour
{
    public float Speed = 2;
    public float VisionRange;
    public VisionDetector VisionDetector;

    private Transform _player;
    private bool detected = false;

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();

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
        animator.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
    }

    private bool IsPlayerClose(Transform transform)
    {
        bool insideVision = VisionDetector.isDetected; 
        var dist = Vector3.Distance(transform.position, _player.position);

        if (insideVision)
        {
            detected = true; 
        }
        else if (detected && dist < 2)
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
