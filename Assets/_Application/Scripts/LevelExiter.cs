﻿using GMTK2020.Audio;
using GMTK2020.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GMTK2020
{
    public class LevelExiter : MonoBehaviour
    {
        [SerializeField] private string levelSceneName = null;
        [SerializeField] private string winSceneName = null;
        [SerializeField] private string tutorialSceneName = null;
        [SerializeField] private LevelSequence levelSequence = null;

        private SoundManager soundManager;

        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        public void RestartLevel()
        {
            if (soundManager)
                soundManager.PlayEffect(SoundManager.Effect.CLICK);
            LoadLevelScene();
        }

        public void LoadNextLevel()
        {
            if (soundManager)
                soundManager.PlayEffect(SoundManager.Effect.CLICK);

            ++GameProgression.CurrentLevelIndex;
            if (GameProgression.CurrentLevelIndex >= levelSequence.Levels.Count)
                LoadWinScene();
            else
            {
                if (levelSequence.Levels[GameProgression.CurrentLevelIndex].TutorialBoard != null)
                    LoadTutorialScene();
                else
                    LoadLevelScene();
            }
        }

        private void LoadLevelScene() => SceneManager.LoadScene(levelSceneName);
        private void LoadWinScene() => SceneManager.LoadScene(winSceneName);
        private void LoadTutorialScene() => SceneManager.LoadScene(tutorialSceneName);
    }
}