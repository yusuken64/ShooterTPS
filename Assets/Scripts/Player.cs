using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int Health;
	public Gun CurrentGun;

	public event Action<int> DamageTaken;

	internal void TakeDamage(int amount)
	{
		Health -= amount;
		DamageTaken?.Invoke(amount);
	}
}
