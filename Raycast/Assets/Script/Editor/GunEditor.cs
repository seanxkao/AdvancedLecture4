using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Gun))]
public class GunEditor : Editor {
	protected Gun gun;

	void OnEnable(){
		gun = (Gun)target;
	} 

	public override void OnInspectorGUI(){
		//DrawDefaultInspector ();
		EditorGUI.BeginChangeCheck();
		gun.gunInfo = (GunInfo)EditorGUILayout.ObjectField ("Gun Info", gun.gunInfo, typeof(GunInfo), false);
		if (EditorGUI.EndChangeCheck ()) {
			gun.changeGun();
		}
		// Draw range
		gun.gunInfo.range = EditorGUILayout.FloatField ("Range", gun.gunInfo.range);
		// Save changes if modified
		if (GUI.changed && !EditorApplication.isPlaying) {
			EditorUtility.SetDirty (gun);
			EditorSceneManager.MarkSceneDirty (SceneManager.GetActiveScene());
		}
	}

	void OnSceneGUI(){
		GunInfo gunInfo = gun.gunInfo;
		// Draw radius editor
		Vector3 center = gun.transform.position;
		Handles.color = new Color(1f, 1f, 0.6f, 0.8f);
		float newRange = Handles.RadiusHandle(Quaternion.identity, center, gunInfo.range);
		// Save changes if modified
		if (!EditorApplication.isPlaying) {
			gunInfo.range = newRange;
			if (GUI.changed) {
				EditorUtility.SetDirty (gun);
				EditorSceneManager.MarkSceneDirty (SceneManager.GetActiveScene());
			}
		}
	}
}
