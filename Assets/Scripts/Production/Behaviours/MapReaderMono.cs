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


	private MapReader m_MapReader;
	private CreateMap createMap;

	private void Awake()
	{
		List<MapKeyData> data = new List<MapKeyData>();

		foreach (MapKeyDataMono readerMono in m_MapReaderMono)
		{
			MapKeyData d = new MapKeyData(readerMono.TileType, readerMono.Prefab);
			data.Add(d);
		}

		m_MapReader = new MapReader();
		createMap = new CreateMap(data, txtFile);
		createMap.MapCreator();
	}
}
