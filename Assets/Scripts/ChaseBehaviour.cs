using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{

    public float Speed = 2;
    public VisionDetector VisionDetector;

    private Transform _player;
    //private float visionRange;
     


    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //visionRange = VisionDetector.DetectionRange;
    }

    // OnStateUpdate is called on each Update frame between
    // OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        float angle = Mathf.Atan2(_player.position.y, _player.position.x) * Mathf.Rad2Deg;
        animator.transform.rotation= new Quaternion(0,0, angle,0);
        // (VisionDetector.isDetected) 
        //
        // Check triggers
        var playerClose = IsPlayerClose(animator.transform);
            animator.SetBool("IsChasing", playerClose);
        //}
        

        // Move to player
        Vector2 dir = _player.position - animator.transform.position;
        animator.transform.position += (Vector3)dir.normalized * Speed * Time.deltaTime;
    }

    private bool IsPlayerClose(Transform transform)
    {
        
        return VisionDetector.isDetected;
    }
}
