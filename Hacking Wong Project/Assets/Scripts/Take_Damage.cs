using UnityEngine;

//Generic Script that can be applied to every object necessary for taking damange (easy link up with sword/gun script)
public class Take_Damage : MonoBehaviour
{

    public float maxHealth;
    float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void DamageTaken(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                Destroy(gameObject); //object dies add dead animations if appropriate/deselect collider
            }
        }
    }
}
