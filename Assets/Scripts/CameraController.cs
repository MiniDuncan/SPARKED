using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;

    public float moveSpeed = 15f;

    [HideInInspector]
    public Transform endCamPos;

    // Start is called before the first frame update
    void Start()
    {
        AssignTarget();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (endCamPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, endCamPos.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, endCamPos.rotation, moveSpeed * Time.deltaTime);
        }
        else
        {

            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);

            if (transform.position.y < offset.y)
            {
                transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
            }
        }
    }

    private void AssignTarget()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;

            offset = transform.position;
        }
    }

    public void SnapToTarget()
    {
        AssignTarget();

        transform.position = target.position + offset;
    }
}
