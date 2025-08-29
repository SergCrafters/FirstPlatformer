using UnityEngine;

public static class ConstantData
{
    public static class AnimatorParameters 
    {
        public static readonly int SpeedX = Animator.StringToHash(nameof(SpeedX));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Ground = Animator.StringToHash(nameof(Ground));
    }

    public static class Tags 
    {
        public const string GROUND_TAG = "Ground";
    }

    public static class InpudData 
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
    }
}
