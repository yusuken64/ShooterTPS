using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private bool hasHit;

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (other.TryGetComponent(out Player player))
        {
            hasHit = true;
            // deal damage
            player.TakeDamage(10, this.transform.position);
        }
    }
}
