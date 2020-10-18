using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    /*Instructions:
     * Use the function SharedInstance to other scripts that involves with the instantiated objects.
     * An example would be: GameObject obj = Object_Pooling.SharedInstance.GetPooledObject("tag_name"); //This would return the inactive GameObject that exists in the hierarchy.
     * Then use obj.SetActive(true), obj.transform.position = Whatever Vector3 you set it to (transform.position) and obj.transform.rotation = transform.rotation.
     * Look at the ones that i have done for other projectiles previously as reference.
    */
    //Other scripts have access to the GameObject's script with variables and functions.
    public static Object_Pooling SharedInstance;

    public GameObject[] objects;

    [Range(0, 1000)]
    public int[] amountToInstantiate;

    public List<GameObject> objectPooled;

    public int objCounter = 1;
    void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        while(objCounter != objects.Length)
        {
            for (int i = 0; i < amountToInstantiate[objCounter]; i++)
            {
                GameObject obj = Instantiate(objects[objCounter]);
                obj.SetActive(false);
                objectPooled.Add(obj);

            }
            objCounter++;
        }
    }
    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < objectPooled.Count; i++)
        {
            if(!objectPooled[i].activeInHierarchy && objectPooled[i].tag == tag)
            {
                return objectPooled[i];
            }
        }
        return null;
    }
}
