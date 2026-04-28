using UnityEngine;

public class Shotgun : Gun
{
    public Transform BulletSpawnPoint;
    public GameObject BulletProjectilePrefab;

    [Header("Shotgun Settings")]
    public int PelletCount = 8;
    public float SpreadAngle = 8f; // degrees
    public float FireRate = 1f;    // shots per second

    private float nextFireTime = 0f;

    [Header("Audio")]
    public AudioClip Shoot;
    public AudioClip DryFire;
    public AudioClip ReloadStart;
    public AudioClip ReloadFinish;
    public AudioClip CockSound;

    public ParticleSystem MuzzleFlash;

    public override void TryShoot(Vector3 targetPoint)
    {
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
        Vector3 baseDir = (targetPoint - origin).normalized;

        // Fire multiple pellets
        for (int i = 0; i < PelletCount; i++)
        {
            Vector3 spreadDir = ApplySpread(baseDir, SpreadAngle);

            Instantiate(
                BulletProjectilePrefab,
                origin,
                Quaternion.LookRotation(spreadDir, Vector3.up)
            );
        }

        MuzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        MuzzleFlash.Play();

        AudioManager.Instance.PlaySound(Shoot);
        ConsumeAmmo();
    }

    private Vector3 ApplySpread(Vector3 direction, float angle)
    {
        float randomYaw = Random.Range(-angle, angle);
        float randomPitch = Random.Range(-angle, angle);

        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
        return spreadRotation * direction;
    }

    public override void Reload()
    {
        if (isReloading) return;

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