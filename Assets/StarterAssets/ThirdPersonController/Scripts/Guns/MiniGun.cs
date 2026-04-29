using UnityEngine;

public class MiniGun : Gun
{
    public Transform BulletSpawnPoint;
    public GameObject BulletProjectilePrefab;

    public float RampTime = 5f;
    public float MinFireRate = 1f;
    public float MaxFireRate = 50f;

    private float heldTime = 0f;
    private float nextFireTime = 0f;

    public AudioClip Shoot;

    public AudioClip DryFire;
    public AudioClip ReloadStart;
    public AudioClip ReloadFinish;
    public AudioClip CockSound;

    public ParticleSystem MuzzleFlash;

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

        heldTime += Time.deltaTime;

        float t = Mathf.Clamp01(heldTime / RampTime);
        float currentRate = Mathf.Lerp(MinFireRate, MaxFireRate, t);

        nextFireTime = Time.time + (1f / currentRate);

        var origin = BulletSpawnPoint.position;

        Vector3 shootDir = (targetPoint - origin).normalized;
        Vector3 spreadDir = ApplySpread(shootDir, SpreadAngle);

        Instantiate(
            BulletProjectilePrefab,
            origin,
            Quaternion.LookRotation(spreadDir, Vector3.up)
        );

        MuzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        MuzzleFlash.Play();

        AudioManager.Instance.PlaySound(Shoot);
        ConsumeAmmo();
    }

    public override void OnTriggerReleased()
    {
        heldTime = 0f;
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