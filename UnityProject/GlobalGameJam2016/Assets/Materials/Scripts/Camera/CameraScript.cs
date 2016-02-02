using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
    public GameObject Target;
    [Tooltip("An empty Gameobject parentet to the Charakter where the Head will be")]
    public Transform CameraRotationPivot;
    public Movement MovementScript;

    public float Distance = 5;
    [Tooltip("Minmal Distance from the Target")]
    public float MinDistance = 2;
    [Tooltip("Maximal Distance from the target")]
    public float MaxDistance = 6;
    [Tooltip("Height above the Target")]
    public float Height = 1.5f;
    [Tooltip("Speed of the Free Camera")]
    public float Sensivity = 8;
    [Tooltip("Maximal Degrees for the X angle")]
    public float MaxXRotation = 85;
    [Tooltip("Minimum Degrees for the X angle")]
    public float MinXRotation = 0;
    [Tooltip("Smoothness of the Camera rotation : fewer = smoother")]
    public float RotationDamping = 0.15f;
    [Tooltip("Smoothness of the Camera Heigt differnce : fewer = smoother")]
    public float HeightDamping = 0.05f;

    private Transform myTransform;
    private Transform targetTransform;

    private float camX;
    private float camY;

	void Awake() 
    {
        myTransform = GetComponent<Transform>();
        targetTransform = Target.GetComponent<Transform>();
	}
	
	void LateUpdate() 
    {
        GetDistance();
        CheckForObstacles();
        UpdatePositionAndRotation();
	}

    private void CheckForObstacles()
    {
        RaycastHit hit = new RaycastHit();

        Ray ray = new Ray(CameraRotationPivot.position, myTransform.position);

        if (Physics.Raycast(ray, out hit, Vector3.Distance(myTransform.position, CameraRotationPivot.position)))
        {
            if (hit.transform.gameObject != CameraRotationPivot.gameObject)
                Distance = Mathf.Clamp(hit.distance, MinDistance, MaxDistance);
            return;
        }

        if (Distance < (MinDistance + MaxDistance) / 2)
            Distance += HeightDamping;
    }

    private void GetDistance()
    {
        Distance -= Input.GetAxis("Mouse ScrollWheel");

        if (Distance > MaxDistance)
            Distance = MaxDistance;

        if (Distance < MinDistance)
            Distance = MinDistance;
    }

    private void UpdatePositionAndRotation()
    {
        if (Input.GetMouseButton(1))
        {
            MovementScript.CanRotate = false;

            camX += Input.GetAxis("Mouse X") * Sensivity;
            camY -= Input.GetAxis("Mouse Y") * Sensivity;

            camY = Mathf.Clamp(camY, MinXRotation, MaxXRotation);

            var rotation = Quaternion.Euler(camY, camX, 0);
            var position = rotation * new Vector3(0.0f, 0.0f, -Distance) + CameraRotationPivot.position;

            myTransform.rotation = rotation;
            myTransform.position = position;
            return;
        }
        else
        {
            camX = myTransform.eulerAngles.x;
            camY = myTransform.eulerAngles.y;

            MovementScript.CanRotate = true;

            float wantedRotationAngleY = targetTransform.eulerAngles.y;
            float wantedHeight = targetTransform.position.y + Height;

            float currentRotationAngleY = myTransform.eulerAngles.y;

            float currentHeight = myTransform.position.y;

            currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, RotationDamping);

            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngleY, 0);

            myTransform.position = targetTransform.position;
            myTransform.position -= currentRotation * Vector3.forward * Distance;

            myTransform.position = new Vector3(myTransform.position.x, currentHeight, myTransform.position.z);

            myTransform.LookAt(targetTransform);
        }
    }
}
