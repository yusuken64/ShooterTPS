using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyBrain EnemyBrain;

    public int HP;

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
