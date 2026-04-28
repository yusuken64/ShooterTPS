using System;
using System.Collections;
using UnityEngine;


public class EnemyAttack : EnemyBehaviorBase
{
    public EnemyAnimationController EnemyAnimationController;
    public float AttackCooldownMax;
    public float AttackCooldown;
    private Transform player;

    public bool UseRangedAttack;
    public Transform HitboxSpawnPoint;
    public GameObject HitboxPrefab;
    public GameObject BulletPrefab;
    public float HitDelay = 0.2f;   // time after animation starts
    public float AttackRange = 2.5f;

    void Start()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>().transform;
        }
    }

    private Coroutine attackRoutine;

    public override void Enter()
    {
        isComplete = false;
        AttackCooldown = AttackCooldownMax;
        timer = AttackCooldownMax;

        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        transform.LookAt(player.position);
        EnemyAnimationController.Play(EnemyAnimationController.attackAnim);

        yield return new WaitForSeconds(HitDelay);

        if (!UseRangedAttack)
        {
            var hitbox = Instantiate(HitboxPrefab, HitboxSpawnPoint.position, HitboxSpawnPoint.rotation);
            Destroy(hitbox, 0.2f);
        }
		else
        {
            var offset = new Vector3(0, 1, 0);
            Vector3 dir = (player.position - HitboxSpawnPoint.position + offset).normalized;
            Quaternion rot = Quaternion.LookRotation(dir);

            var newBullet = Instantiate(BulletPrefab, HitboxSpawnPoint.position, rot);
            newBullet.GetComponent<BulletProjectile>().Owner = this;
        }

        AttackCooldown = AttackCooldownMax;
        yield return new WaitForSeconds(AttackCooldownMax);

        attackRoutine = null;
        isComplete = true;
    }

    internal override void Update()
    {
        base.Update();
        AttackCooldown -= Time.deltaTime;
    }

    public override bool CanRun()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > AttackRange)
        {
            return false;
        }

        return AttackCooldown <= 0 && attackRoutine == null; 
    }

    public override void Exit()
    {
    }
}
