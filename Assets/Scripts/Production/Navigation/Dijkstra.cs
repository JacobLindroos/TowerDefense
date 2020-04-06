using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace AI
{
	//TODO: Implement IPathFinder using Dijsktra algorithm.
	public class Dijkstra : IPathFinder
	{
		private readonly HashSet<Vector2Int> m_RightPosition;
		public Dijkstra(IEnumerable<Vector2Int> rightPosition)
		{
			m_RightPosition = new HashSet<Vector2Int>(rightPosition);
		}

		public IEnumerable<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
		{
			Vector2Int currentNode = start;
			Dictionary<Vector2Int, Vector2Int?> ancestors = new Dictionary<Vector2Int, Vector2Int?>(){ { currentNode, null} };
			Queue<Vector2Int> frontier = new Queue<Vector2Int>(new[] { currentNode });

			while (frontier != null)
			{
				currentNode = frontier.Dequeue();

				if (currentNode == goal)
				{
					break;
				}

				foreach (Vector2Int direction in Tools.DirectionTools.Dirs)
				{
					Vector2Int next = currentNode + direction;

					if (m_RightPosition.Contains(next) && !ancestors.ContainsKey(next))
					{
						frontier.Enqueue(next);
						ancestors[next] = currentNode;
					}
				}
			}

			if (!ancestors.ContainsKey(goal))
			{
				return null;
			}


			List<Vector2Int> finalPath = new List<Vector2Int>();
			if (ancestors.ContainsKey(goal))
			{
				for (Vector2Int? run = goal; run.HasValue; run = ancestors[run.Value])
				{
					finalPath.Add(run.Value);
				}

				finalPath.Reverse();
				return finalPath;
			}

			return Enumerable.Empty<Vector2Int>();
		}
	}    
}
