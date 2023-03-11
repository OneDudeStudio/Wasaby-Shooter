using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UI.Shop
{
    public enum UpgradeState
    {
        NotPurshared,
        Purshared,
        Installed,
        NotInstalled
    }

    public class ShopUpgradeItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _upgradeName;
        [SerializeField] private TextMeshProUGUI _upgradeCostText;
        [SerializeField] private Image _upgradeIcon;
        [SerializeField] private Image _blockPanel;
        [SerializeField] private Button _upgradeHint;
        [SerializeField] private int _upgradeCost;
        [SerializeField] private UpgradeState _currentUpgradeState;
    }
}