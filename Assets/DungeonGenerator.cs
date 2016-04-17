using UnityEngine;
using System.Collections;
using System;

public class DungeonGenerator : MonoBehaviour
{
	public int Radius = 2;


	public GameObject WallExternalClosed;
	public GameObject WallExternalOpen;
	public GameObject WallInternalClosed;
	public GameObject WallInternalOpen;

	public float WallSize;

	/*
	public GameObject gridPrefab;

	public float DistanceBetweenCells = 0.7;
	*/
	class Coord
	{
		public Coord(int x, int y) { this.x = x;  this.y = y; }
		public int x, y;
	}

	struct Cell
	{
		public GameObject go;
		public Coord tl, tr, r, br, bl, l;
	}

	Cell[,] cells;


	public static Vector2 CellCoords(int i, int j)
	{
		return new Vector2((j + Mathf.Cos(60 * Mathf.PI / 180) * (i)), Mathf.Sin(60 * Mathf.PI / 180) * (i));
	}

	void Start()
	{
		cells = new Cell[2 * Radius - 1, 2 * Radius - 1];
		for(int i = 0; i < cells.GetLength(0); ++i)
		{
			int start = Math.Max(Radius-i-1, 0);
			int end = cells.GetLength(1) - Math.Max(i + 1 - Radius, 0);
			for(int j = start; j < end; ++j)
			{
				/*
				GameObject obj = Instantiate(gridPrefab);

				obj.transform.SetParent(transform, false);
				obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * i) * DistanceBetweenCells, Mathf.Sin(60 * Mathf.PI / 180) * i * DistanceBetweenCells);
				*/
				if(i > Math.Max(Radius-j-1, 0))
				{
					cells[i, j].l = new Coord(i-1, j);
					var obj = Instantiate(WallInternalOpen);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i+0.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i-0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(-30, new Vector3(0, 0, 1));
				}
				else
				{
					GameObject obj;
					if (i == 0 && j == start)
					{
						obj = Instantiate(WallExternalOpen);
					}
					else
					{
						obj = Instantiate(WallExternalClosed);
					}
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i+0.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i-0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(-30, new Vector3(0, 0, 1));
				}

				if(i < cells.GetLength(0) - 1 - Math.Max(j + 1 - Radius, 0))
				{
					cells[i, j].r = new Coord(i + 1, j);
				}
				else
				{
					var obj = Instantiate(WallExternalClosed);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i + 1.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i + 0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(150, new Vector3(0, 0, 1));
				}

				if(j > 0 && i >= Radius - j)
				{
					cells[i, j].tl = new Coord(i, j - 1);
					var obj = Instantiate(WallInternalOpen);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * i) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * i * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
				}
				else
				{
					var obj = Instantiate(WallExternalClosed);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * i) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * i * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(-90, new Vector3(0, 0, 1));
				}

				if (i > 0 && j < cells.GetLength(1) - 1)
				{
					cells[i, j].bl = new Coord(i - 1, j + 1);
				}
				else
				{
					var obj = Instantiate(WallExternalClosed);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i + 1.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i - 0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(30, new Vector3(0, 0, 1));
				}

				if(i < cells.GetLength(0)-1 && j > 0)
				{
					cells[i, j].tr = new Coord(i + 1, j - 1);
					var obj = Instantiate(WallInternalOpen);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i+0.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i+0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(210, new Vector3(0, 0, 1));
				}
				else
				{
					var obj = Instantiate(WallExternalClosed);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i+0.5f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i+0.5f) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(210, new Vector3(0, 0, 1));
				}

				if (i < cells.GetLength(0) - j + Radius-1 && j < cells.GetLength(1) - 1 - Math.Max(i + 1 - Radius, 0))
				{
					cells[i, j].br = new Coord(i, j + 1);
				}
				else
				{
					var obj = Instantiate(WallExternalClosed);
					obj.transform.position = new Vector3((j + Mathf.Cos(60 * Mathf.PI / 180) * (i+2f)) * WallSize, Mathf.Sin(60 * Mathf.PI / 180) * (i) * WallSize);
					obj.transform.rotation = Quaternion.AngleAxis(90, new Vector3(0, 0, 1));
				}
			}
		}
	}
}
