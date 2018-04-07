using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	Rigidbody2D thisBody;
	public float speed = 2;
    GameManager gameManager;	float walkTimer;
	float walkerCount;
	bool playingMovementEffect;
	void Start () {
		thisBody = gameObject.GetComponent<Rigidbody2D> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();		
		gameObject.GetComponent<AudioSource> ().volume = 1f;
		walkTimer = 1.2f;
		walkerCount = 0.8f;
		playingMovementEffect = false;
	}
	// Update is called once per frame
	void Update () {

		float xMovement = Input.GetAxis ("Horizontal");
		if ((xMovement < .5 && xMovement > 0) || (xMovement > -.5 && xMovement < 0)) {
			thisBody.velocity = Vector2.zero;
			gameObject.GetComponent<AudioSource> ().loop = false;
			walkerCount = 0.8f;
			playingMovementEffect = false;
			gameObject.GetComponent<AudioSource> ().Stop ();

		} else {
			walkerCount += Time.deltaTime;
			if (walkTimer < walkerCount && !playingMovementEffect) {
				walkerCount = 0;
				gameObject.GetComponent<AudioSource> ().loop = true;

				gameObject.GetComponent<AudioSource> ().Play ();
				playingMovementEffect = true;
			}
		}
		Vector2 movement = new Vector2 (xMovement, 0)*speed;

		thisBody.AddForce (movement);

		switch (gameManager.gameState) {

            case (GameManager.GameState.Free):
                thisBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            case (GameManager.GameState.Talking):
                thisBody.constraints = RigidbodyConstraints2D.FreezePosition;
                break;
               

		}

        


     }
	public void OnTriggerStay2D(Collider2D other){
       
		if (other.gameObject.GetComponent<Interactables>() != null) {
        
			if (Input.GetKeyDown (KeyCode.E)) {
                
				other.gameObject.GetComponent<Interactables> ().triggerInteraction ();
			}
		}
	}
	public void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.GetComponent<Interactables>() != null) {
			other.gameObject.GetComponent<Interactables> ().highlight ();
		}
	}
	public void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.GetComponent<Interactables> () != null) {
			other.gameObject.GetComponent<SpriteRenderer> ().color = new Color32 (255, 255, 255, 255);
		}
	}
}
