using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const float SPEED_COEFFICENT = 50;
    private const string HORIZONTAL_EXIS = "Horizontal";
    private const string GROUND_TAG = "Ground";

    [SerializeField] private float _speedX = 1;
    [SerializeField] private float _jumpForce = 500;

    private Rigidbody2D _rigB;
    private float _dirrection;
    private bool _isJump;
    private bool _isGround;



    private void Start()
    {
        _rigB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _dirrection = Input.GetAxis(HORIZONTAL_EXIS);
        if (_isGround && Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
        }

    }

    private void FixedUpdate()

    {
        _rigB.linearVelocity = new Vector2(_speedX * _dirrection * SPEED_COEFFICENT * Time.fixedDeltaTime, _rigB.linearVelocity.y);
        if (_isJump)
        {
            _rigB.AddForce(new Vector2(0, _jumpForce));
            _isJump = false;
            _isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            _isGround = true;
        }
    }
}