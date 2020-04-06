using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateMap //POCO
{
	private MapReader mapReader;

	//init char for prefabs
	private const char charPath = '0';
	private const char charOsbtacle = '1';
	private const char charTowerDefOne = '2';
	private const char charTowerDefTwo = '3';
	private const char charStartEnenmyBase = '8';
	private const char charEndPlayerBase = '9';

	//cellsize
	[SerializeField] private int cellSize = 2;

	private Vector2Int startTilePos;
	private Vector2Int endTilePos;
	private char currentTileChar;
	private List<string> lines;
	private List<Vector2Int> walkablePath;

	private readonly Dictionary<TileType, GameObject> m_prefabById;

	public List<Vector2Int> WalkablePath => walkablePath;
	public Vector2Int StartTilePos => startTilePos;
	public Vector2Int EndTilePos => endTilePos;

	public CreateMap(IEnumerable<MapKeyData> mapKeyDatas, TextAsset txtFile)
	{
		m_prefabById = new Dictionary<TileType, GameObject>();
		foreach (MapKeyData data in mapKeyDatas)
		{
			m_prefabById.Add(data.Type, data.Prefab);
		}

		mapReader = new MapReader();

		lines = new List<string>();
		lines = mapReader.ReadTextFile(txtFile);
	}


	public void MapCreator()
	{
		walkablePath = new List<Vector2Int>();
		for (int lineIndex = lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
		{
			string line = lines[lineIndex];

			for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
			{
				char item = line[columnIndex];
				
				float z = rowIndex * cellSize;
				float x = columnIndex * cellSize;
				switch (item)
				{
					case charPath:
						currentTileChar = charPath;
						break;
					case charOsbtacle:
						currentTileChar = charOsbtacle;
						break;
					case charStartEnenmyBase:
						currentTileChar = charStartEnenmyBase;
						break;
					case charEndPlayerBase:
						currentTileChar = charEndPlayerBase;
						break;
					case charTowerDefOne:
						currentTileChar = charTowerDefOne;
						break;
					case charTowerDefTwo:
						currentTileChar = charTowerDefTwo;
						break;
				}

				TileType tileType = TileMethods.TypeByIdChar[currentTileChar];
				GameObject currentPrefab = m_prefabById[tileType];
				Vector3 currentPos = new Vector3(x, 0, z);
				GameObject test = GameObject.Instantiate(currentPrefab, currentPos, Quaternion.identity);

				if (TileMethods.IsWalkable(tileType))
				{
					Vector2Int localPos = new Vector2Int((int)test.transform.localPosition.x, (int)test.transform.localPosition.z);

					if (tileType == TileType.Start)
					{
						startTilePos = localPos;
					}
					if (tileType == TileType.End)
					{
						endTilePos = localPos;
					}
					walkablePath.Add(localPos);
				}
			}
		}
	}
}


public class MapKeyData
{
	public TileType Type { get; private set; }
	public GameObject Prefab { get; private set; }

	public MapKeyData(TileType type, GameObject prefab)
	{
		Type = type;
		Prefab = prefab;
	}
}
