using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjectExtensions
{
    public static class GameObjectExtensions
    {
        public static GameObject[] GetChildsObjs(this GameObject obj)
        {
            List<GameObject> Childs = new List<GameObject>();

            for (int i = 0; i < obj.transform.childCount; i++)
            {
                Childs.Add(obj.transform.GetChild(i).gameObject);
            }
            
            return Childs.ToArray();

        }
    }
}
