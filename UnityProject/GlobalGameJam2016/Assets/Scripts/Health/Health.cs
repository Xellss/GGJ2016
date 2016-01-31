using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
    public Spawner SceneSpawner;

    public int MaxHealth;
    [HideInInspector]
    public float CurrentHealth;

    public bool Enemy = false;

    [Tooltip("Remove on Death after RemoveDelay")]
    public bool RemoveOnDeath = true;
    [Tooltip("Start ParticleEffect after ParticleDelay")]
    public bool CreateParticle = true;
    public bool RagdollOnDeath = true;

    public float ParticleDelay = 0;
    public float RemoveDelay = 0;

    public ParticleSystem DeathParticle;

    private Transform myTransform;

    void Awake()
    {
        DeathParticle.playOnAwake = false;
    }

	void Start() 
    {
        myTransform = GetComponent<Transform>();
        CurrentHealth = MaxHealth;
	}

    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (CurrentHealth <= 0)
            Dead();
    }

    public void RemoveHealth(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Dead(); 
    }

    public void GiveHealth(float points)
    {
        CurrentHealth += points;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }

    public void HealFull()
    {
        CurrentHealth = MaxHealth;
    }

    public void Dead()
    {
        if (Enemy)
            SceneSpawner.AliveEnemys.Remove(this.gameObject);

        if (RagdollOnDeath)
        {
            //ToDoooooooooooooooooooo
        }

        if (RemoveOnDeath)
            StartCoroutine(RemoveTimer());

        if (CreateParticle)
            StartCoroutine(ParticleTimer());
    }

    IEnumerator RemoveTimer()
    {
        yield return new WaitForSeconds(RemoveDelay);
        GameObject.Destroy(this.gameObject);
    }

    IEnumerator ParticleTimer()
    {
        yield return new WaitForSeconds(ParticleDelay);
        print("Particle Play");
        DeathParticle.Play(true);
    }
}
