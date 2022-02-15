using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlacer : MonoBehaviour
{
    public GameObject trapPrefab;

    public LayerMask blockLayer;

    private GameObject tempTrap;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TryPlaceTrap();
        }
        if(tempTrap != null)
        {
            ControlTrap();
        }    
    }

    void TryPlaceTrap()
    {
       switch (tempTrap == null)
        {
            case true:
                tempTrap = Instantiate(trapPrefab, new Vector3(transform.position.x, -.6f, transform.position.z) + transform.forward *2f, Quaternion.identity);
                break;
            case false:
                Destroy(tempTrap);
                break;
        }
    }
    void ControlTrap()
    {
        tempTrap.transform.position = GetGridPosition();
        if(Input.GetKeyDown(KeyCode.Return) && CanPlaceTrap())
        {
            tempTrap.GetComponent<Trap>().Place();
            tempTrap = null;
        }
    }

    Vector3 GetGridPosition()
    {
        Vector3 _position = new Vector3(0f, -.6f, 0f);
        if(Physics.Raycast(transform.position + transform.forward * 2f, Vector3.down, out RaycastHit _hit, 5f))
        {
            _position.x = Mathf.Round(_hit.point.x);
            _position.z = Mathf.Round(_hit.point.z);

        }
        return _position;
    }

    bool CanPlaceTrap()
    {
        return !Physics.CheckBox(tempTrap.transform.position, tempTrap.transform.localScale *.499f, tempTrap.transform.rotation, blockLayer);
    }
}
