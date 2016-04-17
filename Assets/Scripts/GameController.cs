using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Texture2D cursor;

	void Start()
	{
		Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
