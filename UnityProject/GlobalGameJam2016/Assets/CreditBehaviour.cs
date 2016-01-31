using UnityEngine;
using System.Collections;

public class CreditBehaviour : MonoBehaviour {

	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }	
	}
}
