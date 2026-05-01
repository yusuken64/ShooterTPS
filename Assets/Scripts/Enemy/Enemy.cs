using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	public EnemyBrain EnemyBrain;

	public int HP;

	public Action OnDeath { get; internal set; }

	private void Start()
	{
		EnemyBrain.target = FindFirstObjectByType<Player>().transform;
	}

	public void TakeDamage(int damage, Vector3 hitPosition)
	{
		if (HP <= 0) { return; }
		HP -= damage;
		if (HP <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		EnemyBrain.OnDeath();
		OnDeath?.Invoke();
	}
}
