using UnityEngine;

public class PlayerAttackerss : MonoBehaviour
{
    [SerializeField] private Sword _sword;

    public bool CanAttack => _sword.IsAttack == false;

    public void Attackss()
    { 
        _sword.Attack();
    }

    public void StopAttack()
    {
        _sword.StopAttack();
    }
}
