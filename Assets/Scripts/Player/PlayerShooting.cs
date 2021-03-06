﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public Slider ammoSlider;
    public int maxBullets = 100;
    int currentBullets;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    public Image enemyImage;
    public Slider enemyHealthSlider;

    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        currentBullets = maxBullets;

        enemyImage.enabled = false;
        enemyHealthSlider.gameObject.SetActive(false);
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    public void Shoot ()
    {
        if (timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            timer = 0f;
            if (currentBullets <= 0)
            {
                return;
            }
            currentBullets--;
            ammoSlider.value = currentBullets;

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {

                    enemyHealth.TakeDamage(damagePerShot, shootHit.point);

                    enemyImage.enabled = true;
                    enemyImage.sprite = enemyHealth.icon;

                    enemyHealthSlider.gameObject.SetActive(true);
                    enemyHealthSlider.maxValue = enemyHealth.startingHealth;
                    enemyHealthSlider.value = enemyHealth.currentHealth;

                }
                else
                {
                    enemyImage.enabled = false;
                    enemyHealthSlider.gameObject.SetActive(false);
                }
                gunLine.SetPosition(1, shootHit.point);

            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
    }
}
