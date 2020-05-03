using System.Threading;
using UnityEngine;

//Generic Script that can be applied to every object necessary for taking damange (easy link up with sword/gun script)
public class Take_Damage : MonoBehaviour
{
    //added a variable to connect to the spawn manager
    //when this object dies, SpawnManager's newDeath() runs
    private SpawnManager script;
    public float maxHealth;
    float currentHealth;
    public Animator anim;
    public float deathAnimTimeInSeconds;
    public EnemyHealth healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar)
        {
            healthBar.SetMaxHealth(currentHealth);
        }
        script = GameObject.Find("GameManager").GetComponent<SpawnManager>();
    }

    public void DamageTaken(float damage)
    {
        if (currentHealth>0)
        {
            currentHealth -= damage;
            if (healthBar)
            {
                healthBar.SetHealth(currentHealth);
            }
            anim.SetTrigger("hurt");
            anim.SetFloat("Health", currentHealth);
            if (currentHealth <= 0)
            {
                script.newDeath();
                Destroy(gameObject, deathAnimTimeInSeconds);
            }
        }
    }
}
