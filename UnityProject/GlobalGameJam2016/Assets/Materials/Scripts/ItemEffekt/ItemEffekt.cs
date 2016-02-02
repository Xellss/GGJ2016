using UnityEngine;
using System.Collections;

public class ItemEffekt : MonoBehaviour {

    public float RotateSpeed = 45;
    void FixedUpdate()
    {
        transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
    }
}
