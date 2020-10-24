using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	public bool OnlyDisplayPathGizmos;
	public Transform objectLinked;
	public LayerMask obstacleMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	Node2D[,] grid;

	float nodeDiamater;
	int gridSizeX, gridSizeY;

	private void Start()
	{
		nodeDiamater = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);
		CreateGrid();
	}

	public int MaxSize { get { return gridSizeX * gridSizeY; } }

	void CreateGrid()
	{
		grid = new Node2D[gridSizeX, gridSizeY];
		Vector2 worldBottomLeft = new Vector2(transform.position.x, transform.position.y )- Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				bool walkable;
				Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiamater + nodeRadius) + Vector2.up * (y * nodeDiamater + nodeRadius);
				Collider2D obstacle = Physics2D.OverlapCircle(worldPoint, nodeRadius, obstacleMask);
				if (obstacle != null) walkable = false;
				else walkable = true;
				grid[x, y] = new Node2D(walkable, worldPoint, x, y);
			}
		}
	}

	public List<Node2D> GetNeighbours(Node2D node)
	{
		List<Node2D> neighbours = new List<Node2D>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if(x ==0 && y ==0) continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) neighbours.Add(grid[checkX, checkY]);
			}
		}

		return neighbours;
	}

	public Node2D NodeFromWorldPoint(Vector2 worldPosition)
	{
		float pourcentX = Mathf.Clamp01((worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
		float pourcentY = Mathf.Clamp01((worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y);
		
		int x = Mathf.RoundToInt((gridSizeX - 1) * pourcentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * pourcentY);

		return grid[x, y];
	}

	public List<Node2D> path;

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0f));

		if (OnlyDisplayPathGizmos)
		{
			if (path != null)
			{
				foreach (Node2D n in path)
				{
					Gizmos.color = Color.black;
					Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiamater - 0.1f));
				}
			}
		}
		else
		{
			if (grid != null)
			{
				Node2D objectNode = NodeFromWorldPoint(objectLinked.position);
				foreach (Node2D n in grid)
				{
					Gizmos.color = (n.walkable) ? Color.white : Color.red;
					if (path != null)
					{
						if (path.Contains(n)) Gizmos.color = Color.black;
					}
					if (objectNode == n) Gizmos.color = Color.green;
					Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiamater - 0.1f));
				}
			}
		}
	}

}
