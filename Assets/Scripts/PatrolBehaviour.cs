using UnityEngine;

public class PatrolBehaviour : StateMachineBehaviour
{
    public float StayTime;
    public float VisionRange;
    public GameObject[] _targets;

    private float _timer;
    private Transform _player;
    private Vector2 _target;
    private Vector2 _startPos;

    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _startPos = new Vector2(animator.transform.position.x, animator.transform.position.y);
        _target = new Vector2(_startPos.x + Random.Range(-1f, 1f)*4, _startPos.y + Random.Range(-1f, 1f)*4);
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", !timeUp);

        // Move
        animator.transform.position = Vector2.Lerp(_startPos, _target, _timer / StayTime);
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime;
        return (_timer > StayTime);
    }

    private bool IsPlayerClose(Transform transform)
    {
        var dist = Vector3.Distance(transform.position, _player.position);
        return (dist < VisionRange);
    }
}
