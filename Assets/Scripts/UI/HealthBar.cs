using PlayerController;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerManager _player;
        [SerializeField] private Gradient _gradient;

        private Image _barImage;
        private float _fillAmount;

        private void Awake()
        {
            _barImage = GetComponentsInChildren<Image>()[1];
        }

        private void Start()
        {
            _fillAmount = 1f;
            _barImage.fillAmount = _fillAmount;
            _barImage.color = _gradient.Evaluate(_fillAmount);

            _player.Damaged += UpdateView;
        }

        private void OnDestroy()
        {
            _player.Damaged -= UpdateView;
        }

        private void UpdateView(float health)
        {
            _barImage.fillAmount = health;
            _barImage.color = _gradient.Evaluate(health);
        }
    }
}