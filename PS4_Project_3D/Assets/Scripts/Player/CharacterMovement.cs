using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's code 100%
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
    private float moveSpd = 0;

    public float hoverForce, hoverHeight;
    public static Rigidbody rb;

    protected bool dashing = false;

    public static Vector3 forward, right;
    private ParticleSystem dashParticle;

    private void Awake()
    {
        dashParticle = GameObject.Find("Particle_Effect").GetComponent<ParticleSystem>(); //Hoping that this works!
        timer = maxTimer; //Making sure that timer works normally by setting timer to maxTimer's value.
        rb = GetComponent<Rigidbody>(); //Getting this GameObject's Rigidbody.
        forward = Camera.main.transform.forward; //Grabs transform.forward from main camera.
        forward.y = 0; //Y axis is useless from here on.
        forward = Vector3.Normalize(forward); //Set it between -1 or 1.
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //right has Quaternion Euler! It uses only y axis but then multiplying forward which then
        //fills it into both X and Z axis.
    }

    private void FixedUpdate()
    {
        //If its not dashing, you can use WASD/Arrow keys to move your character instead. PC ONLY
        //Otherwise, left analog stick on your ps4 controller.
        Movement();

        if (!Input.anyKey)
        {
            rb.velocity = Vector3.zero;
        }
        //This creates a hover effect.
        RaycastHit hit;
        //Ray uses transform.position and direction negative transform.up to detect floor.
        Ray ray = new Ray(transform.position, -transform.up);
        //2.0f is the height between the ray(floor) and the player's Y axis.
        if (Physics.Raycast(ray, out hit, hoverHeight))
        {
            float height = (hoverHeight - hit.distance) / hoverHeight;
            //This uses Vector3.up (1.0f) * height * hoverForce.
            Vector3 appliedHoverForce = Vector3.up * height * hoverForce;
            //AddForce rigidbody with "Acceleration" to continuously acceleration while ignoring its mass.
            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        //Switch statement for Dashing
        switch (dashState)
        {
            case DashState.Ready:
                {
                    //Sets bool depending on user input (Spacebar).
                    bool isKeyDown = Input.GetButtonDown("Dash");
                    if (isKeyDown)
                    {
                        //Changes to temp to create an invincibility moment.
                        gameObject.tag = "Temp";
                        //Sets the bool to true which disables this whole if statement afterwards.
                        dashing = true;
                        //Switches state to Dashing.
                        dashState = DashState.Dashing;
                        //Plays some shitty particle effect.
                        dashParticle.Play();
                    }
                    break;
                }
            case DashState.Dashing:
                {
                    //Timer goes 3 times faster than usual.
                    timer += Time.deltaTime * 3;
                    //Lerp creates a smooth transition between 0 to 3 based on Time.deltaTime multiplying by speed.
                    float smoothness = Mathf.Lerp(0.0f, 3.0f, Time.deltaTime * speed);
                    //Rigidbody addforce wherever the player is facing while using smoothness to make sure it speeds up.
                    rb.AddForce(transform.forward * smoothness, ForceMode.VelocityChange);
                    //If it exceeds over the maxTimer or equal to, stop the whole process and go away from above.
                    if (timer >= maxTimer)
                    {
                        //Stahp the particle loop!
                        dashParticle.Stop();
                        //Dash no longer happening
                        dashing = false;
                        // Timer is set to maxTimer value.
                        timer = maxTimer;
                        //Cooling down mode!
                        dashState = DashState.Cooldown;
                        //So you don't slide across the world.
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }
                    break;
                }
            case DashState.Cooldown:
                {
                    //When timer goes back to 0, you can now dash again.
                    timer -= Time.deltaTime;
                    //Sets your back to Player to prevent you staying invincible.
                    gameObject.tag = "Player";
                    if (timer <= 0)
                    {
                        timer = 0;
                        //Dash is now ready again..
                        dashState = DashState.Ready;
                    }
                    break;
                }
        }
    }

    void Movement()
    {
        //rightMovement - right comes from eulerAngles on 90 degrees Y axis multiplying from forward (forward is only using X and Z axis) as well and moveSpd w/ Time.deltaTime * Input.
        Vector3 rightMovement = right * moveSpd * Time.deltaTime * Input.GetAxis("Horizontal");
        //upMovement - Relative to camera's forward w/ Y axis remaining 0. Then, you normalize it between -1 and 1.
        Vector3 upMovement = forward * moveSpd * Time.deltaTime * Input.GetAxis("Vertical");
        //print("Input Hori: " + Input.GetAxis("Horizontal_PS4")); //Both of these print func checks the input axis if its being recognised.
        //print("Input Vert: " + Input.GetAxis("Vertical"));
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
