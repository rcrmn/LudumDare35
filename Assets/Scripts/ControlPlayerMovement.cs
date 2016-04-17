using UnityEngine;
using System.Collections;

public class ControlPlayerMovement : MonoBehaviour
{
	public float Speed = 1;

	void FixedUpdate()
	{
		if (!GameController.GameStarted) return;

		var pos = transform.position;
		pos.x += Speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime;
		pos.y += Speed * Input.GetAxis("Vertical") * Time.fixedDeltaTime;
		transform.position = pos;

		var scPos = Camera.main.WorldToScreenPoint(pos);

		var mouse = new Vector2(Input.mousePosition.x-scPos.x, Input.mousePosition.y-scPos.y);

		mouse = new Vector2(mouse.x / Screen.width, mouse.y / Screen.height);

		mouse.Normalize();

		transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), new Vector3(mouse.x, mouse.y, 0));
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		other.SendMessage("OnPlayerEnter", gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		other.SendMessage("OnPlayerExit", gameObject, SendMessageOptions.DontRequireReceiver);
	}
}
