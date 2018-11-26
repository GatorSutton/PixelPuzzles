using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingParticleArrayController : MonoBehaviour {

    //public Transform[] affectors;
    public List<Transform> affectors;

    private Vector4[] positions;
    private ParticleSystemRenderer psr;

	void Start () {
        psr = GetComponent<ParticleSystemRenderer>();
        Vector4[] maxArray = new Vector4[20];
        psr.material.SetVectorArray("_Affectors", maxArray);
    }

    // Sending an array of positions to particle shader
    void Update () {
        // positions = new Vector4[affectors.Length];
        positions = new Vector4[affectors.Count];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = affectors[i].position;
        }
        if (affectors.Count > 0)
        {
            psr.material.SetVectorArray("_Affectors", positions);
            // psr.material.SetInt("_AffectorCount", affectors.Length);
            psr.material.SetInt("_AffectorCount", affectors.Count);
        }



    }

}
