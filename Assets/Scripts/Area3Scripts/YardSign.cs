using NUnit.Framework;
using UnityEngine;
using TMPro;

public class YardSign : MonoBehaviour, IInteractable
{
    public bool IsClicked { get; private set;}
    public string signID { get; private set;}
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private string message = "";

    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        signID ??= globalHelper.GenerateUUID(gameObject); 
        popupPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            bool HierachyState = popupPanel.activeInHierarchy;
            closePopup();
        }
        
    }

        public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        Debug.Log("E pressed");
        if (!CanInteract()) return;
        clickTomb();

    }

    private void clickTomb()
    {
        Debug.Log("clicked AF");
        if (!isOpen) OpenPopup();
        else closePopup();

        bool HierachyState = popupPanel.activeInHierarchy;
        Debug.Log($"is work???: {HierachyState}");
    }

    private void OpenPopup()
    {
        popupText.text = message;
        popupPanel.SetActive(true);
        isOpen = true;
    }

    private void closePopup()
    {
        popupPanel.SetActive(false);
        isOpen = false;
    }

}
