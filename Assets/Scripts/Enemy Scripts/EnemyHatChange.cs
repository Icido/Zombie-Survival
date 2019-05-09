using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHatChange : MonoBehaviour {

    MeshRenderer zombieHatMR;
    Material[] zombieHatMaterials;

    Material attackingMaterial;
    Material chasingMaterial;
    Material idleMaterial;


    private void Awake()
    {
        zombieHatMR = transform.GetChild(0).GetComponent<MeshRenderer>();
        zombieHatMaterials = zombieHatMR.materials;
        attackingMaterial = zombieHatMaterials[0];
        chasingMaterial = zombieHatMaterials[1];
        idleMaterial = zombieHatMaterials[2];
        zombieHatMR.material = idleMaterial;
        
    }

    public void changeHat(int state)
    {
        switch(state)
        {
            case 0:
                zombieHatMR.material = chasingMaterial;
                break;

            case 1:
                zombieHatMR.material = attackingMaterial;
                break;

            case 2:
                zombieHatMR.material = idleMaterial;
                break;

            default:
                zombieHatMR.material = idleMaterial;
                break;
        }
    }
}
