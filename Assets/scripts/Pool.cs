using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject go;

        if (inactiveObjects.Count > 0)
        {
            go = inactiveObjects.Dequeue();
            ActivateObject(go, position, rotation);
            isNew = false;
        }
        else
        {
            go = Instantiate(prefab, position, rotation, transform);
            ActivateObject(go, position, rotation);
            isNew = true;
        }

        return go;
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
        inactiveObjects.Enqueue(go);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

    }

    private void ActivateObject(GameObject go, Vector3 position, Quaternion rotation)
    {
        go.SetActive(true);
        go.transform.position = position;
        go.transform.rotation = rotation;
        //activeObjects.Enqueue(go);
    }

   // private List<GameObject> activeObjects = new List<GameObject>();
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();

    private bool isNew;
    public bool IsNew
    {
        get { return isNew; }
        set { isNew = value; }
    }

}