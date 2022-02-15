using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public bool isPlaced = false;
    

    public void Place()
    {
        gameObject.layer = LayerMask.NameToLayer("obstacle");
    }
}
