using System;
using System.Collections;
using UnityEngine;


public class EnemyAttack : EnemyBehaviorBase
{
    public EnemyAnimationController EnemyAnimationController;
    public float AttackCooldownMax;
    public float AttackCooldown;
    private Transform player;

    public Transform HitboxSpawnPoint;
    public GameObject HitboxPrefab;
    public float HitDelay = 0.2f;   // time after animation starts

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

        var hitbox = Instantiate(HitboxPrefab, HitboxSpawnPoint.position, HitboxSpawnPoint.rotation);
        Destroy(hitbox, 0.2f);

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

        if (dist > 2.5f)
        {
            return false;
        }

        return AttackCooldown <= 0 && attackRoutine == null; 
    }

    public override void Exit()
    {
    }
}
