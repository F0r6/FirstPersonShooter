using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPB;
    public Transform firePoint;
    public float fireRate;

    private bool isCooldown = false;

    [Header("Keybinds")]
    public KeyCode FireButton = KeyCode.Mouse0;

    void Update()
    {
        if (Input.GetKey(FireButton) && !isCooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPB, firePoint.position, firePoint.rotation);
        StartCoroutine(Cooldown());

        //RaycastHit hit;
        //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        //{
        //    Debug.Log(hit.transform.name);
        //
        //    Target target = hit.transform.GetComponent<Target>();
        //    if (target != null)
        //    {
        //        target.TakeDamage(damage);
        //    }
        //}
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(fireRate);
        isCooldown = false;
    }
}
