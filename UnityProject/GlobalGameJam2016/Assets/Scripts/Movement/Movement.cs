using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Tooltip("Movement Speed")]
    public float Speed = 10;
    [Tooltip("Rotation Speed")]
    public float Sensivity = 2.5f;
    [Tooltip("How much Energie your jump has")]
    public float JumpForce = 0.1f;
    [Tooltip("How long the Energie will effect your Charakter in seconds")]
    public float JumpTime = 0.3f;

    [HideInInspector]
    public bool CanRotate = true;

    private CharacterController controller;
    private Transform myTransform;
    
    private float jumpCounter = 0;
    private bool Jumping = false;
    private float rotationY;

    private float weaponRot;

	void Start () 
    {
        controller = GetComponent<CharacterController>();
        myTransform = GetComponent<Transform>();
	}

	void Update () 
    {
        Jump();
        Move();
        Rotate();
        InitiateJump();
	}

    private void InitiateJump()
    {
        float i =Input.GetAxis("Jump");

        if (i != 0)
        {
            if (controller.isGrounded)
            {
                Jumping = true;
                StartCoroutine(JumpTimer());
            }
        }
    }

    private void Jump()
    {
        if (Jumping && jumpCounter <= (JumpTime / 2))
        {
            jumpCounter += Time.deltaTime;
            myTransform.Translate(Vector3.up * JumpForce);
            return;
        }

        if (Jumping && jumpCounter >= (JumpTime / 2))
        {
            jumpCounter += Time.deltaTime;
            myTransform.Translate(Vector3.up * JumpForce / 2);
            return;
        }
    }

    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(JumpTime);
        Jumping = false;
        jumpCounter = 0;
    }

    private void Move()
    {
        float translationHorizontal = Input.GetAxis("Horizontal") * Speed;
        float translationVertical = Input.GetAxis("Vertical") * Speed;

        controller.SimpleMove(myTransform.forward * translationVertical);
        controller.SimpleMove(myTransform.right * translationHorizontal);
    }

    private void Rotate()
    {
        if (!CanRotate)
            return;

        rotationY += Input.GetAxis("Mouse X") * Sensivity;

        var quaternion = Quaternion.Euler(0, rotationY, 0);

        myTransform.rotation = quaternion;
    }
}
