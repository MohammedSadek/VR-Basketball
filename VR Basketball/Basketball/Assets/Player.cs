using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Basketball basketball;
	public Vector3 basketballOffset;
	public float basketballDistance = 1f;
	public float minimumShootingForce = 400f;
	public float maximumShootingForce = 1000f;

	private bool holdingBasketball;
	private bool calculatingShot;
	private float shootingTimer = 0f;

	// Use this for initialization
	void Start () {
		holdingBasketball = true;
	}

	public void PickBasketball () {
		shootingTimer = 0f;
		holdingBasketball = true;
		calculatingShot = false;

		basketball.hitSomething = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (holdingBasketball) {
			basketball.transform.position = this.transform.position + this.transform.forward * basketballDistance + basketballOffset;
			basketball.GetComponent<Rigidbody> ().useGravity = false;

			if (calculatingShot) {
				shootingTimer += Time.deltaTime;
			}

			if (GvrViewer.Instance.Triggered || Input.GetKeyDown("space")) {
				if (calculatingShot == false) {
					calculatingShot = true;
				} else if (holdingBasketball) {
					holdingBasketball = false;
					basketball.GetComponent<Rigidbody> ().useGravity = true;

					float calculatedScale = Mathf.Min(shootingTimer, 1f);
					float calculatedForce = minimumShootingForce + (maximumShootingForce - minimumShootingForce) * calculatedScale;

					basketball.GetComponent<Rigidbody> ().AddForce (this.transform.forward * calculatedForce);
				}
			}
		}
	}
}
