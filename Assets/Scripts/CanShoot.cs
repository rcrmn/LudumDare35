using UnityEngine;
using System.Collections;

public class CanShoot : MonoBehaviour
{
	public Bullet BulletPrefab;

	public float WeaponDistance;

	public float WeaponDelay;

	float TimeToActive = 0;

	public void ShootAt(Vector3 target)
	{
		if (TimeToActive <= 0)
		{
			var obj = Instantiate(BulletPrefab);
			var v = target - transform.position;
			v.z = 0;
			v.Normalize();
			obj.transform.position = transform.position + v * WeaponDistance;
			obj.SetTarget(target);
			TimeToActive = WeaponDelay;
		}
	}

	void Update()
	{
		TimeToActive -= Time.deltaTime;
		if(Input.GetMouseButtonDown(0))
		{
			var mouse = Input.mousePosition;
			ShootAt(Camera.main.ScreenToWorldPoint(mouse));
		}
	}
}
