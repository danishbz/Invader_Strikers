using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    [SerializeField] private float maxHealth, secToWait;
    [SerializeField] private Slider healthSlider;

    private float currentHealth;
    private GameObject player;

    // New variables for immunity system
    private float immunityDuration = 5f; // Duration of immunity after colliding with a power-up
    private bool isImmune = false;
    private PlayerController playerController;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void ApplyDamage(float damageToTake)
    {
        // Check if the player is immune before applying damage
        if (!isImmune)
        {
            Debug.Log("Damage taken");
            currentHealth -= damageToTake;

            if (currentHealth <= 0)
            {
                Destroy(player);
                StartCoroutine(EndScreenCoroutine());
                player.SetActive(false);
            }

            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.Log("Player is immune; no damage taken");
        }
    }

    private IEnumerator EndScreenCoroutine()
    {
        Debug.Log("Started Coroutine");
        yield return new WaitForSeconds(secToWait);
        GameManager.instance.GameOver();
    }

    // Method to activate immunity
    public void ActivateImmunity()
    {
        isImmune = true;
        Invoke("DeactivateImmunity", immunityDuration);
    }

    // Method to deactivate immunity
    private void DeactivateImmunity()
    {
        isImmune = false;
    }
}
