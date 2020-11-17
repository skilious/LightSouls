using UnityEngine;

public class TeleporterCheck : RayToGround
{
    private static TeleporterCheck instance;
    public static TeleporterCheck GetInstance()
    {
        return instance;
    }

    // Event Shoutouts
    public event System.EventHandler OnTeleporter;
    public event System.EventHandler OnGround;
    
    public LayerMask teleporterLayer;
    public LayerMask groundLayer;

    [SerializeField]
    protected bool playerOnTeleporter = false;

    // Serialized for testing purposes - to see them change in the editor
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
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, layerMask))
        {
            if (raycastHit.collider.GetComponentInParent<PopUp_TC>())
            {
                if (raycastHit.collider.isTrigger)
                {
                    meshCollider_OnTeleporter = raycastHit.collider.GetComponent<MeshCollider>();
                    animator = meshCollider_OnTeleporter.GetComponentInParent<Animator>();
                    playerOnTeleporter = true;
                    // animator.SetBool("onTeleporter" , true);
                    Debug.Log(" if (raycastHit.collider.GetComponent<PopUp_TC>()) - bool playerOnTeleporter = " + playerOnTeleporter);

                    // Do Teleporter Stuff
                    // OnTeleporter.Invoke(this, System.EventArgs.Empty);

                    animator.SetBool("onTeleporter", true);
                    // Text Popup: "Press X to Enter Stage"

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Debug.Log(" SceneToLoad = " + raycastHit.collider.GetComponentInParent<PopUp_TC>().GetScene());
                        Loader.Load(raycastHit.collider.GetComponentInParent<PopUp_TC>().GetScene());
                    }
                }
                else if (!raycastHit.collider.isTrigger)
                {
                    meshCollider_OffTeleporter = raycastHit.collider.GetComponent<MeshCollider>();
                    animator = meshCollider_OffTeleporter.GetComponentInParent<Animator>();
                    playerOnTeleporter = false;
                    // animator.SetBool("onTeleporter", false);
                    Debug.Log(" if (!raycastHit.collider.isTrigger) - bool playerOnTeleporter = " + playerOnTeleporter);
                    Debug.Log(" meshCollider_OffTeleporter = " + meshCollider_OffTeleporter);
                    Debug.Log(" animator = " + animator);
                    animator.SetBool("onTeleporter", false);
                    // OnGround.Invoke(this, System.EventArgs.Empty);
                }
            }
        }
    }
}
