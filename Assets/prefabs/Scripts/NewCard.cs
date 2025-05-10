using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewCard : MonoBehaviour
{
    public Transform teleport;
    private float damage = 15f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(waitForHurt(collision));

    }
    IEnumerator waitForHurt(Collider2D collision)
    {
        collision.transform.position = teleport.transform.position;
        yield return new WaitForSeconds(0.05f);
        if (collision.GetComponent<CristalCharControler>() != null) { collision.GetComponent<CristalCharControler>().takeDamage(damage); }
        if (collision.GetComponent<Metal_Char_Combat>() != null) { collision.GetComponent<Metal_Char_Combat>().takeDamage(damage); }
       
    }
}
