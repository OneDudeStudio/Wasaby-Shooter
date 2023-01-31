using UnityEngine;


public enum CanvasType
{
    ShopHint,
    Shop
}
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _shopHintCanvas;
    [SerializeField] private GameObject _shopCanvas;
    
    public void SetCanvasActive(CanvasType type)
    {
        if (type == CanvasType.ShopHint)
        {
            _shopHintCanvas.SetActive(true);
        }
        if (type == CanvasType.Shop)
        {
            _shopCanvas.SetActive(true);
        }
    }

    public void SetCanvasDeactive(CanvasType type)
    {
        if (type == CanvasType.ShopHint)
        {
            _shopHintCanvas.SetActive(false);
        }
        if (type == CanvasType.Shop)
        {
            _shopCanvas.SetActive(false);
        }
    }
}
