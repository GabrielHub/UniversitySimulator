using UnityEngine;

public class Scrollable : MonoBehaviour {
	public float speed = 0.1f;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKey(KeyCode.UpArrow)) {
			this.gameObject.transform.Translate(new Vector3(0, -1 * speed * Time.deltaTime, 0));
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			this.gameObject.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			this.gameObject.transform.Translate(new Vector3(-1 * speed * Time.deltaTime, 0, 0));
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			this.gameObject.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
		}
	}
}
