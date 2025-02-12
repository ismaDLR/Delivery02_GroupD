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
    private Vector2 _target;
    private Vector2 _startPos;
    private PatrolTargets PatrolTargets;
    private int numTarget = 0;
    private Vector2 position;
    private bool detected;



    // OnStateEnter is called when a transition starts and
    // the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();
        PatrolTargets = GameObject.FindObjectOfType<PatrolTargets>();
        visionRange = VisionDetector.DetectionRange;
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _startPos = new Vector2(animator.transform.position.x, animator.transform.position.y);
        position = PatrolTargets._targets[numTarget].transform.position;
        _target = position;
        //_target = new Vector2(_startPos.x + Random.Range(-1f, 1f)*4, _startPos.y + Random.Range(-1f, 1f)*4);
        OnEnterState?.Invoke();
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
        if (animator.transform.position.x == position.x && animator.transform.position.x == position.x)
        {
            if (numTarget < PatrolTargets._targets.Length - 1)
            {
                numTarget++;
                if (EdgeDetected())
                {

                    animator.transform.Rotate(0, 180, 0);
                    //animator.transform.localScale = new Vector3(-5, 5, 5);
                }
                position = PatrolTargets._targets[numTarget].transform.position;
            }
            else
            {
                numTarget = 0;
                if (EdgeDetected())
                {
                    //Debug.Log("fsajfhiusdhfois");
                    animator.transform.Rotate(0, 180, 0);
                    //animator.transform.localScale = new Vector3(5, 5, 5);
                }
                position = PatrolTargets._targets[numTarget].transform.position;

            }
        }
        // Move
        animator.transform.position = Vector2.Lerp(_startPos, _target, (_timer / StayTime) );
       
       
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime * PatrolTargets.Speed;
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
    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(PatrolTargets.EdgeDetection.position, Vector2.one, 1.5f, PatrolTargets.IsPosition);
    
        return (hit.collider != null);
    }


}
