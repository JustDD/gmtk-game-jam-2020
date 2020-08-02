﻿using GMTK2020.Data;
using GMTK2020.Rendering;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace GMTK2020
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private BoardRenderer boardRenderer = null;
        [SerializeField] private BoardManipulator boardManipulator = null;
        [SerializeField] private PredictionEditor predictionEditor = null;
        [SerializeField] private LevelSpecification levelSpec = null;

        public Level Level { get; private set; }

        public ScoreKeeper ScoreKeeper { get; private set; }

        private void Start()
        {
            ScoreKeeper = new ScoreKeeper();
            LoadLevel();
        }

        public void LoadLevel()
        {
            HashSet<Vector2Int> levelPattern = new HashSet<Vector2Int>(levelSpec.MatchingPattern);

            // TODO: Use RNG with same seed during level generation and playback.
            var rng = new Random();
            Simulator simulator = new Simulator(levelPattern, rng, false);

            Level = new LevelGenerator(levelSpec, simulator).GenerateValidLevel();

            boardManipulator.Initialize(Level.Grid);
            predictionEditor.Initialize(Level.Grid);
            boardRenderer.RenderInitial(Level.Grid, ScoreKeeper);
        }
    }
}