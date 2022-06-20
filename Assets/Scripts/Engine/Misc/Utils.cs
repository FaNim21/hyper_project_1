﻿using UnityEngine;

public class Utils
{
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vector = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vector.z = 0f;
        return vector;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static void Log(string message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    public static void LogWarning(string message)
    {
#if UNITY_EDITOR
        Debug.LogWarning(message);
#endif
    }
}
