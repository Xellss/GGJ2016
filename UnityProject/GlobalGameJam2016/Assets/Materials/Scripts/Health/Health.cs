using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
    //public GameObject canvas;

    public Spawner SceneSpawner;
    GameObject spawn;

    public int MaxHealth;
    [HideInInspector]
    public float CurrentHealth { get; set; }

    public bool Enemy = false;

    public bool Player = true;

    [Tooltip("Remove on Death after RemoveDelay")]
    public bool RemoveOnDeath = true;
    [Tooltip("Start ParticleEffect after ParticleDelay")]
    public bool CreateParticle = true;
    public bool RagdollOnDeath = true;
    public GameObject RagdollOnDeathObject;

    public float ParticleDelay = 0;
    public float RemoveDelay = 0;

    private Transform myTransform;

    void Awake()
    {
        spawn = GameObject.Find("EnemySpawn");
        SceneSpawner = spawn.GetComponent<Spawner>();
        //canvas.SetActive(false);
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

        if (Player)
        {
            //canvas.SetActive(true);
        }

        if (RagdollOnDeath)
        {
            Instantiate(RagdollOnDeathObject, myTransform.position, myTransform.rotation);
            GameObject.Destroy(this.gameObject);
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
    }
}
