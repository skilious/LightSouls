using UnityEngine;

public class TeleporterCheck : RayToGround
{
    //private static TeleporterCheck instance;
    //public static TeleporterCheck GetInstance()
    //{
    //    return instance;
    //}

    // Event Shoutouts
    //public event System.EventHandler OnTeleporter;
    //public event System.EventHandler OnGround;
    
    public LayerMask teleporterLayer;
    public LayerMask groundLayer;

    //[SerializeField]
    //protected bool playerOnTeleporter = false;

    // Serialized for testing purposes. To see them change in the editor. Removed [SerializeField]
    protected MeshCollider meshCollider_OnTeleporter;
    protected MeshCollider meshCollider_OffTeleporter;
    protected Animator animator;

    private void Awake()
    {
        // instance = this;
        rayDirection = -transform.up;
    }
    private void Update()
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

                    // playerOnTeleporter = true;
                    // Debug.Log(" if (raycastHit.collider.GetComponent<PopUp_TC>()) - bool playerOnTeleporter = " + playerOnTeleporter);

                    // Do Teleporter Stuff
                    // OnTeleporter.Invoke(this, System.EventArgs.Empty);

                    animator.SetBool("onTeleporter", true);
                    // Text Popup: "Press X to Enter Stage"

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Debug.Log(" SceneToLoad = " + raycastHit.collider.GetComponentInParent<PopUp_TC>().GetScene());
                        if(raycastHit.collider.GetComponentInParent<PopUp_TC>().GetStageStatus())
                        {
                            print("Its completed already!");
                        }
                        else if(raycastHit.collider.GetComponentInParent<PopUp_TC>().stageNumber < PopUp_TC.completedStages + 2)
                        {
                            Loader.Load(raycastHit.collider.GetComponentInParent<PopUp_TC>().GetScene());
                        }
                        else
                        {
                            print("you don't have access yet!");
                        }

                    }
                }
                else if (!raycastHit.collider.isTrigger)
                {
                    meshCollider_OffTeleporter = raycastHit.collider.GetComponent<MeshCollider>();
                    animator = meshCollider_OffTeleporter.GetComponentInParent<Animator>();

                    // playerOnTeleporter = false;
                    // Debug.Log(" if (!raycastHit.collider.isTrigger) - bool playerOnTeleporter = " + playerOnTeleporter);
                    
                    Debug.Log(" meshCollider_OffTeleporter = " + meshCollider_OffTeleporter);
                    Debug.Log(" animator = " + animator);
                    animator.SetBool("onTeleporter", false);
                    // OnGround.Invoke(this, System.EventArgs.Empty);
                }
            }
        }
    }
}
