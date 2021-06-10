using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputShoppingList : MonoBehaviour
{
    public string InputString;
    public TMP_InputField Input;
    public SimplePathTranslationScriptableObject ScriptableObj;

    public void OnInputChange()
    {
        InputString = Input.text;
    }

    public void PutIntoScriptableObj()
    {
        string[] InputArray = InputString.Split(',');
        ScriptableObj.Inputs = InputArray;
    }
}
