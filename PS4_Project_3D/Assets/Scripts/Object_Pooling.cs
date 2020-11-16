using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tai's script.
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

    //This function only starts at the beginning w/ for looping until it instantiates all the necessary GameObjects that are needed in the scene.
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        while(objCounter != objects.Length)
        {
            for (int i = 0; i < amountToInstantiate[objCounter]; i++)
            {
                GameObject obj = Instantiate(objects[objCounter]);
                obj.SetActive(false);
                objectPooled.Add(obj);
                DontDestroyOnLoad(obj);
            }
            objCounter++;
        }
    }
    //Can be used to reference a GameObject w/ Tag.
    public GameObject GetPooledObject(string tag)
    {
        //Grabs every objectpooled GameObject.
        for (int i = 0; i < objectPooled.Count; i++)
        {
            //Until it relates to whatever tag you selected and its inactive in the inhierarchy.
            if(!objectPooled[i].activeInHierarchy && objectPooled[i].tag == tag)
            {
                return objectPooled[i]; //Returns and do whatever you want with it.
            }
        }
        return null; //Otherwise, it doesnt exist or all is being used up at the same time. Throws a null error.
    }
}
