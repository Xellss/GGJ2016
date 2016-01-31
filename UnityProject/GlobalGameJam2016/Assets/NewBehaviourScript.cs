using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    public Transform target;

    private Vector3 firstPosition;
	void Start ()
    {
        firstPosition = target.position;
	}
	
	void FixedUpdate ()
    {
        Vector3 newPosition = new Vector3(target.position.x, firstPosition.y, target.position.z);
	}
}
