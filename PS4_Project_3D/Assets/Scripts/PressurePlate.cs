using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PressurePlate : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private LayerMask layer;

    private MeshRenderer renderer;

    [SerializeField]
    private Color colour;

    private Color ogColour;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();
        ogColour = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, 0.05f, transform.up, out hit, layer))
        {
            print(hit.transform.name);
            if(hit.transform.name == "Player")
            {
                renderer.material.color = colour;
                anim.SetBool("SteppedOn", true);
            }
        }
        else
        {
            renderer.material.color = ogColour;
            anim.SetBool("SteppedOn", false);
        }
    }
}
