using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_StartButton : MonoBehaviour
{
	public void Click()
	{
		SceneManager.LoadScene(2);
	}
}
