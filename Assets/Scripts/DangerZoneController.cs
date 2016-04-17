using UnityEngine;
using System.Collections;

public class DangerZoneController : MonoBehaviour
{

	void OnPlayerEnter(GameObject obj)
	{
		transform.parent.SendMessage("OnPlayerEnter", obj, SendMessageOptions.DontRequireReceiver);
	}

	void OnPlayerExit(GameObject obj)
	{
		transform.parent.SendMessage("OnPlayerExit", obj, SendMessageOptions.DontRequireReceiver);
	}
}
