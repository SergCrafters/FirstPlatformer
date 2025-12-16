using UnityEngine;


[RequireComponent(typeof(Fliper))]
public class EnemyGroundDetector : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private LayerMask _groundLayer;

    private Fliper _fliper;

    private void Start()
    {
        _fliper = GetComponent<Fliper>();
    }

    private void OnDrawGizmos()
    {
        Vector2 origin = GetOrigin();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, _radius);

        Vector2 downOrigin = origin;
        downOrigin.y += _offsetY;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(downOrigin, _radius);
    }

    public bool CanMove()
    {
        Vector2 origin = GetOrigin(); 
        Collider2D hitForward = Physics2D.OverlapCircle(origin, _radius, _groundLayer);
        
        Vector2 downOrigin = origin;
        downOrigin.y += _offsetY;

        Collider2D hitDown = Physics2D.OverlapCircle(downOrigin, _radius, _groundLayer);

        return hitForward == null && hitDown != null;
    }

    private Vector2 GetOrigin()
    {
        int directionCoefficient = _fliper?.IsTurnRight ?? true ? 1 : -1;
        float origin = transform.position.x + _offsetX * directionCoefficient;
        return new Vector2(origin, transform.position.y);
    }
}
