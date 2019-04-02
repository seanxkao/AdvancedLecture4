using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

[CustomEditor(typeof(GunInfo))]
public class GunInfoEditor : Editor {
	protected GunInfo gunInfo;

	void OnEnable(){
		gunInfo = (GunInfo)target;
	} 

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		// Draw GunShot list
		if (gunInfo.gunShotList == null) {
			gunInfo.gunShotList = new List<GunShot> ();
		}
		int count = gunInfo.gunShotList.Count;
		int insertId = -1;
		int removeId = -1;
		EditorGUILayout.LabelField ("GunShots", GUILayout.Width(80));
		EditorGUI.indentLevel++;
		for(int i=0;i<count;i++) {
			EditorGUILayout.BeginHorizontal ();
			EditorGUILayout.LabelField ("Shot "+ (i+1), GUILayout.Width(80));
			GunShot gunShot = gunInfo.gunShotList [i];
			gunShot.coolDown = EditorGUILayout.FloatField (gunShot.coolDown, GUILayout.Width(50));
			gunShot.gunShotSE = (AudioClip)EditorGUILayout.ObjectField (gunShot.gunShotSE, typeof(AudioClip), false);
			gunInfo.gunShotList [i] = gunShot;
			if(GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(15))){
				insertId = i;
			}
			if(GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(15))){
				removeId = i;
			}
			EditorGUILayout.EndHorizontal();
		}
		EditorGUI.indentLevel--;
		// process add, insert, delete button of list
		if (insertId != -1) {
			gunInfo.gunShotList.Insert (insertId, gunInfo.gunShotList[insertId]);
		}
		if (removeId != -1) {
			gunInfo.gunShotList.RemoveAt(removeId);
		}
		if (GUILayout.Button ("Add GunShot")) {
			GunShot newGunShot;
			if (count > 0) {
				newGunShot = gunInfo.gunShotList [count - 1];
			} else {
				newGunShot = new GunShot ();
			}
			gunInfo.gunShotList.Add(newGunShot);
		}
		// Save changes if modified
		if (GUI.changed) {
			EditorUtility.SetDirty (gunInfo);
			EditorSceneManager.MarkSceneDirty (SceneManager.GetActiveScene());
		}
	}
}
