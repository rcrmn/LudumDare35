using UnityEngine;
using System.Collections;

public class ControlPlayerMovement : MonoBehaviour
{
	public float Speed = 1;

	void Update()
	{
		var pos = transform.position;
		pos.x += Speed * Input.GetAxis("Horizontal");
		pos.y += Speed * Input.GetAxis("Vertical");
		transform.position = pos;

		var scPos = Camera.main.WorldToScreenPoint(pos);

		var mouse = new Vector2(Input.mousePosition.x-scPos.x, Input.mousePosition.y-scPos.y);

		mouse = new Vector2(mouse.x / Screen.width, mouse.y / Screen.height);

		mouse.Normalize();

		transform.rotation = Quaternion.FromToRotation(new Vector3(1, 0, 0), new Vector3(mouse.x, mouse.y, 0));
	}
}
