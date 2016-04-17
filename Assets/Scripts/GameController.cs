using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
	public Texture2D cursor;

	public int PlayerLife;

	public float DamageDelay;

	public Scrollbar PlayerLifeDisplay;

	public Text EnemiesKilledLabel;

	public AudioClip EnemyHurtSound;
	public AudioClip EnemyKilledSound;
	public AudioClip PlayerHurtSound;
	public AudioClip PlayerLostSound;

	public GameObject GameFinishedScreen;

	public static bool GameStarted
	{
		get; private set;
	}

	int currentLife;
	float timeToDamage = 0;

	int enemiesKilled = 0;

	AudioSource audioS;

	void Start()
	{
		GameFinishedScreen.SetActive(false);
		Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
		currentLife = PlayerLife;
		audioS = GetComponent<AudioSource>();
		GameStarted = true;
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

			if(currentLife > 0)
			{
				audioS.PlayOneShot(PlayerHurtSound);
			}
			else
			{
				audioS.PlayOneShot(PlayerLostSound);
				GameFinishedScreen.SetActive(true);
				GameStarted = false;
				Invoke("Restart", 3.5f);
			}
		}
	}

	public void EnemyKilled()
	{
		enemiesKilled++;
		EnemiesKilledLabel.text = enemiesKilled + " Kills";
		audioS.PlayOneShot(EnemyKilledSound);
	}

	public void EnemyHurt()
	{
		audioS.PlayOneShot(EnemyHurtSound);
	}

	public int GetLevel()
	{
		return enemiesKilled / 5;
	}

	void Restart()
	{
		Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
		SceneManager.LoadScene(1);
	}
}
