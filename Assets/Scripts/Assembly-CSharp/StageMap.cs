using UnityEngine;

public class StageMap
{
	public const int ROW_MAX = 5;

	public const int COL_MAX = 9;

	public const float GRID_TOP = 2.3f;

	public const float GRID_LEFT = -2f;

	public const float GRID_BOTTOM = -2.6f;

	public const float GRID_RIGHT = 5.2f;

	public const float GRID_WIDTH = 0.8f;

	public const float GRID_HEIGHT = 1f;

	public static Vector3 GetPlantPos(int row, int col)
	{
		return new Vector3(-1.5f + (float)col * 0.8f, 1.5999999f - (float)row * 1f, 0f);
	}

	public static Vector3 GetZombiePos(int row)
	{
		float num = Random.Range(1f, 2f);
		return new Vector3(5.2f + num, 1.5999999f - (float)row * 1f, 0f);
	}

	public static bool IsPointInMap(Vector3 point)
	{
		return point.x <= 5.2f && point.x >= -2f && point.y <= 2.3f && point.y >= -2.6f;
	}

	public static void GetRowAndCol(Vector3 point, out int row, out int col)
	{
		col = Mathf.FloorToInt((point.x - -2f) / 0.8f);
		row = Mathf.FloorToInt((2.3f - point.y) / 1f);
	}
}
