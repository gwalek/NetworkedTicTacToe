using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //Debug.Log("Click!");

            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 WorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(WorldPosition, Vector2.zero);
            if (hit)
            {
                //Debug.Log("HIT! " + hit.collider.gameObject.name);
              
                GridLocation gridSpot = hit.collider.gameObject.GetComponent<GridLocation>(); 
                if (gridSpot)
                {
                    gridSpot.OnMouseClick(); 
                }
            }
        }
    }
}
