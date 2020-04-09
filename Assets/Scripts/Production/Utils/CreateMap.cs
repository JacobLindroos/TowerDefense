using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateMap //POCO
{
	private MapReader m_MapReader;

	//init char for prefabs
	private const char m_CharPath = '0';
	private const char m_CharOsbtacle = '1';
	private const char m_CharTowerDefOne = '2';
	private const char m_CharTowerDefTwo = '3';
	private const char m_CharStartEnenmyBase = '8';
	private const char m_CharEndPlayerBase = '9';

	//cellsize
	[SerializeField] private int m_cellSize = 2;
	private float m_Offset = -0.75f;

	private GameObject m_EndObject;
	private Vector2Int m_StartTilePos;
	private Vector2Int m_EndTilePos;
	private char m_CurrentTileChar;
	private List<string> m_Lines;
	private List<Vector2Int> m_WalkablePath;

	private readonly Dictionary<TileType, GameObject> m_prefabById;

	public GameObject EndObject => m_EndObject;
	public List<Vector2Int> WalkablePath => m_WalkablePath;
	public Vector2Int StartTilePos => m_StartTilePos;
	public Vector2Int EndTilePos => m_EndTilePos;

	public CreateMap(IEnumerable<MapKeyData> mapKeyDatas, TextAsset txtFile)
	{
		m_prefabById = new Dictionary<TileType, GameObject>();
		foreach (MapKeyData data in mapKeyDatas)
		{
			m_prefabById.Add(data.Type, data.Prefab);
		}

		m_MapReader = new MapReader();

		m_Lines = new List<string>();
		m_Lines = m_MapReader.ReadTextFile(txtFile);
	}


	public void MapCreator()
	{
		m_WalkablePath = new List<Vector2Int>();
		for (int lineIndex = m_Lines.Count - 1, rowIndex = 0; lineIndex >= 0; lineIndex--, rowIndex++)
		{
			string line = m_Lines[lineIndex];

			for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
			{
				char item = line[columnIndex];
				
				float z = rowIndex * m_cellSize;
				float x = columnIndex * m_cellSize;
				switch (item)
				{
					case m_CharPath:
						m_CurrentTileChar = m_CharPath;
						break;
					case m_CharOsbtacle:
						m_CurrentTileChar = m_CharOsbtacle;
						break;
					case m_CharStartEnenmyBase:
						m_CurrentTileChar = m_CharStartEnenmyBase;
						break;
					case m_CharEndPlayerBase:
						m_CurrentTileChar = m_CharEndPlayerBase;
						break;
					case m_CharTowerDefOne:
						m_CurrentTileChar = m_CharTowerDefOne;
						break;
					case m_CharTowerDefTwo:
						m_CurrentTileChar = m_CharTowerDefTwo;
						break;
				}

				TileType tileType = TileMethods.TypeByIdChar[m_CurrentTileChar];
				GameObject currentPrefab = m_prefabById[tileType];
				Vector3 currentPos = new Vector3(x, m_Offset, z);
				GameObject test = GameObject.Instantiate(currentPrefab, currentPos, Quaternion.identity);

				if (TileMethods.IsWalkable(tileType))
				{
					Vector2Int localPos = new Vector2Int((int)test.transform.localPosition.x, (int)test.transform.localPosition.z);

					if (tileType == TileType.Start)
					{
						m_StartTilePos = localPos;
					}
					if (tileType == TileType.End)
					{
						m_EndTilePos = localPos;
						m_EndObject = test;
					}
					m_WalkablePath.Add(localPos);
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
