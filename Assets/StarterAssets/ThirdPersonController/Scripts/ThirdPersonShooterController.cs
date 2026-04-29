using Cinemachine;
using StarterAssets;
using System;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
	public CinemachineVirtualCamera VirtualCamera;
	public CinemachineVirtualCamera AimVirtualCamera;
	public StarterAssetsInputs StarterAssetsInputs;
	public ThirdPersonController ThirdPersonController;

	public float NormalSensitivity;
	public float AimSensitivity;
	public LayerMask aimColliderLayerMask;

	public Transform DebugTransform;

	public Animator Animator;

	public Player Player;
	private bool aiming;

	public bool Reloading;

	private void Awake()
	{
		//TODO do this register on switch weapons;
		Player.CurrentGun.OnReloadStart += CurrentGun_OnReloadStart;
		Player.CurrentGun.OnReloadEnd += CurrentGun_OnReloadEnd;
	}

	private void Update()
	{
		if (Player.Died)
		{
			aiming = false;
			AimVirtualCamera.gameObject.SetActive(false);
		}

		if (StarterAssetsInputs.aim)
		{
			AimVirtualCamera.gameObject.SetActive(true);
			ThirdPersonController.SetSensitivity(AimSensitivity);
			ThirdPersonController.SetRotateOnMove(false);
			Animator.SetLayerWeight(1, Mathf.Lerp(Animator.GetLayerWeight(1), 1, Time.deltaTime * 10f));

			Vector3 aimDir = Camera.main.transform.forward;

			if (aimDir.sqrMagnitude > 0.001f)
			{
				aimDir.Normalize();

				Quaternion targetRotation = Quaternion.LookRotation(aimDir);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
			}

			HandleShoot();

			if (!aiming)
			{
				Player.CurrentGun?.Cock();
				aiming = true;
			}
		}
		else
		{
			aiming = false;
			AimVirtualCamera.gameObject.SetActive(false);
			ThirdPersonController.SetSensitivity(NormalSensitivity);
			ThirdPersonController.SetRotateOnMove(true);
			Animator.SetLayerWeight(1, Mathf.Lerp(Animator.GetLayerWeight(1), 0, Time.deltaTime * 10f));

			HandleReload();
		}
	}

	private void HandleShoot()
	{
		if (StarterAssetsInputs.releaseThisFrame && Player.CurrentGun != null)
		{
			Player.CurrentGun?.OnTriggerReleased();

		}
		if (StarterAssetsInputs.shoot && Player.CurrentGun != null)
		{
			Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
			Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

			Vector3 targetPoint;

			if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
			{
				targetPoint = hit.point;
			}
			else
			{
				// fallback if nothing hit
				targetPoint = ray.GetPoint(100f);
			}
			Player.CurrentGun.TryShoot(targetPoint);
		}
	}

	private void HandleReload()
	{
		if (StarterAssetsInputs.reload &&
			Player.CurrentGun != null &&
			Player.CurrentGun.CanReload() &&
			!Reloading)
		{
			Player.CurrentGun.Reload();
		}
	}

	private void CurrentGun_OnReloadStart()
	{
		Reloading = true;
		Animator.CrossFade("Reload", 0.05f);
		//Animator.Play("Reload");
	}

	private void CurrentGun_OnReloadEnd()
	{
		Reloading = false;
		Animator.CrossFade("Idle Walk Run Blend", 0.05f);
		//Animator.Play("Idle Walk Run Blend");
	}
}
