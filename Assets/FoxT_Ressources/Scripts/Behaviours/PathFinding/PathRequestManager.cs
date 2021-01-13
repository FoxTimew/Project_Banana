using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
	/*Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	PathRequest currentPathRequest;*/

	Queue<PathResult> results = new Queue<PathResult>();

	static PathRequestManager instance;
	PathFinding pathfinding;

	//bool isProcessingPath;

	private void Awake()
	{
		instance = this;
		pathfinding = GetComponent<PathFinding>();
	}

	private void Update()
	{
		if (results.Count > 0)
		{
			int itemsInQueue = results.Count;
			lock (results)
			{
				for (int i = 0; i < itemsInQueue; i++)
				{
					PathResult result = results.Dequeue();
					result.callback(result.path, result.success);
				}
			}
		}
	}

	public static void RequestPath(PathRequest request/*Vector2 pathStart, Vector2 pathEnd, Action<Vector2[], bool> callback*/)
	{
		ThreadStart threadStart = delegate
		{
			instance.pathfinding.FindPath(request, instance.FinishProcessingPath);
		};
		threadStart.Invoke();
		/*PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
		instance.pathRequestQueue.Enqueue(newRequest);
		instance.TryProcessNext();*/
	}

	/*void TryProcessNext()
	{
		if (!isProcessingPath && pathRequestQueue.Count > 0)
		{
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}*/

	public void FinishProcessingPath(PathResult result/*Vector2[] path, bool success, PathRequest originalRequest*/)
	{
		//PathResult result = new PathResult(path, success, originalRequest.callback);
		lock (results)
		{
			results.Enqueue(result);
		}
		/*currentPathRequest.callback(path, success);
		isProcessingPath = false;
		TryProcessNext();*/
	}
}
public struct PathResult
{
	public Vector2[] path;
	public bool success;
	public Action<Vector2[], bool> callback;

	public PathResult(Vector2[] path, bool success, Action<Vector2[], bool> callback)
	{
		this.path = path;
		this.success = success;
		this.callback = callback;
	}
}

public struct PathRequest
{
	public Vector2 pathStart;
	public Vector2 pathEnd;
	public Action<Vector2[], bool> callback;

	public PathRequest(Vector2 _start, Vector2 _end, Action<Vector2[], bool> _callback)
	{
		pathStart = _start;
		pathEnd = _end;
		callback = _callback;
	}
}
