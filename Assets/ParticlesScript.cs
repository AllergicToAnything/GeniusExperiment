using UnityEngine;
using UnityEditor;
using System;

public class ParticlesScript : MonoBehaviour
{
    public ParticleClass[] particleClasses;
    /*
    public void Rename()
    {
        for (int i = 0; i < particleClasses.Length; i++)
        {
            particleClasses[i].title = particleClasses[i].particleType.ToString() + "  " + i.ToString();
           
        }
    }
    */

}
/*
[CustomEditor(typeof(ParticlesScript))]
public class ParticlesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ParticlesScript particlesScript = (ParticlesScript)target;
        if (GUILayout.Button("Generate Particles Name"))
        {
            particlesScript.Rename();
        }

        DrawDefaultInspector();
    }


}*/


public enum ParticleType { SmallParticle , InnerParticle , OuterParticle }


[System.Serializable]
public class ParticleClass
{
    [HideInInspector]
    public string title;
    
    public ParticleType particleType;
    public Animator particle;
    public string animationResetName;
    public string animationSetTriggerName;
}


