using UnityEngine;

public class IdleAleatoriBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;
    public VisionDetector VisionDetector;

    private float _timer;
    private Transform _player;
    private bool detected = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", timeUp);
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime;
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
