using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public float startingHealth = 100f;
    public float currentHealth;

    private Slider healthSlider;
    private Text healthText;

    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;

        healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        healthText = GameObject.Find("Health Text").GetComponent<Text>();

        healthSlider.maxValue = currentHealth;
        healthSlider.value = healthSlider.maxValue;
        healthText.text = healthSlider.value.ToString();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthSlider.value -= amount;
        healthText.text = healthSlider.value.ToString();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }
}
