using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCheck : MonoBehaviour
{
    public List<GameObject> FindObjectsOnLayer(int layer)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> layerObjects = new List<GameObject>();

        foreach (GameObject t in allObjects)
        {
            if (t.layer == layer)
            {
                layerObjects.Add(t);
            }
        }

        allObjects = new GameObject[1];

        return layerObjects;
    }

    public List<GameObject> FindObjectsWithName(string name)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        List<GameObject> nameObjects = new List<GameObject>();

        foreach (GameObject t in allObjects)
        {
            if (t.name == name)
            {
                nameObjects.Add(t);
            }
        }

        allObjects = new GameObject[1];

        return nameObjects;
    }

    public List<GameObject> FindObjectsWithNameWithoutLayer(string name, int layer)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> nameObjects = new List<GameObject>();

        foreach (GameObject t in allObjects)
        {
            if (t.name == name && t.transform.parent.gameObject.layer != layer)
            {
                nameObjects.Add(t);
            }
        }

        allObjects = new GameObject[1];

        return nameObjects;
    }

    public List<GameObject> FindObjectsOnLayerWithTag(int layer, string tag)
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        List<GameObject> nameObjects = new List<GameObject>();

        foreach (GameObject t in allObjects)
        {
            if (t.layer == layer && t.CompareTag(tag))
            {
                nameObjects.Add(t);
            }
        }

        allObjects = new GameObject[1];

        return nameObjects;
    }
}
