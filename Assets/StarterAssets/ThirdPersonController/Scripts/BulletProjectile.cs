using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float MaxLifeTime = 4f;
    private Rigidbody bulletRigidBody;

    public GameObject HitParticles;
    public GameObject MissParticles;

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
        Destroy(gameObject);
		if (other.transform.parent != null &&
            other.transform.parent.TryGetComponent<Enemy>(out Enemy enemy))
		{
			enemy.TakeDamage(1);
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
