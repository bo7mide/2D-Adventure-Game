using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

    public enum FollowType
    {
        MoveForward , Lerp
    }
    public FollowType type= FollowType.MoveForward;
    public PathDefinition path;
    public float speed = 1;
    public float MaxDistanceToGoal = .1f;
    public IEnumerator<Transform> currentPoint;

    public void Start()
    {
        if(path == null)
        {
            Debug.LogError("Path cannot be null",gameObject);
            return;
        }
        currentPoint = path.GetPathEnumerator();
        currentPoint.MoveNext();
        if (currentPoint.Current == null)
            return;
        transform.position = currentPoint.Current.position;
        
    }
    public void Update()
    {
        if (currentPoint == null || currentPoint.Current == null)
            return;
        if (type == FollowType.MoveForward)
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.Current.position, Time.deltaTime * speed);
        else
            transform.position = Vector3.Lerp(transform.position, currentPoint.Current.position, Time.deltaTime * speed);

        var distanceSquared = (transform.position - currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
            currentPoint.MoveNext();
    }

}
