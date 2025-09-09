using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const float SPEED_COEFFICENT = 50;

    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _runSpeedX = 2;
    [SerializeField] private float _jumpForce = 500;

    private Rigidbody2D _rigB;
    private float _previousDirection;
    private bool _isTurnRight = true;

    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
        _previousDirection = 0;
    }


    public void Jump()

    {
        _rigB.AddForce(new Vector2(0, _jumpForce));
    }


    public void Move(float dirrection, bool IsGround)
    {
        _rigB.linearVelocity = new Vector2(_speedX * dirrection * SPEED_COEFFICENT * Time.fixedDeltaTime, _rigB.linearVelocity.y);

        if (Mathf.Sign(dirrection) != Mathf.Sign(_previousDirection) && dirrection != 0 && IsGround)
        {
            _rigB.linearVelocity = new Vector2(_rigB.linearVelocity.x, 0);
            _previousDirection = dirrection;
        }
    }

    public void Run(Transform target) => Move(target, _runSpeedX);

    public void Walk(Transform target) => Move(target, _speedX);

    private void Move(Transform target, float speed)
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        newPosition.y = transform.position.y;
        _rigB.MovePosition(newPosition);
    }
}