using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node2D : IHeapItem<Node2D> {

	public bool walkable;
	public Vector2 worldPosition;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public Node2D parent;
	int heapIndex;
	public Node2D(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY)
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	public int fCost { get { return gCost + hCost; } }

	public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

	public int CompareTo(Node2D nodeToCompare)
	{
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
		{
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}
