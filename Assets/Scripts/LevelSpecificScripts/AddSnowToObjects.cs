using ReDesign;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddSnowToObjects : MonoBehaviour
{
    [SerializeField] private Material snowMaterial;
    private List<GameObject> snowObjects;

    private void Start()
    {
        snowObjects = GameObject.FindGameObjectsWithTag("AddSnow").ToList();
    }

    public void AddSnow()
    {   
        foreach (GameObject obj in snowObjects)
        {
            List<Material> materials = new List<Material>();
            materials.AddRange(obj.GetComponent<Renderer>().materials);
            materials.Add(snowMaterial);
            obj.GetComponent<Renderer>().materials = materials.ToArray();
        }
    }
}
