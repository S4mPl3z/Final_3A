using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    //Armas y Trampas
    public GameObject trapPrefab;

    public GameObject Arma;

    public LayerMask blockLayer;

    private GameObject tempTrap;

    //Bools
    public bool NormalShot;
    public bool BallShot;
    public bool CanUseTrap = true;
    public bool CanUseWeapon = true;
    // Particulas
    public ParticleSystem Disparo;
    public GameObject Disparo_Particle;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && CanUseTrap == true)
        {
            CanUseWeapon = false;
            TryPlaceTrap();
        }
        if(tempTrap != null)
        {
            ControlTrap();
        }    

        if(Input.GetKeyDown(KeyCode.Alpha1) && CanUseWeapon == true)
        {
            CanUseTrap = false;
            StartCoroutine(ShotNormalShot());
        }
    }

    IEnumerator ShotNormalShot()
    {
        Disparo_Particle.SetActive(true);
        Disparo.Play();
        CanUseWeapon = false;
        yield return new WaitForSeconds(0.5f);
        CanUseWeapon = true;
        CanUseTrap = true;
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
            CanUseTrap = true;
            CanUseWeapon = true;
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
