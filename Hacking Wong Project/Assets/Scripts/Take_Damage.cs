using System.Threading;
using UnityEngine;

//Generic Script that can be applied to every object necessary for taking damange (easy link up with sword/gun script)
public class Take_Damage : MonoBehaviour
{

    public float maxHealth;
    float currentHealth;
    public Animator anim;
    public float deathAnimTimeInSeconds;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hurt");
        anim.SetFloat("Health", currentHealth);
        if (currentHealth <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                Destroy(gameObject, deathAnimTimeInSeconds);
            }
        }
    }
}
