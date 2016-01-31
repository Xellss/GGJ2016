using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour 
{
    public float DeleteAfterSeconds;

	void Start() 
    {
        StartCoroutine(Timer());
	}
	
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(DeleteAfterSeconds);
        GameObject.Destroy(this.gameObject);
    }
}
