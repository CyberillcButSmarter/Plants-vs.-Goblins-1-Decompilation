using System.Collections;
using UnityEngine;

public class SearchZombie : MonoBehaviour
{
	private GameModel model;

	private void Awake()
	{
		model = GameModel.GetInstance();
	}

	public bool IsZombieInRange(int row, float min, float max)
	{
		foreach (GameObject item in model.zombieList[row])
		{
			float num = item.transform.position.x - base.transform.position.x;
			if (min <= num && num <= max)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsZombieInRange(float range)
	{
		for (int i = 0; i < 5; i++)
		{
			foreach (GameObject item in model.zombieList[i])
			{
				float num = Vector3.Distance(item.transform.position, base.transform.position);
				if (num <= range)
				{
					return true;
				}
			}
		}
		return false;
	}

	public GameObject SearchClosetZombie(int row, float min, float max)
	{
		float num = 10000f;
		GameObject result = null;
		foreach (GameObject item in model.zombieList[row])
		{
			float num2 = item.transform.position.x - base.transform.position.x;
			if (min <= num2 && num2 <= max && Mathf.Abs(num2) < num)
			{
				num = Mathf.Abs(num2);
				result = item;
			}
		}
		return result;
	}

	public GameObject SearchClosetZombie(float range)
	{
		float num = 10000f;
		GameObject result = null;
		for (int i = 0; i < 5; i++)
		{
			foreach (GameObject item in model.zombieList[i])
			{
				float num2 = Vector3.Distance(item.transform.position, base.transform.position);
				if (num2 < range && Mathf.Abs(num2) < num)
				{
					num = Mathf.Abs(num2);
					result = item;
				}
			}
		}
		return result;
	}

	public object[] SearchZombiesInRange(int row, float min, float max)
	{
		ArrayList arrayList = new ArrayList();
		foreach (GameObject item in model.zombieList[row])
		{
			float num = item.transform.position.x - base.transform.position.x;
			if (min <= num && num <= max)
			{
				arrayList.Add(item);
			}
		}
		return arrayList.ToArray();
	}

	public object[] SearchZombiesInRange(float range)
	{
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < 5; i++)
		{
			foreach (GameObject item in model.zombieList[i])
			{
				float num = Vector3.Distance(item.transform.position, base.transform.position);
				if (num <= range)
				{
					arrayList.Add(item);
				}
			}
		}
		return arrayList.ToArray();
	}
}
