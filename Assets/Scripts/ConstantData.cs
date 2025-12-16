using UnityEngine;

public static class ConstantData
{
    public static class AnimatorParameters 
    {
        public static readonly int SpeedX = Animator.StringToHash(nameof(SpeedX));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int Ground = Animator.StringToHash(nameof(Ground));
        public static readonly int IsOn = Animator.StringToHash(nameof(IsOn));
        public static readonly int IsOff = Animator.StringToHash(nameof(IsOff));
        public static readonly int IsWalk = Animator.StringToHash(nameof(IsWalk));
        public static readonly int IsRun = Animator.StringToHash(nameof(IsRun));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
        public static readonly int Hit = Animator.StringToHash(nameof(Hit));
    }

    public static class Tags 
    {
        public const string GROUND_TAG = "Ground";
    }

    public static class InpudData 
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
    }

    public static class SaveData
    {
        public const string MUSIC_KEY = "Music";
        public const string MUSIC_MUTE_KEY = "MusicIsOn";
        public const string SOUND_KEY = "Sound";
        public const string SOUND_MUTE_KEY = "SoundIsOn";
        
        public const int IS_ON_VALUE = 1;
        public const int IS_OFF_VALUE = 0;

        public const float DEFAULT_VALUME = 1;
    }

}
