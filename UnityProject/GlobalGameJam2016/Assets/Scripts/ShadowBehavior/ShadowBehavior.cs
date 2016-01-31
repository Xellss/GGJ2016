using UnityEngine;
using System.Collections;

public class ShadowBehavior : MonoBehaviour {

    [HideInInspector]
    public bool InShadow = false;

    private GameObject objectSun;
    private Transform sun;
    private RaycastHit hit;

	void Awake ()
    {
        objectSun = GameObject.Find("Directional Light");
        sun = objectSun.transform;
	}
	
	void Update ()
    {
        Physics.Raycast(transform.position, sun.forward * -1, out hit);

        if (hit.transform == null || hit.transform.gameObject.tag == "Enemy")
        {
            InShadow = false;
            return;
        }
        else
        {
            InShadow = true;
        }

	}
}
