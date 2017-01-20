using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(Debugger))]
public class DebuggerInspector : Editor
{

    Debugger debugger;
    static bool[] chanels;
    static bool enableChanel;

    void OnEnable()
    {
        debugger = (Debugger)target;
        chanels = new bool[Enum.GetNames(typeof(LogChannel)).Length];
        int i = 0;
        foreach (var value in Enum.GetNames(typeof(LogChannel)))
        {
            chanels[i] = (debugger._channel & (1 << i)) != 0;
            i++;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        enableChanel = EditorGUILayout.Foldout(enableChanel, "LogChannel");
        if (enableChanel)
        {
            int i = 0;
            foreach (var value in Enum.GetNames(typeof(LogChannel)))
            {
                chanels[i] = EditorGUILayout.Toggle(value, chanels[i]);
                i++;
            }
        }

        if (Application.isPlaying)
        {
            for (int i = 0, count = chanels.Length; i < count; i++)
            {
                if (chanels[i])
                    debugger._channel |= 1 << i;
                else
                    debugger._channel &= ~(1 << i);
            }
        }
        else
        {
            for (int i = 0, count = chanels.Length; i < count; i++)
            {
                if (chanels[i])
                    debugger._channel |= 1 << i;
                else
                    debugger._channel &= ~(1 << i);
            }
//            for (int i = 0, count = chanels.Length; i < count; i++)
//            {
//                if (chanels[i])
//                    Debugger.channel |= 1 << i;
//                else
//                    Debugger.channel &= ~(1 << i);
//            }
//
//            debugger._channel = Debugger.channel; 
        }
       
           
    }
}
