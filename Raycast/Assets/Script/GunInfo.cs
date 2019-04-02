using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunInfo : ScriptableObject {	
	public GameObject modelP;
	public GameObject explosionP;
	public float timeScale = 1;
	public float range = 10;
	[HideInInspector]	public List<GunShot> gunShotList;
}
