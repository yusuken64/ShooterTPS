using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Health;
	public Gun CurrentGun;
	private bool died;

	public event Action<int, Vector3> DamageTaken;

	public Animator Animator;

	internal void TakeDamage(int amount, Vector3 hitPosition)
	{
		if (died) { return; }

		Health -= amount;
		DamageTaken?.Invoke(amount, hitPosition);

		if (Health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		died = true;
		Animator.Play("Die");
	}
}
