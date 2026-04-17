using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public float MaxLifeTime = 4f;
    private Rigidbody bulletRigidBody;

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
	}
}
