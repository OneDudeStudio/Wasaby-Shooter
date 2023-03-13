using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.UI.Shop
{
    public class PausePanel : CanvasPanel
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _toMainMenuButton;
        [SerializeField] private Button _exitButton;

        private const int MainMenuSceneIndex = 0;

        public event Action Resumed;

        private void Start()
        {
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _toMainMenuButton.onClick.AddListener(OnToMainMenuButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDestroy()
        {
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _toMainMenuButton.onClick.RemoveListener(OnToMainMenuButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }
        
        private void OnResumeButtonClicked()
        {
            print("Resume");
            Resumed?.Invoke();
        }

        private void OnToMainMenuButtonClicked()
        {
            SceneManager.LoadScene(MainMenuSceneIndex);
        }
        
        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
    }
}