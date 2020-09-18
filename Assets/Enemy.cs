using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
   
    [SerializeField] private GameObject _cloudParticlePrefab;

    private void OnCollisionEnter2D(Collision2D collision) {

        //Destroy enemy if hit by bird
        Bird bird = collision.collider.GetComponent<Bird>();
        if (bird != null) {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); //Quaternion is some kind of rotation thing
            Destroy(gameObject);
            return;
        }

        //If hit by enemy then do nothing 
        Enemy enemy = collision.collider.GetComponent<Enemy>(); 
        if (enemy != null) {
            return; 
        }

        //collision.contancts[x].normal gets us the angle we were hit from
        //If we get hit from the top by not bird or enemy then destroy enemy
        if (collision.contacts[0].normal.y < -0.5) {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

    }

}
