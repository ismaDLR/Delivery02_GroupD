using UnityEngine;

public class PatrolAleatoriBehaviour : StateMachineBehaviour
{
    public VisionDetector VisionDetector;
    public float StayTime;
    public float VisionRange;

    private float _timer;
    private Transform _player;
    private Vector3 _target;
    private Vector2 _startPos;
    private bool detected = false;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        VisionDetector = GameObject.FindObjectOfType<VisionDetector>();
        _timer = 0.0f;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _startPos = new Vector2(animator.transform.position.x, animator.transform.position.y);

        Vector3 camPosition = Camera.main.transform.position;

        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        float minX = camPosition.x - camWidth / 2f;
        float maxX = camPosition.x + camWidth / 2f;
        float minY = camPosition.y - camHeight / 2f;
        float maxY = camPosition.y + camHeight / 2f;

        float targetX = Random.Range(minX, maxX);
        float targetY = Random.Range(minY, maxY);

        _target = new Vector3(targetX, targetY, 0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var playerClose = IsPlayerClose(animator.transform);
        var timeUp = IsTimeUp();

        animator.SetBool("IsChasing", playerClose);
        animator.SetBool("IsPatroling", !timeUp);

        // Move
        Vector2 dir = _target - animator.transform.position;

        animator.transform.position += (Vector3)dir.normalized * 0.5f * Time.deltaTime;
    }

    private bool IsTimeUp()
    {
        _timer += Time.deltaTime * 0.5f;
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
