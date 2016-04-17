using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	private Vector3 direction;

	public float Speed;

	float KillDistance = 10;

	public void SetTarget(Vector3 target)
	{
		direction = (target - transform.position);
		direction.z = 0;
		direction.Normalize();
	}

	void FixedUpdate()
	{
		transform.position += direction * Speed * Time.fixedDeltaTime;
		KillDistance -= Speed * Time.fixedDeltaTime;
		if(KillDistance <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy(gameObject);
	}
}
