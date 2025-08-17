using UnityEngine;

public class Folower : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
