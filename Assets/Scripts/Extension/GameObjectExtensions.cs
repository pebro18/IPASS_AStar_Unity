using System.Collections.Generic;
using UnityEngine;

namespace GameObjectExtensions
{
    /// <summary>
    /// Extra custom methods for GameObjects
    /// </summary>
    public static class GameObjectExtensions
    {

        /// <summary>
        /// Gets every child GameObject that is in the GameObject
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Array containing every child gameobject under target</returns>
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
