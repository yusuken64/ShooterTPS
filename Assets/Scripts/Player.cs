using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Health;
	public Gun CurrentGun;

	public event Action<int, Vector3> DamageTaken;

	internal void TakeDamage(int amount, Vector3 hitPosition)
	{
		Health -= amount;
		DamageTaken?.Invoke(amount, hitPosition);
	}
}
