using UnityEngine;

public class Sword : MonoBehaviour
{
    public bool IsAttack { get; internal set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsAttack && collision.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("hit");
        }
    }

    public void Attack()
    {
        IsAttack = true;
    }

    public void StopAttack()
    {
        IsAttack = false;
    }


}
