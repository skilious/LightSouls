using UnityEngine;

public class TeleporterCheck : RayToGround
{
    private static TeleporterCheck instance;
    public static TeleporterCheck GetInstance()
    {
        return instance;
    }

    // Event Shoutouts
    //public event System.EventHandler OnTeleporter;
    //public event System.EventHandler OnGround;
    
    public LayerMask teleporterLayer;
    public LayerMask groundLayer;

    [SerializeField]
    protected bool playerOnTeleporter = false;

    [SerializeField]
    protected MeshCollider meshCollider_OnTeleporter;
    [SerializeField]
    protected MeshCollider meshCollider_OffTeleporter;

    [SerializeField]
    protected Animator animator;

    private void Awake()
    {
        instance = this;
        rayDirection = -transform.up;
    }
    private void FixedUpdate()
    {
        rayOrigin = transform.position;
        ray = new Ray(rayOrigin, rayDirection * distance);
        RaycastDown(teleporterLayer);
        //RaycastDown(groundLayer);
    }

    protected void RaycastDown(LayerMask layerMask)
    {
        // base.RaycastDown(layerMask);

        // Dray ray for debugging
        Debug.DrawRay(rayOrigin, rayDirection * distance, Color.red);

        Debug.Log(playerOnTeleporter);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, layerMask))
        {
            if (raycastHit.collider.gameObject.GetComponentInChildren<PopUp_TC>())
            {
                playerOnTeleporter = true;
                // animator.SetBool("onTeleporter" , true);
                Debug.Log("ON Teleporter = " + playerOnTeleporter);

                // Do Teleporter Stuff
                Debug.Log("Do THings for ON tele text");
                OnTeleporterStuff();
                // OnTeleporter.Invoke(this, System.EventArgs.Empty);

                // Text Popup: "Press X to Enter Stage"
            }
            else if (!raycastHit.collider.gameObject.GetComponentInChildren<PopUp_TC>())
            {
                playerOnTeleporter = false;
                // animator.SetBool("onTeleporter", false);
                Debug.Log("OFF Teleporter" + playerOnTeleporter);

                OffTeleporterStuff();
                //OnGround.Invoke(this, System.EventArgs.Empty);
            }
        }
    }

    protected void OnTeleporterStuff()
    {
        animator.SetBool("onTeleporter", true);
        if (Input.GetKeyDown(KeyCode.X))
        {
            print("Stage 1 loading");
            Loader.Load(Loader.Scene.Level_Skull);
        }
    }
    protected void OffTeleporterStuff()
    {
        animator.SetBool("onTeleporter", false);
    }
}
