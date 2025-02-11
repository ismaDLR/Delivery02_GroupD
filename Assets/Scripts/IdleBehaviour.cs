using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public VisionDetector VisionDetector;


    private float _timer;
    private Transform _player;
    private bool detected;
    private float visionRange;


    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();
        visionRange = VisionDetector.DetectionRange;

    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", timeUp);
    }

    // OnStateExit is called when a transition ends and
    // the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime;
        return (_timer > StayTime);
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
