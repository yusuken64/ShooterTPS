using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Gun : MonoBehaviour
{
    [Header("Ammo")]
    public bool UsesAmmo = false;

    public int ClipSize = 30;
    public int AmmoInClip;
    public int ReserveAmmo;
    public float SpreadAngle = 8f; // degrees

    [Header("Reload")]
    public float ReloadTime = 1.5f;
    protected bool isReloading = false;

    public event Action OnReloadStart;
    public event Action OnReloadEnd;

    protected virtual void Awake()
    {
        AmmoInClip = ClipSize;
    }
    public abstract void TryShoot(Vector3 targetPoint);

    protected bool CanShoot()
    {
        if (isReloading)
            return false;

        if (UsesAmmo && AmmoInClip <= 0)
            return false;

        return true;
    }

    protected void ConsumeAmmo(int amount = 1)
    {
        if (!UsesAmmo)
            return;

        AmmoInClip -= amount;
    }

    internal bool CanReload()
    {
        if (!UsesAmmo || isReloading)
        {
            return false;
        }

        if (AmmoInClip >= ClipSize)
		{
            return false;
		}

        return true;
    }

    public virtual void Reload()
    {
        if (!UsesAmmo || isReloading)
            return;

        //if (AmmoInClip >= ClipSize || ReserveAmmo <= 0)
        if (AmmoInClip >= ClipSize)
            return;

        StartCoroutine(ReloadRoutine());
    }

    public virtual void ReloadFinished() { }

    protected virtual IEnumerator ReloadRoutine()
    {
        isReloading = true;

        OnReloadStart?.Invoke();
        yield return new WaitForSeconds(ReloadTime);

        int needed = ClipSize - AmmoInClip;
        //int toLoad = Mathf.Min(needed, ReserveAmmo);
        int toLoad = needed;

        AmmoInClip += toLoad;
        ReserveAmmo -= toLoad;

        isReloading = false;
        ReloadFinished();
        OnReloadEnd?.Invoke();
    }

    public virtual void OnTriggerReleased() { }

	public virtual void Cock() {}

    public static Vector3 ApplySpread(Vector3 direction, float angle)
    {
        float randomYaw = Random.Range(-angle, angle);
        float randomPitch = Random.Range(-angle, angle);

        Quaternion spreadRotation = Quaternion.Euler(randomPitch, randomYaw, 0);
        return spreadRotation * direction;
    }
}
