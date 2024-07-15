using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    // Bullet
    public GameObject bullet;

    // Bullet Force
    public float shootForce, upwardForce;

    // Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsRemaining, bulletsFired;

    // Bools
    bool isFiring, readyToFire, reloading;

    // References
    public Camera fpsCam;
    public Transform attackPoint;

    // Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    public bool allowInvoke = true;

    private void Awake()
    {
        // Fill magazine
        bulletsRemaining = magazineSize;
        readyToFire = true;
    }

    private void Update()
    {
        PlayerInput();

        // Set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsRemaining / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }

    private void PlayerInput()
    {
        // Check if allowed to hold down bvutton and take corresponding input
        if (allowButtonHold) isFiring = Input.GetKey(KeyCode.Mouse0);
        else isFiring = Input.GetKeyDown(KeyCode.Mouse0);

        // Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsRemaining < magazineSize && !reloading)
            Reload();

        // Reload automatically when trying to shoot without ammo
        if (readyToFire && isFiring && !reloading && bulletsRemaining <= 0)
            Reload();

        // Firing
        if (readyToFire && isFiring && !reloading && bulletsRemaining > 0)
        {
            // Set bullets fired to 0
            bulletsFired = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToFire = false;

        // Find exact hit position
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        // Calculate direction
        Vector3 directionNoSpread = targetPoint - attackPoint.position;

        // Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate new direction with spread
        Vector3 directionWithSpread = directionNoSpread + new Vector3(x, y, 0);

        // Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        // Add forces to bullet 
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        // Instantiate muzzle flash, if available
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);


        bulletsRemaining--;
        bulletsFired++;

        // Invoke resetShot function
        if (allowInvoke)
        { 
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        // If more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsFired < bulletsPerTap && bulletsRemaining > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        // Allow shooting and invoking again
        readyToFire = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsRemaining = magazineSize;
        reloading = false;
    }

}
