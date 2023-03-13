using System;
using UI.UI.Shop;
using UnityEngine;

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
        [SerializeField] private bool _isInPauseState;
        [SerializeField] private bool _isInShopState;
        [SerializeField] private bool _isInDefaultState;
        
        [SerializeField] private CanvasPanel _currentPanel;
        [SerializeField] private CanvasPanel _previousPanel;

        public bool IsPaused => _isInPauseState;
        
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
            }
        }

        public bool TryGoToPause()
        {
            if (_isInPauseState)
            {
                OpenNewPanel(_defaultPanel);
                _canvasState = CanvasState.InGame;
                _isInPauseState = false;
            }
            else if(!_isInPauseState)
            {
                _canvasState = CanvasState.Pause;
                OpenNewPanel(_pausePanel);
                _isInPauseState = true;
            }

            return _isInPauseState;
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
