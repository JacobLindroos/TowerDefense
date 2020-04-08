using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocatePath : MonoBehaviour
{
	private Dijkstra dijkstra;
	private MapReaderMono mapMono;
	private List<Vector3> worldPos;

	public List<Vector3> GetWorldPos => worldPos;


	private void Start()
	{
		mapMono = GetComponent<MapReaderMono>();

		GetPath();
	}


	private void GetPath()
	{
		dijkstra = new Dijkstra(mapMono.FoundPath);

		List<Vector2Int> translateToVec3;
		translateToVec3 = dijkstra.FindPath(mapMono.StartPos, mapMono.EndPos).ToList();

		worldPos = new List<Vector3>();
		foreach (var item in translateToVec3)
		{
			worldPos.Add(new Vector3(item.x * 2, 0, item.y * 2));
		}
	}
}



/*
 * skapa våg-system
 *		- int waves
 *		- int enemysPerWave
 *		- float timeBetweenWaves
 *		- float timeBetweenSpawns
 *		- Function that Handle Wave and Spawning enemy's
 *			- All enemy's need to get the path to follow
 *		
 *		
 *		
 * få tornen att skjuta
 * ge hälsa till player base som tar skada om en enemyModel reach's playerBase
 * 
 */
