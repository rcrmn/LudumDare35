using UnityEngine;
using System.Collections;

public class UI_PageFlipper : MonoBehaviour
{
	public float TimeToNextPage;
	public GameObject NextPage;

	void Update()
	{
		TimeToNextPage -= Time.deltaTime;
		if(TimeToNextPage <= 0)
		{
			NextPage.SetActive(true);
			gameObject.SetActive(false);
		}
		else if(Input.GetMouseButtonDown(0))
		{
			NextPage.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
