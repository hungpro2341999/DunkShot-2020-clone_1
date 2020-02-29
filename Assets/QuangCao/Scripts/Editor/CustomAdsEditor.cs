using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ManagerAds))]
public class CustomAdsEditor : Editor
{
    private ManagerAds managerAds;
    private void OnEnable()
    {
        managerAds = (ManagerAds)target;
    }
    public override void OnInspectorGUI()
    {
        GUILayout.Space(10);
        EditorGUILayout.BeginVertical();
        managerAds.isUseAdmob = EditorGUILayout.Toggle("Use Admob", managerAds.isUseAdmob);
        if (managerAds.isUseAdmob)
        {
            EditorGUILayout.BeginHorizontal();
            //  GUILayout.Label("ID Banner");
            managerAds.ID_BANNER = EditorGUILayout.TextField("ID Banner", managerAds.ID_BANNER.Trim());
            EditorGUILayout.EndHorizontal();
            managerAds.bannerType = (BannerType)EditorGUILayout.EnumPopup("Banner Type:", managerAds.bannerType);
            EditorGUILayout.BeginHorizontal();
            // GUILayout.Label("ID Show Full");
            managerAds.ID_FULL = EditorGUILayout.TextField("ID Show Full", managerAds.ID_FULL.Trim());
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            // GUILayout.Label("ID Video");
            managerAds.ID_REWARD = EditorGUILayout.TextField("ID Video", managerAds.ID_REWARD.Trim());
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Space(10);
        managerAds.isUseUnityAds = EditorGUILayout.Toggle("Use Unity Ads", managerAds.isUseUnityAds);
        if (managerAds.isUseUnityAds)
        {
            EditorGUILayout.BeginHorizontal();

            managerAds.ID_UNITY_ADS = EditorGUILayout.TextField("ID Unity", managerAds.ID_UNITY_ADS.Trim());
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Space(10);
        managerAds.isStartBanner = EditorGUILayout.Toggle("Banner In Start", managerAds.isStartBanner);
        GUILayout.Space(10);
        managerAds.ID_MORE = EditorGUILayout.TextField("ID MORE", managerAds.ID_MORE);
        GUILayout.Space(10);
        managerAds.isUseFireBase = EditorGUILayout.Toggle("Use Firebase", managerAds.isUseFireBase);
        GUILayout.Space(10);
        if (GUILayout.Button("Save"))
        {

            SetUpDefineSymbolsForGroup(managerAds.USE_ADMOB, managerAds.isUseAdmob);
            SetUpDefineSymbolsForGroup(managerAds.USE_UNITY_ADS, managerAds.isUseUnityAds);
            SetUpDefineSymbolsForGroup(managerAds.USE_FIREBASE, managerAds.isUseFireBase);
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(managerAds);
        }


        EditorGUILayout.EndVertical();
    }

    private void SetUpDefineSymbolsForGroup(string key, bool enable)
    {
        Debug.Log(enable);
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
        // Only if not defined already.
        if (defines.Contains(key))
        {
            if (enable)
            {
                Debug.LogWarning("Selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ") already contains <b>" + key + "</b> <i>Scripting Define Symbol</i>.");
                return;
            }
            else
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines.Replace(key, "")));

                return;
            }
        }
        else
        {
            // Append
            if (enable)
                PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + key));
        }


    }
}
