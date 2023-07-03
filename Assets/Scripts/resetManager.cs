using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetManager : MonoBehaviour
{
    public InitialPosition[] positions;


    public void ResetAllObjects(){
        for(int i = 0; i < positions.Length; i++){
            positions[i].Reset();
        }
    }
}
