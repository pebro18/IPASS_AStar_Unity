using UnityEngine;

/// <summary>
/// Class object used as a ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "PassingData", menuName = "SimplePathDataPassing")]
public class SimplePathTranslationScriptableObject : ScriptableObject
{
    public string[] Inputs;
}
