using UnityEngine;
using System.Collections;

public class FollowTarget2D : MonoBehaviour
{
	public Transform Target;

	public Vector2 Offset = new Vector2(0, 0);

	public float FreeMoveDistance = 0;

	void Update()
	{
		var pos3 = transform.position;
		var pos = new Vector2(pos3.x, pos3.y);
		var targetPos = Target.position;
		Vector2 dist = new Vector2(targetPos.x + Offset.x, targetPos.y + Offset.y);

		dist = dist - pos;

		var mag = dist.magnitude;
		if (mag >= FreeMoveDistance)
		{
			if (mag > 0)
			{
				dist = dist / mag * FreeMoveDistance;
			}
			else
			{
				dist = Vector2.zero;
			}
			pos3.x = targetPos.x - dist.x;
			pos3.y = targetPos.y - dist.y;

			transform.position = pos3;
		}
	}
}
