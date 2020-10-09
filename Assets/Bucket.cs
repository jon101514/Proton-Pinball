using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

	public static Bucket instance;

	public GameObject ballPrefab;

	private Animator anim;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		anim = GetComponent<Animator>();
	}

	/** Pour the contents of the bucket, then raise after two seconds.
	 *
	 */
	public void Pour() {
		anim.SetBool("isDown", true);
		Invoke("Raise", 2.0f);
	}

	/** Raise the bucket back to its original position.
	 *
	 */
	public void Raise() {
		anim.SetBool("isDown", false);
	}
}
