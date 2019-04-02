using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun : MonoBehaviour {
	public GunInfo gunInfo;
	protected int shootCounter;
	protected float nextShootTime;

	void Start(){
		nextShootTime = 0;
	}

	public void changeGun(){
		DestroyImmediate (transform.GetChild (0).gameObject);
		GameObject model = (GameObject)PrefabUtility.InstantiatePrefab (gunInfo.modelP);
		model.transform.SetParent(transform, false);
	}

	public bool isReady(){
		return Time.time > nextShootTime;
	}

	public void fire(Ray ray){
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, gunInfo.range)) {
			GetComponent<AudioSource> ().PlayOneShot (gunInfo.gunShotList [shootCounter].gunShotSE);
			nextShootTime = Time.time + gunInfo.timeScale * gunInfo.gunShotList [shootCounter].coolDown;
			GameObject explosion = Instantiate (gunInfo.explosionP);
			explosion.transform.position = hit.point;
			Destroy (explosion, 1);
			shootCounter = (shootCounter+1)%gunInfo.gunShotList.Count;
		}
	}
}
