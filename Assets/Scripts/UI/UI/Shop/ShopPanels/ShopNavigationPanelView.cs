using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UI.Shop
{
    public class ShopNavigationPanelView : MonoBehaviour
    {
        [SerializeField] private List<Button> _navigationButtons;
        [SerializeField] private Button _exitButton;
        [SerializeField] private ShopMainPanelView _mainPanelView;




        public void MoveToOtherTab(int tabIndex)
        {
            foreach (var tabs in _mainPanelView.ShopContents)
            {
                tabs.gameObject.SetActive(false);
            }
            
            _mainPanelView.ShopContents[tabIndex].gameObject.SetActive(true);
        }
        
        
        
    }
}