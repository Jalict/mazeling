using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MazeGenerator))]
public class MazeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        MazeGenerator gen = (MazeGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Build Maze"))
        {
            gen.BuildMaze();
        }

        if(GUILayout.Button("Delete Maze"))
        {
            gen.DeleteChildren();
        }
    }
}
