using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UI.Shop.ShopPanels
{
    public class ShopWeaponStatsPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _weaponName;
        [SerializeField] private Image _currentWeaponIcon;


        [SerializeField] private TextMeshProUGUI _weaponDamageText;
        [SerializeField] private TextMeshProUGUI _weaponRangeText;
        [SerializeField] private TextMeshProUGUI _weaponSpeedText;
        [SerializeField] private TextMeshProUGUI _weaponBulletTypeText;
        
        
        [SerializeField] private TextMeshProUGUI _firstModuleText;
        [SerializeField] private TextMeshProUGUI _secondModuleText;
        [SerializeField] private TextMeshProUGUI _thirdhtModuleText;
    }
}
