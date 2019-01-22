using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affectorAdder : MonoBehaviour {

    LivingParticleArrayController lPAC;

	// Use this for initialization
	void Start () {
        lPAC = transform.parent.parent.parent.GetComponentInChildren<LivingParticleArrayController>();
        lPAC.affectors.Add(transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        lPAC.affectors.Remove(transform);
    }

    private void OnDisable()
    {
        lPAC.affectors.Remove(transform);
    }
}
