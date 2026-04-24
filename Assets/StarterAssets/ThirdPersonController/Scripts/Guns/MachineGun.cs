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

    public ParticleSystem MuzzleFlash;

    private bool firing = false;
    public override void TryShoot(Vector3 targetPoint)
    {
        // Gate firing by time
        if (Time.time < nextFireTime)
            return;

        if (!CanShoot())
        {
            nextFireTime = Time.time + ReloadTime;
            if (firing)
            {
                AudioManager.Instance.PlaySound(DryFire);
                firing = false;
            }
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

        MuzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        MuzzleFlash.Play();

        firing = true;
        AudioManager.Instance.PlaySound(Shoot);
        ConsumeAmmo();
    }

	public override void OnTriggerReleased()
	{
		base.OnTriggerReleased();
        firing = false;
	}

	public override void Reload()
    {
        if (isReloading) { return; }
        if (CanReload())
        {
            AudioManager.Instance.PlaySound(ReloadStart);
            base.Reload();
        }
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
