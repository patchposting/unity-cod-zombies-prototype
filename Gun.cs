using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("General Stats")]
    [SerializeField] float damage = 10f;
    [SerializeField] int range = 50;
    [SerializeField] public int maxAmmo = 25;
    [SerializeField] public int currentAmmo;

    [Header("Rapid Fire")]
    [SerializeField] bool rapidFire = false;
    [SerializeField] float fireDelay;
    private float firingTimer;

    [Header("Shotgun")]
    [SerializeField] bool shotgun = false;
    [SerializeField] int bulletsPerShot = 6;
    [SerializeField] float spreadDistance = 5;

    [Header("Misc")]
    public TextMeshProUGUI gunNameUI;
    public TextMeshProUGUI ammoUI;
    public Points pointsScript;
    private CamRecoil camRecoilScript;
    private GunRecoil gunRecoilScript;
    //private WeaponRecoil weaponRecoilScript;
    public FirstPersonControllerCU fpScript;
    public AudioSource gunShotSFX;
    public AudioClip gun0Clip;
    public bool currentlyActiveGun;
    public GameObject hitMarker;
    public GameObject killMarker;

    private Camera mainCamera;
    private int layerMaskZombie;
    private int layerMaskZombieHead;
    private int layerMaskZombieTorso;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        camRecoilScript = GameObject.FindWithTag("CameraRecoil").GetComponent<CamRecoil>();
        gunRecoilScript = GameObject.FindWithTag("WeaponHolder").GetComponent<GunRecoil>();
        //weaponRecoilScript = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponRecoil>();
        gunRecoilScript.gun = this.gameObject;

        layerMaskZombie = LayerMask.GetMask("Zombie");
        layerMaskZombieHead = LayerMask.GetMask("ZombieHead");
        layerMaskZombieTorso = LayerMask.GetMask("ZombieTorso");

        currentAmmo = maxAmmo;
        firingTimer = fireDelay;

        currentlyActiveGun = true;
        gunRecoilScript.recoilPosition = this.gameObject.transform;
        gunRecoilScript.rotationPoint = this.gameObject.transform;
        hitMarker.SetActive(false);
        killMarker.SetActive(false);
    }

    void Update()
    {
        if (this.gameObject.activeSelf == false)
        {
            currentlyActiveGun = false;
        }
        else 
        {
            currentlyActiveGun = true;

            gunRecoilScript.gun = this.gameObject;
            gunRecoilScript.recoilPosition = this.gameObject.transform;
            gunRecoilScript.rotationPoint = this.gameObject.transform;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        else if (Input.GetMouseButton(0) && rapidFire) 
        {
            firingTimer -= Time.deltaTime;
            if (firingTimer < 0) 
            {
                Shoot();
                firingTimer += fireDelay;
            }
        }

        gunNameUI.text = this.gameObject.name;
        ammoUI.text = currentAmmo.ToString("00/") + maxAmmo;
    }

    void Shoot() 
    {
        if (currentAmmo > 0) 
        {
            currentAmmo--;
            gunShotSFX.clip = gun0Clip;
            gunShotSFX.Play();
            gunRecoilScript.Recoil();
            camRecoilScript.Fire();

            RaycastHit hit;
            if (shotgun)
            {
                for (int i = 0; i < bulletsPerShot; i++)
                {

                }
            }
            else 
            {
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range, layerMaskZombieHead))
                {
                    Debug.Log("Shot" + hit.transform.name);
                    Zombie target = hit.transform.GetComponent<Zombie>();
                    HitMarkerActive();

                    if (target != null)
                    {
                        target.TakeDamageHead(damage * 1.75f);
                        pointsScript.points += 10;
                    }
                }
                else if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range, layerMaskZombieTorso))
                {
                    Debug.Log("Shot" + hit.transform.name);
                    Zombie target = hit.transform.GetComponent<Zombie>();
                    HitMarkerActive();

                    if (target != null)
                    {
                        target.TakeDamageTorso(damage);
                        pointsScript.points += 10;
                    }
                }
            }
        }
    }

    public void HitMarkerActive()
    {
        hitMarker.SetActive(true);
        Invoke("HitMarkerDisable", .05f);
    }

    private void HitMarkerDisable()
    {
        hitMarker.SetActive(false);
    }

    public void KillMarkerActive() 
    {
        killMarker.SetActive(true);
        Invoke("KillMarkerDisable", .05f);
    }

    private void KillMarkerDisable() 
    {
        killMarker.SetActive(false);
    }

    Vector3 GetShootingDirection() 
    {
        var camPos = mainCamera.transform;
        Vector3 targetPos = camPos.position + camPos.forward * range;
        targetPos = new Vector3(
            targetPos.x + Random.Range(-spreadDistance, spreadDistance),
            targetPos.y + Random.Range(-spreadDistance, spreadDistance),
            targetPos.z + Random.Range(-spreadDistance, spreadDistance));

        Vector3 direction = targetPos - camPos.position;
        return direction.normalized;
    }
}
