using ReDesign;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AddSnowToObjects : MonoBehaviour
{
    [SerializeField] private Material snowMaterial;
    [SerializeField] private GameObject snowTerrain;
    [SerializeField] private GameObject snowParticles;
    private List<GameObject> snowObjects;

    private void Start()
    {
        snowObjects = GameObject.FindGameObjectsWithTag("AddSnow").ToList();
    }

    public void AddSnow()
    {
        snowTerrain.SetActive(true);
        foreach (GameObject obj in snowObjects)
        {
            List<Material> materials = new List<Material>();
            materials.AddRange(obj.GetComponent<Renderer>().materials);
            materials.Add(snowMaterial);
            obj.GetComponent<Renderer>().materials = materials.ToArray();
        }
    }

    public void StartSnowing()
    {
        snowParticles.SetActive(true);
        snowParticles.GetComponent<ParticleSystem>().Play();
    }
}
