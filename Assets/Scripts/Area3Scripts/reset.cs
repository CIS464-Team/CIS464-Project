using NUnit.Framework;
using UnityEngine;


public class reset : MonoBehaviour, IInteractable
{


        public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        foreach(var statue in FindObjectsByType<Statue>(FindObjectsSortMode.None))
        {
            statue.Reset();   
        } 
    }
}
