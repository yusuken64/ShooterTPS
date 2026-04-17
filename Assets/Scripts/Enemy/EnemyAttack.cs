using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyBehavior
{
    public EnemyAnimationController EnemyAnimationController;
    public float cooldown = 3.2f;

    private float _timer;
    public Transform player;

    private bool _isComplete;
    public bool IsComplete => _isComplete;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    public void Enter()
    {
        _isComplete = false;
        _timer = cooldown;
        TryAttack(player);
    }

	public void Exit()
	{
	}

	public void Tick()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
		{
            _isComplete = true;
		}
    }

	public void TryAttack(Transform target)
    {
        // rotate toward target (optional)
        transform.LookAt(target.position);

        // trigger animation
        EnemyAnimationController.Play(EnemyAnimationController.attackAnim);
    }
}
