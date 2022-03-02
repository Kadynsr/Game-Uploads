using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    //Built In
    void Awake() {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    void OnTriggerEnter2D(Collider2D collision) {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if(damageDealer is not null) {
            TakeDamage(damageDealer.GetDamage());
            PlayhitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }
    //Getter
    public int GetHealth() => health;

    //Methods
    void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }
    void Die() {
        if (!isPlayer) {
            scoreKeeper.ModifyScore(score);
        } else {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    void PlayhitEffect() {
        if (hitEffect != null) {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

        }
    }
    void ShakeCamera() {
        if (cameraShake != null && applyCameraShake) {
            cameraShake.Play();
        }
    }
}
