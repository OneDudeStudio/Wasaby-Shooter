using System.Collections;
using System.Collections.Generic;
using UI.UI.Shop;
using UnityEngine;

public class ShopMainPanelView : MonoBehaviour
{
    [SerializeField] private ShopContent _currentShopContent;
    
    [SerializeField] private List<ShopContent> _tabContents;

    public List<ShopContent> ShopContents => _tabContents;
}
