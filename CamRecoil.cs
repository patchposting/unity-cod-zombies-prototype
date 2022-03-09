using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    public float rotationSpeed = 6;
    public float returnSpeed = 25;

    public Vector3 recoilRotationAim = new Vector3(.5f, .5f, 1.5f);
    public Vector3 recoilRotationHip = new Vector3(2, 2, 2);

    public bool aiming;

    public Vector3 currentRotation;
    public Vector3 rot;

    void Start()
    {
        //aiming = false;
    }

    private void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRotation, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(rot);
    }

    public void Fire() 
    {
        if (aiming)
        {
            currentRotation += new Vector3(-recoilRotationAim.x, Random.Range(-recoilRotationAim.y, recoilRotationAim.y), Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
        }
        else 
        {
            Debug.Log("recoil");
            currentRotation += new Vector3(-recoilRotationHip.x, Random.Range(-recoilRotationHip.y, recoilRotationHip.y), Random.Range(-recoilRotationHip.z, recoilRotationHip.z));
        }
    }
}
