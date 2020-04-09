using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocatePath : MonoBehaviour
{
	private Dijkstra m_dijkstra;
	private MapReaderMono m_MapMono;
	private List<Vector3> m_WorldPos;

	public List<Vector3> GetWorldPos => m_WorldPos;
	public MapReaderMono MapReaderMono => m_MapMono;

	private void Start()
	{
		m_MapMono = GetComponent<MapReaderMono>();

		GetPath();
	}


	private void GetPath()
	{
		m_dijkstra = new Dijkstra(m_MapMono.FoundPath);

		List<Vector2Int> translateToVec3;
		translateToVec3 = m_dijkstra.FindPath(m_MapMono.StartPos, m_MapMono.EndPos).ToList();

		m_WorldPos = new List<Vector3>();
		foreach (var item in translateToVec3)
		{
			m_WorldPos.Add(new Vector3(item.x * 2, 0, item.y * 2));
		}
	}
}
