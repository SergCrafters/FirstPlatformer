using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool IsGround { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantData.Tags.GROUND_TAG))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.7f)
                {
                    IsGround = true;
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstantData.Tags.GROUND_TAG))
            IsGround = false;
    }
}
