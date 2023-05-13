//we need that using statement
using UnityEditor;
using UnityEngine;
using static Codice.Client.Common.Connection.AskCredentialsToUser;

//We connect the editor with the Weapon SO class
[CustomEditor(typeof(Hero))]
//We need to extend the Editor
public class HeroEditor : Editor
{
    //Here we grab a reference to our Weapon SO
    Hero hero;

    private void OnEnable()
    {
        //target is by default available for you
        //because we inherite Editor
        hero = target as Hero;
    }

    //Here is the meat of the script
    public override void OnInspectorGUI()
    {
        //Draw whatever we already have in SO definition
        base.OnInspectorGUI();
        //Guard clause
        if (hero.Photo == null)
            return;

        //Convert the weaponSprite (see SO script) to Texture
        Texture2D texture = AssetPreview.GetAssetPreview(hero.Photo);
        //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
        //This allows us to place the image JUST UNDER our default inspector
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        //Draws the texture where we have defined our Label (empty space)
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}