using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingParticleController : MonoBehaviour {

    public Transform affector;

    private ParticleSystemRenderer psr;

	void Start () {
        psr = GetComponent<ParticleSystemRenderer>();
     
	}
	
	void Update () {
        if(affector == null)
        {
          //  affector = GameObject.FindGameObjectWithTag("affector").transform;
        }
        psr.material.SetVector("_Affector", affector.position);
    }
}
