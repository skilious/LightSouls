using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed = 1.0f;

    public void Move(Vector3 movement)
    {
        transform.position += movement * Time.deltaTime * moveSpeed;
    }
}


// Author: Tarek Tabet