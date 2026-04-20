using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    private bool hasHit;

    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;

        if (other.CompareTag("Player"))
        {
            hasHit = true;
            // deal damage

            FindFirstObjectByType<UI>().PlayerHealth -= 10;
        }
    }
}
