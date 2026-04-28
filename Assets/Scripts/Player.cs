using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	public int Health;
	public Gun CurrentGun;
	public List<Gun> weapons;
	private bool died;

	public event Action<int, Vector3> DamageTaken;

	public Animator Animator;
	public StarterAssetsInputs StarterAssetsInputs;
	private int currentWeaponIndex;

	private void Start()
	{
		EquipWeapon(currentWeaponIndex);
	}


	public void TakeDamage(int amount, Vector3 hitPosition)
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

	private void Update()
	{
		if (StarterAssetsInputs.weaponScroll != 0)
		{
			currentWeaponIndex += StarterAssetsInputs.weaponScroll;

			if (currentWeaponIndex < 0)
			{
				currentWeaponIndex = weapons.Count - 1;
			}
			if (currentWeaponIndex >= weapons.Count)
			{
				currentWeaponIndex = 0;
			}

			EquipWeapon(currentWeaponIndex);
		}
	}

	private void EquipWeapon(int currentWeaponIndex)
	{
		CurrentGun = weapons[currentWeaponIndex];

		foreach(var gun in weapons)
		{
			gun.gameObject.SetActive(gun == CurrentGun);
		}
	}
}
