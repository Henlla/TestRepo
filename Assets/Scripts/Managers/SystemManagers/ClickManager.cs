using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //FindObjectOfType<AudioManager>().Play("TapSound");
            //Vector3 hitP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(UniversalPosition(), Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.TryGetComponent<Interactable>(out var interactable))
                {
                    interactable.OnClicked();
                }
            }
        }
    }

    public static Vector3 UniversalPosition()
    {
        /*Touch touch = Input.GetTouch(0);
        Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);*/

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return position;
    }
}
