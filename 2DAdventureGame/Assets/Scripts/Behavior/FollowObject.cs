using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {
    public GameObject objectFollowed;
    public Vector2 OffSet;
	
    void Update () {
        transform.position = objectFollowed.transform.position + (Vector3)OffSet;	
	}
}
