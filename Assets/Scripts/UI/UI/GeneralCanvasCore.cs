using System;
using UI.UI.Shop;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.UI
{
    public enum CanvasState
    {
        InGame,
        Pause,
        Shop,
        InteractWith,
        Default
    }
    

    public class GeneralCanvasCore : MonoBehaviour
    {
        [SerializeField] private CanvasState _canvasState;
        
        [SerializeField] private CanvasPanel _defaultPanel;
        [SerializeField] private CanvasPanel _shopPanel;
        [SerializeField] private PausePanel _pausePanel;

        
        [Header("State")] 
        [SerializeField] private bool _isInInPauseState;
        [SerializeField] private bool _isInShopStateState;
        [SerializeField] private bool _isInDefaultState;
        
        [SerializeField] private CanvasPanel _currentPanel;
        [SerializeField] private CanvasPanel _previousPanel;

        public bool IsInPausedState => _isInInPauseState;
        public event Action Resumed;
        
        private void Start()
        {
            _previousPanel = _defaultPanel;
            _currentPanel = _defaultPanel;
        }

        private void OnEnable()
        {
            _pausePanel.Resumed += OnResumed;
        }

        private void OnDisable()
        {
            _pausePanel.Resumed -= OnResumed;
        }

        private void OnResumed()
        {
            if (_pausePanel)
            {
                _pausePanel.DeactivatePanel();
                TryGoToPause();
                Resumed?.Invoke();
            }
        }

        public bool TryGoToPause()
        {
            if (_isInInPauseState)
            {
                OpenNewPanel(_defaultPanel);
                _canvasState = CanvasState.InGame;
                _isInInPauseState = false;
            }
            else if(!_isInInPauseState)
            {
                _canvasState = CanvasState.Pause;
                OpenNewPanel(_pausePanel);
                _isInInPauseState = true;
            }

            return _isInInPauseState;
        }

        public void OpenNewPanel(CanvasPanel panelToOpen)
        {
            _previousPanel = _currentPanel;
            _previousPanel.DeactivatePanel();
            _currentPanel = panelToOpen;
            _currentPanel.ActivatePanel();
        }
    }
}
