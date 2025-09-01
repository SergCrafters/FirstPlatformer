using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SPEED_COEFFICENT = 50;

    [SerializeField] private float _speedX = 1;
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

        if ((dirrection > 0 && _isTurnRight == false)
            || (dirrection < 0 && _isTurnRight))
        {
            Flip();
        }

        if (Mathf.Sign(dirrection) != Mathf.Sign(_previousDirection) && dirrection != 0 && IsGround)
        {
            _rigB.linearVelocity = new Vector2(_rigB.linearVelocity.x, 0);
            _previousDirection = dirrection;
        }
    }


    private void Flip()
    {
        _isTurnRight = !_isTurnRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}