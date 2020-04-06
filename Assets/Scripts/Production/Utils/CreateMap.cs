using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateMap
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
	
	private char currentTileChar;
	private List<string> lines;
	private readonly Dictionary<TileType, GameObject> m_prefabById;

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
		for (int lineIndex = lines.Count - 1; lineIndex >= 0; lineIndex--)
		{
			string line = lines[lineIndex];

			for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
			{
				char item = line[columnIndex];
				
				float z = lineIndex * cellSize;
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
				GameObject.Instantiate(currentPrefab, new Vector3(x, 0, z), Quaternion.identity);
			}
		}
	}
}


/* 
 * List<Vector2Int>
 * 
 * find coordinates of objects in map
 * 
 * translate to world coordinates
 * 
 * Origin, distance between center of tiles
 * 
 * 
 * lagra alla positioner av '0', '8' och '9' i en lista med namn "walkable objects in world"
 * 
 * hitta närmsta vägen från '8' till '9' med BFS djikstra i listan "walkable objects in world"...
 * ...och lagra den närmsta vägen i en ny lista med namn "Closest path"
 * 
 * förflytta EnemyModel längs med listan "Closest Path"
 */


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
