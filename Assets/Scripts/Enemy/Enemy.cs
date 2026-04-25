using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBrain EnemyBrain;

    public int HP;

	private void Start()
	{
		EnemyBrain.target = FindFirstObjectByType<Player>().transform;
		EnemyBrain.EnemyChase.target = FindFirstObjectByType<Player>();
	}

	public void TakeDamage(int damage)
	{
        if (HP <= 0) { return; }
        HP -= damage;
        if (HP <= 0)
		{
            Die();
		}
	}

	private void Die()
    {
        EnemyBrain.OnDeath();
    }
}
