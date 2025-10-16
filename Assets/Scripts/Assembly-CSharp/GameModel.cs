using System.Collections;
using UnityEngine;

public class GameModel
{
	public GameObject[,] map;

	public ArrayList[] zombieList;

	public ArrayList[] bulletList;

	public int sun;

	public bool inDay;

	private static GameModel instance;

	private GameModel()
	{
		Clear();
	}

	public void Clear()
	{
		map = new GameObject[5, 9];
		zombieList = new ArrayList[5];
		bulletList = new ArrayList[5];
		for (int i = 0; i < 5; i++)
		{
			zombieList[i] = new ArrayList();
			bulletList[i] = new ArrayList();
		}
	}

	public static GameModel GetInstance()
	{
		if (instance == null)
		{
			instance = new GameModel();
		}
		return instance;
	}
}
