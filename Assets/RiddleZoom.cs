using UnityEngine;
using UnityEngine.UI;

public class RiddleZoom : MonoBehaviour
{
    public Canvas zoomCanvas;
    private bool isZoomed = false;

    private void Start()
    {
        zoomCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isZoomed && Input.GetMouseButtonDown(0))
        {
            CloseZoom();
        }

        if (!isZoomed && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                OpenZoom();
            }
        }
    }

    private void OpenZoom()
    {
        isZoomed = true;
        zoomCanvas.gameObject.SetActive(true);
    }

    private void CloseZoom()
    {
        isZoomed = false;
        zoomCanvas.gameObject.SetActive(false);
    }
}
