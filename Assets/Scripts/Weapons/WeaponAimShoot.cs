using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimShoot : MonoBehaviour
{
    // Unity 2D Aim and Shoot at mouse position Tutorial https://www.youtube.com/watch?v=-bkmPm_Besk
    // How to make a gun in unity! https://www.youtube.com/watch?v=w6QM9XU1m_o
    
    [SerializeField] private GameObject bullet;
    // control time between firing, no. of bullets, bullet spread (for shotgun), bullet speed
    [SerializeField] private float timeBetweenFiring, numberOfBullets, bulletSpread, bulletSpeed, bulletTime;
    // where the bullet comes out from
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private AudioClip shootSound;

    private Camera mainCam;
    private float timer;

    [HideInInspector] public bool canFire;
    // // Start is called before the first frame update
    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(!canFire)
        {
            timer += Time.deltaTime;
            if(timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(Input.GetMouseButton(0) && canFire)
        {
            SFXManager.instance.playShootSound(shootSound);
            canFire = false;
            Shoot();
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < numberOfBullets; i++) 
        {
            GameObject b = Instantiate(bullet, ShootPoint.position, ShootPoint.rotation);
            Rigidbody2D brb = b.GetComponent<Rigidbody2D>();
            Vector2 dir = transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-bulletSpread, bulletSpread);
            // brb.velocity = (dir + pdir) * bulletSpeed;
            brb.velocity = (dir + pdir) * bulletSpeed;
            Destroy(b, bulletTime);
        }
    }
}
