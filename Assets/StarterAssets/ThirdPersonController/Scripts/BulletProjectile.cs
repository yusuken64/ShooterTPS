using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float MaxLifeTime = 4f;
    private Rigidbody bulletRigidBody;

    public GameObject HitParticles;
    public GameObject MissParticles;

	public EnemyAttack Owner { get; internal set; }

	void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, MaxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 10f;
        bulletRigidBody.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        if (Owner != null &&
            other.transform.root == Owner.transform.root) return;

        Destroy(gameObject);
		if (other.transform != null &&
            other.transform.TryGetComponent<IDamageable>(out IDamageable damagable))
		{
			damagable.TakeDamage(1, other.transform.position);
			var particle = Instantiate(HitParticles, other.transform.position, Quaternion.identity);
            Destroy(particle, 1f);
		}
		else
        {
            var particle = Instantiate(MissParticles, transform.position, Quaternion.identity);
            Destroy(particle, 1f);
        }
	}
}

internal interface IDamageable
{
	void TakeDamage(int v, Vector3 hitPosition);
}