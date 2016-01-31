using UnityEngine;
using System.Collections;

public class TakeItem : MonoBehaviour {
    [HideInInspector]
    public int ItemCount = 0;

    public void ResetItemCount()
    {
        ItemCount = 0;
    }
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            GameObject.Destroy(other.gameObject);
            ItemCount++;
        }
	}
}
