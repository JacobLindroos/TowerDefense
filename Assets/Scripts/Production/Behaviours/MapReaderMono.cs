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
	[SerializeField] public TextAsset m_TxtFile;

	private List<Vector2Int> m_FoundPath;
	private Vector2Int m_StartPos;
	private Vector2Int m_EndPos;
	private MapReader m_MapReader;
	private CreateMap m_CreateMap;

	public CreateMap CreateMap => m_CreateMap;
	public List<Vector2Int> FoundPath => m_FoundPath;
	public Vector2Int StartPos => m_StartPos;
	public Vector2Int EndPos => m_EndPos;

	private void Awake()
	{
		m_FoundPath = new List<Vector2Int>();
		List<MapKeyData> data = new List<MapKeyData>();

		foreach (MapKeyDataMono readerMono in m_MapReaderMono)
		{
			MapKeyData d = new MapKeyData(readerMono.TileType, readerMono.Prefab);
			data.Add(d);
		}

		m_MapReader = new MapReader();
		m_CreateMap = new CreateMap(data, m_TxtFile);
		m_CreateMap.MapCreator();

		foreach (var item in m_CreateMap.WalkablePath)
		{
			m_FoundPath.Add(new Vector2Int(item.x / 2, item.y / 2));
		}

		m_StartPos = new Vector2Int(m_CreateMap.StartTilePos.x / 2, m_CreateMap.StartTilePos.y / 2);
		m_EndPos = new Vector2Int(m_CreateMap.EndTilePos.x / 2, m_CreateMap.EndTilePos.y / 2);
	}

	private void Start()
	{
		Time.timeScale = 2;
	}
}
