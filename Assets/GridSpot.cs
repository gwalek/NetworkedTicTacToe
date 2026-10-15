using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSpot : MonoBehaviour, IPointerDownHandler
{
    public int GridSpotNumber = 8; 

    public void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown();

/*
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            
        }
*/
    }

    private void OnMouseDown()
    {
        Debug.Log("Interface! @" + GridSpotNumber);
    }
}
