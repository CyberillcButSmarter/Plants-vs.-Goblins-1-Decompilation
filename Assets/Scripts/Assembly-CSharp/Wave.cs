using System;
using UnityEngine;

[Serializable]
public struct Wave
{
	[Serializable]
	public struct Data
	{
		public ZombieType zombieType;

		public uint count;
	}

	public bool isLargeWave;

	[Range(0f, 1f)]
	public float percentage;

	public Data[] zombieData;
}
