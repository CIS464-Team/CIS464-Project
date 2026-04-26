using NUnit.Framework;
using UnityEngine;

public class tomb : MonoBehaviour, IInteractable
{
    public bool IsClicked { get; private set;}
    public string tombID { get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tombID ??= globalHelper.GenerateUUID(gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public void Innteract()
    {
        throw new System.NotImplementedException();
    }

    private void clickTomb()
    {
        
    }


}
