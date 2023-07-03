using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneLight : MonoBehaviour
{
    public Material baseMat;
    public Material emissiveMat;
    public MeshRenderer phoneMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            phoneMesh.material = emissiveMat;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            phoneMesh.material = baseMat;
        }
    }
}
