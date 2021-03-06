﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	public Sprite[] LevelSkin;

	public float Speed;

	public float AttackDistance;

	public float AttackForce = 4;

	public Color PresidentColor;

	bool isPresident = false;

	int level = -1;

	GameObject player;

	GameController gameController;

	public void SetLevel(int min, int max)
	{
		int lvl = Random.Range(min, max + 1);
		lvl = System.Math.Min(lvl, LevelSkin.Length-2);
		if(gameController == null)
		{
			gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		}

		if(isPresident || gameController.ShouldSpawnPresident)
		{
			lvl = LevelSkin.Length - 1;
			gameController.ShouldSpawnPresident = false;
			GetComponent<SpriteRenderer>().color = PresidentColor;
			isPresident = true;
		}
		SetLevel(lvl);
	}

	private void SetLevel(int lvl)
	{
		level = lvl;
		GetComponent<SpriteRenderer>().sprite = LevelSkin[level];
	}

	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		if (level < 0)
		{
			SetLevel(0, LevelSkin.Length - 1);
		}
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
				gameController.EnemyKilled(isPresident);
			}
		}
	}
}
