using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MainMenu, SingleItemSearch, ShoppingList;
    
    public void ChangeActiveMenu(int id)
    {
        switch (id)
        {
            case 0:
                MainMenu.SetActive(true);
                SingleItemSearch.SetActive(false);
                ShoppingList.SetActive(false);
                break;
            case 1:
                MainMenu.SetActive(false);
                SingleItemSearch.SetActive(true);
                ShoppingList.SetActive(false);
                break;
            case 2:
                MainMenu.SetActive(false);
                SingleItemSearch.SetActive(false);
                ShoppingList.SetActive(true);
                break;
            default:
                break;
        }
    }

}
