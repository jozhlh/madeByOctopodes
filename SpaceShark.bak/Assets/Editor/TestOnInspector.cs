using UnityEngine;
using System.Collections;
using UnityEditor;

//Creates a custom Label on the inspector for all the scripts named ScriptName
// Make sure you have a ScriptName script in your
// project, else this will not work.
/*
[CustomEditor(typeof(Segment))]
public class TestOnInspector : Editor
{
    private const int opts = 20;
    public string[] titles = new string[opts] { "NW_L", "NW", "N", "NE", "NE_R",
                                                "W", "W", "C", "E", "E",
                                                "SW_L", "SW", "S", "SE", "SE_R",
                                                "XX", "SW_U", "S", "SE_U", "XX"};
    private int itemSelected = 0;
    private int xCounts = opts / 4;
    private Texture[] ims;

    void Awake()
    {
        ims = new Texture[opts];
        for(int i = 0; i < opts; i++)
        {
            ims[i] = (Texture)AssetDatabase.LoadAssetAtPath("Assets/Textures/Buttons/EditorButtons7.png",(typeof(Texture)));
        }
    }

    public override void OnInspectorGUI()
    {
        int sel = 0;
        GUILayout.Label("This is a Label in a Custom Editor");
        GUILayout.Space(16.0f);
        itemSelected = GUILayout.SelectionGrid(itemSelected, titles, xCounts, GUILayout.Width(256.0f), GUILayout.Height(200.0f) );
    }
}
*/