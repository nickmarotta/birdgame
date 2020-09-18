using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    private Vector3 _initialPosition; //_ is a C# naming practice to denote private variable
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField] private float _launchPower = 500;

    private void Awake() {
        _initialPosition = transform.position;
    }

    private void Update() {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        //Velocity magnitude is the x and y velocities combined 
        //When the bird has little velocity (close to not moving) then reset
        if (_birdWasLaunched && 
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1) { 
                _timeSittingAround += Time.deltaTime; //Represents a frame in time. At 60 FPS will be .01666667
        } 

        //If bird is out of bounds or has been sitting around for > 3 then reset the scene
        if (transform.position.y > 15 ||
            transform.position.y < -15 ||
            transform.position.x > 15 ||
            transform.position.x < -15 ||
            _timeSittingAround > 3) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.red; 
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp() {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<LineRenderer>().enabled = false;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
    }

    private void OnMouseDrag() {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        transform.position = new Vector3(newPosition.x, newPosition.y); //A Vector3 is a 3 point coordinate. By only passing in an x and y, we're defaulting the Z to 0. 
    }

}
