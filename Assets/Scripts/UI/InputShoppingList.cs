using UnityEngine;
using TMPro;

/// <summary>
/// Simple UI controller that turns the inputfield string into an array used for the ScriptableObject
/// </summary>
public class InputShoppingList : MonoBehaviour
{
    public string InputString;
    public TMP_InputField Input;
    public SimplePathTranslationScriptableObject ScriptableObj;

    /// <summary>
    /// Gets the text from inputfield and turn into a string
    /// Set in the InputField Settings when to call this method
    /// </summary>
    public void OnInputChange()
    {
        InputString = Input.text;
    }

    /// <summary>
    /// Turns the string into an array and set it into the ScriptableObject
    /// </summary>
    public void PutIntoScriptableObj()
    {
        string[] InputArray = InputString.Split(',');
        ScriptableObj.Inputs = InputArray;
    }
}
