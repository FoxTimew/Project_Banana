using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	const float minPathUpdateTime = 0.1f;
	const float pathUpdateMoveThreshold = 0.5f;

	public Transform target;
	public float speed = 5;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppindDst = 10;
	/*Vector2[] path;
	int targetIndex;*/
	Path path;
	EnemySys sys;
	private void Awake()
	{
		sys = this.GetComponentInParent<EnemySys>();
	}

	private void Start()
	{
		StartCoroutine(UpdtaePath());
	}

	public void OnPathFound(Vector2[] waypoints/*newPath*/, bool pathSuccessful)
	{
		if (pathSuccessful)
		{
			path = new Path(waypoints, transform.position, turnDst, stoppindDst);//newPath;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	IEnumerator UpdtaePath()
	{
		if (Time.timeSinceLevelLoad < 0.3f)
		{
			yield return new WaitForSeconds(0.3f);
		}
		PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector2 targetPosOld = target.position;

		while (true)
		{
			yield return new WaitForSeconds(minPathUpdateTime);
			if ((new Vector2(target.position.x, target.position.y) - targetPosOld).sqrMagnitude > sqrMoveThreshold)
			{
				PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
				targetPosOld = new Vector2(target.position.x, target.position.y);
			}
		}
	}

	IEnumerator FollowPath()
	{
		//Vector2 currentWaypoint = path[0];
		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
			{
				if (pathIndex == path.finishLineIndex)
				{
					followingPath = false;
					sys.VectorDirector = Vector3.zero;
					break;
				}
				else
				{
					pathIndex++;
				}
			}

			if (followingPath)
			{
				Vector3 targetPos = rotationTrick(path.lookPoints[pathIndex]);
				Vector3 selfPof = rotationTrick(new Vector2 (transform.position.x, transform.position.y));
				Quaternion targetRotation = Quaternion.LookRotation(targetPos - selfPof/*path.lookPoints[pathIndex] - new Vector2(transform.position.x, transform.position.y)*/);
				targetRotation = new Quaternion(0f, 0f, -targetRotation.y, targetRotation.w);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector2.up, Space.Self);
				sys.VectorDirector = (transform.localPosition * 10);
				transform.localPosition = Vector3.zero;
			}
			/*if (new Vector2 (transform.position.x, transform.position.y) == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
			}
			transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);*/
			yield return null;
		}
	}

	Vector3 rotationTrick(Vector2 v)
	{
		return new Vector3(v.x, 0f, v.y);
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			path.DrawWithGizmos();
			/*for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector2.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}*/
		}
	}
}
