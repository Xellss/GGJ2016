using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {
    Animator animator;
    Movement movement;
    bool shooting_MUSSNOCHGEWECHSELTWERDEN;
	void Awake() {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
    }

    void Update ()
    {
        Move();
	}
    private void Move()
    {
        if (!Input.GetButton("Horizontal")&& !Input.GetButton("Vertical"))
        {
            animator.SetFloat("MoveSpeed", 0);
            movement.Speed = 0;
            return;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Vertical") >0.1f))
        {
            animator.SetFloat("MoveSpeed", 10);
            movement.Speed = 13;
        }
        else
        {
            animator.SetFloat("MoveSpeed", 3);
            movement.Speed = 7;
        }
    }
}
