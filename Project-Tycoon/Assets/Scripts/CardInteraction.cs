using UnityEngine;

public class CardInteraction : MonoBehaviour
{
    private void OnMouseEnter()
    {
        Debug.Log("WASSUP");
    }

    private void OnMouseExit()
    {
        Debug.Log("BYE BYE");
    }

    private Vector2 cardToInteract;

    private void Awake()
    {
        cardToInteract = transform.localPosition;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
