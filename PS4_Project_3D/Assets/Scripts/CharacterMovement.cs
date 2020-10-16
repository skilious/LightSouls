using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    enum DashState
    {
        Cooldown,
        Ready,
        Dashing
    };

    private DashState dashState;

    public float speed;
    public float maxTimer;
    public float timer = 0;

    [SerializeField]
    private float moveSpd;

    public static Rigidbody rb;
    private bool dashing = false;

    public static Vector3 forward, right;

    private void Awake()
    {
        timer = maxTimer;
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    private void FixedUpdate()
    {

        //If its not dashing, you can use WASD/Arrow keys to move your character instead.

        if (Input.anyKey)
            Movement();

    }

    private void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                {
                    bool isKeyDown = Input.GetKeyDown(KeyCode.Space);
                    if (isKeyDown)
                    {
                        gameObject.tag = "Temp";
                        dashing = true;
                        dashState = DashState.Dashing;
                    }
                    break;
                }
            case DashState.Dashing:
                {
                    timer += Time.deltaTime * 3;
                    float smoothness = Mathf.Lerp(0.0f, 5.0f, Time.deltaTime * speed);
                    rb.AddForce(transform.forward * smoothness, ForceMode.VelocityChange);
                    if (timer >= maxTimer)
                    {
                        dashing = false;
                        timer = maxTimer;
                        dashState = DashState.Cooldown;
                        rb.velocity = Vector3.zero;
                    }
                    break;
                }
            case DashState.Cooldown:
                {
                    timer -= Time.deltaTime;
                    gameObject.tag = "Player";
                    print("No longer dashing!");
                    if (timer <= 0)
                    {
                        timer = 0;
                        dashState = DashState.Ready;
                    }
                    break;
                }
        }
    }

    void Movement()
    {
        Vector3 rightMovement = right * moveSpd * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpd * Time.deltaTime * Input.GetAxis("Vertical");

        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
