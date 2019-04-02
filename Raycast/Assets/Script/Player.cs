using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public Gun gun;

	void Update () {
		if (Input.GetMouseButton (0) && gun.isReady()) {
			int width = Screen.width;
			int height = Screen.height;
			Ray ray = Camera.main.ScreenPointToRay (new Vector3(width/2, height/2, 0));
			gun.fire (ray);
		}
	}
}
