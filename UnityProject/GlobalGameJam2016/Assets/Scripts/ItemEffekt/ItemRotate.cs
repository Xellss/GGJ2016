using UnityEngine;
using System.Collections;

public class ItemRotate : MonoBehaviour {

    public float RotationSpeed = 45;
	
	void Update () {
        transform.Rotate(RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime, RotationSpeed * Time.deltaTime);
	}
}
