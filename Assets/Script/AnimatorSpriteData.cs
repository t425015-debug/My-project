using UnityEngine;

[CreateAssetMenu(
    fileName = "NewAnimatorData",
    menuName = "Animation/Sprite Animation Data"
)]
public class AnimatorSpriteData : ScriptableObject
{
    [Header("‘Ò‹@")]
    public AnimationClip idleClip;

    [Header("UŒ‚")]
    public AnimationClip attackClip;

    [Header("€–S")]
    public AnimationClip deathClip;
}