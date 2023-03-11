using UnityEngine;

namespace UI.UI.Shop
{
    public class ShopPanelView : CanvasPanel
    {
        public void CloseShopCanvas()
        {
            this.gameObject.SetActive(false);
        }
    }
}
