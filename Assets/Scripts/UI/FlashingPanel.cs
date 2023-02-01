using PlayerController;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class FlashingPanel : MonoBehaviour
    {
        [SerializeField] private PlayerManager _player;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player.Damaged += Flash;
        }

        private void OnDestroy()
        {
            _player.Damaged -= Flash;
        }

        private void Flash(float health)
        {
            _animator.SetTrigger("Damaged");
        }
    }
}