using UnityEngine;

public interface IInputReader
{
    public float Dirrection { get; }

    public bool GetIsJump();

    public bool GetIsInteract();

    public bool GetIsAttack();
}
