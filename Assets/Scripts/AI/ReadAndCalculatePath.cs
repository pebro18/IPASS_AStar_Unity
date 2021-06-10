using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AStar;

public class ReadAndCalculatePath : MonoBehaviour
{
    [SerializeField] private SimplePathTranslationScriptableObject dataPassObj;
    List<Node>[] Path;

    private void Start()
    {
        foreach (var input in dataPassObj.Inputs)
        {
            // TODO:

        }
    }

   

}
