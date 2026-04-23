using UnityEngine;

public class MachineGun : Gun
{
    public Transform BulletSpawnPoint;
    public GameObject BulletProjectilePrefab;

    public float FireRate = 10f; // bullets per second

    private float nextFireTime = 0f;

    public AudioClip Shoot;

    public AudioClip DryFire;
    public AudioClip ReloadStart;
    public AudioClip ReloadFinish;
    public AudioClip CockSound;

    public override void TryShoot(Vector3 targetPoint)
    {
        // Gate firing by time
        if (Time.time < nextFireTime)
            return;

        if (!CanShoot())
        {
            nextFireTime = Time.time + ReloadTime;
            AudioManager.Instance.PlaySound(DryFire);
            return;
        }

        nextFireTime = Time.time + (1f / FireRate);

        var origin = BulletSpawnPoint.position;

        Vector3 shootDir = (targetPoint - origin).normalized;

        Instantiate(
            BulletProjectilePrefab,
            origin,
            Quaternion.LookRotation(shootDir, Vector3.up)
        );

        AudioManager.Instance.PlaySound(Shoot);
        ConsumeAmmo();
    }

	public override void Reload()
	{
        if (isReloading) { return; }
        AudioManager.Instance.PlaySound(ReloadStart);
		base.Reload();
    }

	public override void ReloadFinished()
    {
        AudioManager.Instance.PlaySound(ReloadFinish);
        base.ReloadFinished();
	}

	public override void Cock()
	{
        AudioManager.Instance.PlaySound(CockSound);
		base.Cock();
	}
}