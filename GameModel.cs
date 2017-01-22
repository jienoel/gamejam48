using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Assets.Scripts {
    class GameModel : MonoBehaviour {
        public static GameModel Instance;

        void Awake() {
            if (Instance == null) {
                Instance = this;
            }
        }

        [Header("Static")]
        public GameObject SoundGameObject;

        [Header("Dynamic")]
        public PitchManager PitchManager;
        public SoundManager SoundManager;
    }
}
