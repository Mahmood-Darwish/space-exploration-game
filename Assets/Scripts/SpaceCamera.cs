using UnityEngine;


public class SpaceCamera : MonoBehaviour
{
    //  Properties about our camera.
    [SerializeField]
    float rotateSpeed = 90.0f;
    [SerializeField]
    bool usedFixedUpdate = true;

    //  Which camera we should switch to when pressing H.
    [SerializeField]
    GameObject FPSCamera;

    //  Support for our calculations.
    private Transform target;
    private Vector3 startOffset;

    //  Save the place we want our camera to be in and let it deattach itself from the object.
    private void Start()
    {
        target = transform.parent;
        startOffset = transform.localPosition;
        transform.SetParent(null);
    }

    //  Change view if we press H. Update camera if wanted.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FPSCamera.SetActive(true);
            gameObject.SetActive(false);
        }
        if (!usedFixedUpdate)
            UpdateCamera();
    }

    //  Update camera if wanted.
    private void FixedUpdate()
    {
        if (usedFixedUpdate)
            UpdateCamera();
    }

    //  Move the camera to the required position.
    private void UpdateCamera()
    {
        transform.position = target.TransformPoint(startOffset);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation * Quaternion.Euler(10, 90, 0) , rotateSpeed * Time.deltaTime);
    }
}

