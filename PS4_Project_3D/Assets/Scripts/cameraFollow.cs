using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.one;

    private bool rotating = false;

    private int directions = 0;

    private void LateUpdate()
    {
        FollowTransform();
    }

    private void Update()
    {
        if(!rotating)
        {
            float dpadAxis = Input.GetAxis("Rotate");
            smoothSpeed = 0.0f;
            if (dpadAxis >= 1)
            {
                directions--;
                StartCoroutine(rotationChange());
            }
            else if (dpadAxis <= -1)
            {
                directions++;
                StartCoroutine(rotationChange());
            }
        }
    }
    void FollowTransform()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed);
        transform.position = smoothedPos;

        transform.LookAt(target);
    }

    IEnumerator rotationChange()
    {
        smoothSpeed = 0.1f;
        if (rotating)
        {
            yield return null;
        }
        if (directions > 3)
        {
            directions = 0;
        }
        else if (directions < 0)
        {
            directions = 3;
        }
        rotating = true;
        switch(directions)
        {
            case 0:
                {
                    Vector3 setOffset = new Vector3(-5, 5, -5);
                    offset = setOffset;
                    break;
                }
            case 1:
                {
                    Vector3 setOffset = new Vector3(-5, 5, 5);
                    offset = setOffset;
                    break;
                }
            case 2:
                {
                    Vector3 setOffset = new Vector3(5, 5, 5);
                    offset = setOffset;
                    break;
                }
            case 3:
                {
                    Vector3 setOffset = new Vector3(5, 5, -5);
                    offset = setOffset;
                    break;
                }
        }
        yield return new WaitForSeconds(0.25f);
        CharacterMovement.forward = Camera.main.transform.forward;
        CharacterMovement.forward.y = 0;
        CharacterMovement.right = Quaternion.Euler(new Vector3(0, 90, 0)) * CharacterMovement.forward;
        rotating = false;
    }
}
