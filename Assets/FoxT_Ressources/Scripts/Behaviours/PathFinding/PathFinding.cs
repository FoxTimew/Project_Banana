using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class PathFinding : MonoBehaviour
{

	//public Transform seeker, target;
	//PathRequestManager requestManager;
	Grid grid;

	private void Awake()
	{
		//requestManager = GetComponent<PathRequestManager>();
		grid = GetComponent<Grid>();
	}

	/*public void StartFindPath(Vector2 startPos, Vector2 targetPos)
	{
		StartCoroutine(FindPath(startPos, targetPos));
	}*/

	public void /*IEnumerator*/ FindPath(PathRequest request, Action<PathResult> callbak/*Vector2 startPos, Vector2 targetPos*/)
	{
		/*Stopwatch sw = new Stopwatch();
		sw.Start();*/

		Vector2[] waypoints = new Vector2[0];
		bool pathSuccess = false;

		Node2D startNode = grid.NodeFromWorldPoint(request.pathStart/*startPos*/);
		Node2D targetNode = grid.NodeFromWorldPoint(request.pathEnd/*targetPos*/);

		if (/*startNode.walkable &&*/ targetNode.walkable)
		{
			Heap<Node2D> openSet = new Heap<Node2D>(grid.MaxSize);
			HashSet<Node2D> closedSet = new HashSet<Node2D>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				Node2D currentNode = openSet.RemoveFirst();
				closedSet.Add(currentNode);

				if (currentNode == targetNode)
				{
					/*sw.Stop();
					print("Path found : " + sw.ElapsedMilliseconds + " ms");*/
					pathSuccess = true;
					break;
				}

				foreach (Node2D neighbours in grid.GetNeighbours(currentNode))
				{
					if (!neighbours.walkable || closedSet.Contains(neighbours)) continue;

					int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbours);
					if (newMovementCostToNeighbour < neighbours.gCost || !openSet.Contains(neighbours))
					{
						neighbours.gCost = newMovementCostToNeighbour;
						neighbours.hCost = GetDistance(neighbours, targetNode);
						neighbours.parent = currentNode;

						if (!openSet.Contains(neighbours)) openSet.Add(neighbours);
						else openSet.UpdateItem(neighbours);
					}
				}
			}
		}
		//yield return null;
		if (pathSuccess)
		{
			waypoints = RetracePath(startNode, targetNode);
			pathSuccess = waypoints.Length > 0;
		}
		//requestManager.FinishProcessingPath(waypoints, pathSuccess);
		callbak(new PathResult(waypoints, pathSuccess, request.callback));
	}

	Vector2[] RetracePath(Node2D startNode, Node2D endNode)
	{
		List<Node2D> path = new List<Node2D>();
		Node2D currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
	}

	Vector2[] SimplifyPath(List<Node2D> path)
	{
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i++)
		{
			Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
			if (directionNew != directionOld) waypoints.Add(path[i - 1].worldPosition);
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}

	int GetDistance(Node2D nodeA, Node2D nodeB)
	{ 
		int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (distX > distY) return 14 * distY + 10 * (distX - distY);
		return 14 * distX + 10 * (distY - distX);
	}
}
