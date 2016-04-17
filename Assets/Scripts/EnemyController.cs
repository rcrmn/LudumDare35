using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Sprite[] LevelSkin;

	public float Speed;

	public float AttackDistance;

	public float AttackForce = 4;

	int level = -1;

	GameObject player;

	GameController gameController;

	public void SetLevel(int min, int max)
	{
		SetLevel(Random.Range(min, max+1));
	}

	private void SetLevel(int lvl)
	{
		level = System.Math.Min(lvl, LevelSkin.Length-1);
		GetComponent<SpriteRenderer>().sprite = LevelSkin[level];
	}

	void Start()
	{
		if (level < 0)
		{
			SetLevel(0, LevelSkin.Length - 1);
		}
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	void FixedUpdate()
	{
		if (!GameController.GameStarted) return;
		if (player != null)
		{
			var v = player.transform.position - transform.position;
			var v2 = new Vector2(v.x, v.y);
			var p2 = new Vector2(transform.position.x, transform.position.y);
			var hit = Physics2D.Raycast(p2, v2, v2.magnitude, LayerMask.GetMask(new string[]{"Default"}));
			if (hit.collider != null && hit.collider.gameObject.tag == "Player")
			{
				var dir = player.transform.position - transform.position;
				dir.z = 0;
				dir.Normalize();
				transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), dir);

				transform.position = transform.position + dir * Speed * Time.fixedDeltaTime;

				var d = player.transform.position - transform.position;
				d.z = 0;
				if(d.magnitude < AttackDistance)
				{
					gameController.DamagePlayer();
					player.GetComponent<Rigidbody2D>().AddForce(dir*AttackDistance*AttackForce, ForceMode2D.Impulse);
				}
			}
		}
	}

	void OnPlayerEnter(GameObject obj)
	{
		player = obj;
	}

	void OnPlayerExit(GameObject obj)
	{
		player = null;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (!GameController.GameStarted) return;
		if(coll.collider.GetComponent<Bullet>() != null)
		{
			if (level > 0)
			{
				SetLevel(level - 1);
				gameController.EnemyHurt();
			}
			else
			{
				Destroy(gameObject);
				gameController.EnemyKilled();
			}
		}
	}
}
