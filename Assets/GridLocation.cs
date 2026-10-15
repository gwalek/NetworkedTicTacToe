using UnityEngine;

public class GridLocation : MonoBehaviour
{
    public int GridLocationNumber = 1; 

    public void OnMouseClick()
    {
        Debug.Log("Click on Spot #" + GridLocationNumber);
    }

}
