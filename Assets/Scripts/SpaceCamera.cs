using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SpaceCamera : MonoBehaviour
{

    public float rotateSpeed = 90.0f;
    public bool usedFixedUpdate = true;
    private Transform target;
    private Vector3 startOffset;
    [SerializeField]
    GameObject FPSCamera;

    private void Start()
    {
        target = transform.parent;

        if (target == null)
            Debug.LogWarning(name + ": Lag Camera will not function correctly without a target.");
        if (transform.parent == null)
            Debug.LogWarning(name + ": Lag Camera will not function correctly without a parent to derive the initial offset from.");

        startOffset = transform.localPosition;
        transform.SetParent(null);
    }

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

    private void FixedUpdate()
    {
        if (usedFixedUpdate)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (target != null)
        {
            transform.position = target.TransformPoint(startOffset);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation * Quaternion.Euler(10, 90, 0) , rotateSpeed * Time.deltaTime);
        }
    }
}

