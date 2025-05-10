using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public float maxHealth = 100;
    float currentHealth;
    MiniAI2 miniai2;
    //ArrowEntagle arrowEntaglee;

    void Start()
    {
        currentHealth = maxHealth;
        miniai2 = GetComponent<MiniAI2>();

    }

    public void TakeDamage(float damage)
    {

        miniai2.leftrightspeed = 0;
        currentHealth -= damage;
        anim.SetBool("Walking", false);     //damage ald��� vaki y�r�me anim. durduruyor
        anim.SetTrigger("Hurt");            //hasar alma anim. �al��t�r�yor
        anim.SetBool("Walking", true);      //tekrardan y�r�meye devam ediyor
        if (currentHealth <= 0)
        {
            Die();
        }
        void Die()
        {

            anim.SetBool("IsDead", true);
            this.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            miniai2.followSpeed = 0;
            Destroy(gameObject, 2f);


        }



    }



}
