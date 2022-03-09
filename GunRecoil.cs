using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    [Header("Sway Parameters")]
    [SerializeField] private float smooth;
    [SerializeField] private float multiplier;

    [Header("Recoil Parameters")]
    public Transform recoilPosition;
    public Transform rotationPoint;
    public float positionalRecoilSpeed = 8;
    public float rotationalRecoilSpeed = 8;
    public float positionalReturnSpeed = 18;
    public float rotationalReturnSpeed = 38;
    public Vector3 recoilRotationHip = new Vector3(10, 5, 7);
    public Vector3 recoilKickBackHip = new Vector3(.015f, 0f, -.2f);
    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 rot;

    [Header("GunBob Parameters")]
    [SerializeField] private float gunBobWalkSpeed;
    [SerializeField] private float gunBobWalkAmount;
    [SerializeField] private float gunBobSprintSpeed;
    [SerializeField] private float gunBobSprintAmount;
    [SerializeField] private float gunBobCrouchSpeed;
    [SerializeField] private float gunBobCrouchAmount;
    public GameObject gun;
    private float horizontal, vertical;  
    //private float defaultXPos = 0;
    private float defaultYPos = 0;
    private float defaultZPos = 0;
    private float timer;

    //misc
    public FirstPersonControllerCU fpScript;
    public bool aiming;

    void Start()
    {
        
    }

    private void Update()
    {
        //recoilPosition.transform.localPosition = GameObject.FindGameObjectWithTag("Gun").transform.localPosition;
        //rotationPoint.transform.localPosition = GameObject.FindGameObjectWithTag("Gun").transform.localPosition;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 localPosition = gun.transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else 
        {
            GunBob();
        }
    }

    private void FixedUpdate()
    {
        //SWAY
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * multiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * multiplier;

        // calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        // rotate 
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);



        //RECOIL
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);
    }

    public void Recoil() 
    {
        rotationalRecoil += new Vector3(-recoilRotationHip.x, Random.Range(-recoilRotationHip.y, recoilRotationHip.y), Random.Range(-recoilRotationHip.z, recoilRotationHip.z));
        positionalRecoil += new Vector3(Random.Range(-recoilKickBackHip.x, recoilKickBackHip.x), Random.Range(-recoilKickBackHip.y, recoilKickBackHip.y), recoilKickBackHip.z);
    }

    private void GunBob()
    {
        timer += Time.deltaTime * (fpScript.isCrouching ? gunBobCrouchSpeed : fpScript.IsSprinting ? gunBobSprintSpeed : gunBobWalkSpeed);
        gun.transform.localPosition = new Vector3(
            gun.transform.localPosition.x,
            //defaultXPos + Mathf.Sin(timer) * (fpScript.isCrouching ? gunBobCrouchAmount : fpScript.IsSprinting ? gunBobSprintAmount : gunBobWalkAmount),
            //gun.transform.localPosition.y,
            defaultYPos + Mathf.Sin(timer) * (fpScript.isCrouching ? gunBobCrouchAmount/2 : fpScript.IsSprinting ? gunBobSprintAmount/2 : gunBobWalkAmount/2),
            //gun.transform.localPosition.z);
            defaultZPos + Mathf.Sin(timer) * (fpScript.isCrouching ? gunBobCrouchAmount : fpScript.IsSprinting ? gunBobSprintAmount : gunBobWalkAmount));
    }
}
