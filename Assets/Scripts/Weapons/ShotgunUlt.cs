using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunUlt : MonoBehaviour
{
    [SerializeField] private GameObject shotgunEffect; //Shotgun Effect
    [SerializeField] private float ultDuration; //Ultimate Duration

    private bool pressedOnce; //Check pressed once
    private void Awake()
    {
        shotgunEffect.SetActive(false);
    }
    private void Update()
    {
        bool isUltActive = UltimateManager.instance.getUltStatus();
        //If x is pressed & ult is active & has not been pressed, activate ult
        if (Input.GetKeyDown("x") && isUltActive && !pressedOnce)
        {
            pressedOnce = true;
            activateUlt();
        }
    }
    private void activateUlt()
    {
        shotgunEffect.SetActive(true); //Set shotgun ultimate effect active
        SFXManager.instance.playShotgunUlt(true);
        StartCoroutine(resetKillCoroutine());
    }
    //Reset kill count after ult duration ends
    private IEnumerator resetKillCoroutine()
    {
        shotgunEffect.SetActive(true);
        yield return new WaitForSeconds(ultDuration);
        SFXManager.instance.playShotgunUlt(false);
        shotgunEffect.SetActive(false);
        UltimateManager.instance.resetKillCount();
        pressedOnce = false;
    }
}
