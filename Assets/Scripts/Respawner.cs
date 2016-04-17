using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public GameObject EnemyPrefab;

	public float Radius;

	public int MaxEnemies;

	public float RespawnDelay;

	GameController gameController;

	Coroutine respawnCo = null;


	void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		Respawn();
		respawnCo = StartCoroutine(RespawnFunction());
	}

	void Update()
	{
		if (!GameController.GameStarted) return;
		if(respawnCo == null && transform.childCount < MaxEnemies)
		{
			respawnCo = StartCoroutine(RespawnFunction());
		}
	}

	IEnumerator RespawnFunction()
	{
		while(transform.childCount < MaxEnemies)
		{
			yield return new WaitForSeconds(RespawnDelay * Random.Range(0.5f, 1.5f));
			if (GameController.GameStarted)
			{
				Respawn();
			}
		}
		respawnCo = null;
	}

	void Respawn()
	{
		var obj = Instantiate(EnemyPrefab);
		obj.transform.SetParent(transform, false);
		obj.transform.localPosition = Random.insideUnitCircle * Radius;
		var level = gameController.GetLevel();
		obj.GetComponent<EnemyController>().SetLevel(level, level + 3);
	}
}
