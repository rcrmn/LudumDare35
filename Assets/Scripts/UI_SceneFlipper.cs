using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_SceneFlipper : MonoBehaviour {

	public float TimeToNextScene;
	public int NextScene;

	void Update()
	{
		TimeToNextScene -= Time.deltaTime;
		if(TimeToNextScene <= 0)
		{
			SceneManager.LoadScene(NextScene);
		}
		else if(Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene(NextScene);
		}
	}
}
