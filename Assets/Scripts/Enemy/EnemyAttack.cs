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
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }
    }

    private Coroutine attackRoutine;

    public override void Enter()
    {
        isComplete = false;
        AttackCooldown = AttackCooldownMax;

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

        isComplete = true;
    }

    internal override void Update()
    {
        base.Update();
        AttackCooldown -= Time.deltaTime;
    }

    public override bool CanRun()
    {
        return AttackCooldown <= 0 && attackRoutine == null; 
    }

    public override void Exit()
    {
    }
}
