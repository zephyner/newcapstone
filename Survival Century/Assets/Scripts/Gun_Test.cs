using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun_Test : MonoBehaviour
{
    public GameObject bullet;
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;

    //Force of the bullets
    public float shotForce;
    public float upwardForce;

    //The gun stats
    public float timeBetweenShooting;
    public float spread;
    public float reloadTime;
    public float timeBetweenShots;

    public int magazineSize;
    public int bulletPerTap;

    public bool buttonHold;

    public int bulletsLeft;
    public int bulletsShot;

    //The booleans
    bool shooting;
    bool readyToShoot;
    bool reload;

    //The bullet reference points
    public Camera shooterCam;
    public Transform attackPoint;

    //Used for bug fixing
    public bool allowInvoke = true;

    // Start is called before the first frame update
    void Awake()
    {
        //Checks to see if magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();

        //Displays ammo
        if (ammoDisplay != null)
            ammoDisplay.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize / bulletPerTap);
    }

    void MyInput()
    {
        //Checks to see if you can hold the shoot button down
        if (buttonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reload input
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reload) Reload();
        //Reload automaticallt when shooting with no ammo
        if (readyToShoot && shooting && !reload && bulletsLeft <= 0) Reload();

        //Used for shooting
        if (readyToShoot && shooting && !reload && bulletsLeft > 0)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

        //Finds the hit position using the ray
        Ray ray = shooterCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        //Checks if the ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        //The direction from attackPoint, to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculates spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculates new direction with the spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //The bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotates the bullet to the direction you want to shoot in
        currentBullet.transform.forward = directionWithSpread.normalized;

       //gives the bullets force
       currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shotForce, ForceMode.Impulse);
       currentBullet.GetComponent<Rigidbody>().AddForce(shooterCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Resets the shots with the timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletPerTap, make sure to repeat the shoot function
        if (bulletsShot < bulletPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    void ResetShot()
    {
        //Allow for shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    void Reload()
    {
        reload = true;
        Invoke("FinishedReload", reloadTime);
    }

    void FinishedReload()
    {
        bulletsLeft = magazineSize;
        reload = false;
    }
}
