using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        ItemData itemData = (ItemData)target;

        //DrawPropertiesExcluding(serializedObject, "colliderSize", "colliderOffset");

        if (!itemData.nonDestuctible)
        {
            DrawDefaultInspector();
            //itemData.colliderSize = EditorGUILayout.Vector2Field("Collider Size", itemData.colliderSize);
            //itemData.colliderOffset = EditorGUILayout.Vector2Field("Collider Offset", itemData.colliderOffset);
        }
        else
        {
            DrawPropertiesExcluding(serializedObject, "colliderSize", "colliderOffset");
        }
        serializedObject.ApplyModifiedProperties();
    }
}