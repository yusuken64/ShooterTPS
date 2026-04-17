using Cinemachine;
using StarterAssets;
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

	public BulletProjectile BulletProjectilePrefab;
	public Transform BulletSpawnPoint;

	private void Update()
	{
		if (StarterAssetsInputs.aim)
		{
			AimVirtualCamera.gameObject.SetActive(true);
			ThirdPersonController.SetSensitivity(AimSensitivity);
			ThirdPersonController.SetRotateOnMove(false);
		}
		else
		{
			AimVirtualCamera.gameObject.SetActive(false);
			ThirdPersonController.SetSensitivity(NormalSensitivity);
			ThirdPersonController.SetRotateOnMove(true);
		}

		Vector3 aimDir = Camera.main.transform.forward;

		if (aimDir.sqrMagnitude > 0.001f)
		{
			aimDir.Normalize();

			Quaternion targetRotation = Quaternion.LookRotation(aimDir);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
		}

		HandleShoot();
	}

	private void HandleShoot()
	{
		if (StarterAssetsInputs.shoot)
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

			Vector3 shootDir = (targetPoint - BulletSpawnPoint.position).normalized;

			Instantiate(
				BulletProjectilePrefab,
				BulletSpawnPoint.position,
				Quaternion.LookRotation(shootDir, Vector3.up)
			);

			StarterAssetsInputs.shoot = false;
		}
	}
}
