using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public Texture2D cursor;

	public int PlayerLife;

	public float DamageDelay;

	public Scrollbar PlayerLifeDisplay;

	public Text EnemiesKilledLabel;

	int currentLife;
	float timeToDamage = 0;

	int enemiesKilled = 0;

	void Start()
	{
		Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
		currentLife = PlayerLife;
	}

	// Update is called once per frame
	void Update()
	{
		timeToDamage -= Time.deltaTime;
	}

	public void DamagePlayer()
	{
		if (timeToDamage <= 0)
		{
			currentLife -= 1;
			PlayerLifeDisplay.size = currentLife * 1f / PlayerLife;
			timeToDamage = DamageDelay;
		}
	}

	public void EnemyKilled()
	{
		enemiesKilled++;
		EnemiesKilledLabel.text = enemiesKilled + " Kills";
	}

	public int GetLevel()
	{
		return enemiesKilled / 5;
	}
}
