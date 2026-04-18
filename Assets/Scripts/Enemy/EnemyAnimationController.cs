using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;

    [Header("Animation State Names")]
    public string idleAnim = "idle";
    public string chaseAnim = "run";
    public string attackAnim = "roll";
    public string deadAnim = "die";

    private string _currentAnim;

    public void Play(string animName)
    {
        if (_currentAnim == animName) return;

        animator.CrossFade(animName, 0.15f);
        _currentAnim = animName;
    }
}