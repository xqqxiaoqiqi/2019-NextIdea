﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementParticle : MonoBehaviour
{
    private static string particlepath = "Texiao/Prefabs/";
    private string particlename = "default";
    private GameObject myparticle;
    public void UpdateParticleType(string name)
    {
        if(!name.Equals(particlename))
        {
            particlename = name;
            RemoveParticlePrefab();
            AddParticlePrefab();
        }
    }
    public void PlayParticle()
    {
        myparticle.SetActive(true);
    }
    public void StopParticle()
    {
        myparticle.SetActive(false);
    }
    private void AddParticlePrefab()
    {
        myparticle = (GameObject)Instantiate(Resources.Load(particlepath + particlename, typeof(GameObject)));
        myparticle.transform.SetParent(gameObject.transform);
        myparticle.transform.localPosition = Vector3.zero;
    }
    private void RemoveParticlePrefab()
    {
        GameObject.Destroy(myparticle);
    }
}
