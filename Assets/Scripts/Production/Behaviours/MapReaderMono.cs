using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapKeyDataMono
{
	[SerializeField] private TileType m_Type;
	[SerializeField] private GameObject m_Prefab;

	public TileType TileType => m_Type;
	public GameObject Prefab  => m_Prefab;
}


public class MapReaderMono : MonoBehaviour
{
	[SerializeField] private MapKeyDataMono[] m_MapReaderMono;
	[SerializeField] public TextAsset txtFile;

	private List<Vector2Int> foundPath;
	private Vector2Int startPos;
	private Vector2Int endPos;
	private MapReader m_MapReader;
	private CreateMap createMap;


	public List<Vector2Int> FoundPath => foundPath;
	public Vector2Int StartPos => startPos;
	public Vector2Int EndPos => endPos;

	private void Awake()
	{
		foundPath = new List<Vector2Int>();
		List<MapKeyData> data = new List<MapKeyData>();

		foreach (MapKeyDataMono readerMono in m_MapReaderMono)
		{
			MapKeyData d = new MapKeyData(readerMono.TileType, readerMono.Prefab);
			data.Add(d);
		}

		m_MapReader = new MapReader();
		createMap = new CreateMap(data, txtFile);
		createMap.MapCreator();

		foreach (var item in createMap.WalkablePath)
		{
			foundPath.Add(new Vector2Int(item.x / 2, item.y / 2));
		}

		startPos = new Vector2Int(createMap.StartTilePos.x / 2, createMap.StartTilePos.y / 2);
		endPos = new Vector2Int(createMap.EndTilePos.x / 2, createMap.EndTilePos.y / 2);
	}
}
