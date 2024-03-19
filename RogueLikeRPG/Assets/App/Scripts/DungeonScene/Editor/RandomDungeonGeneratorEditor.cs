using App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.Generation;
using UnityEditor;
using UnityEngine;

namespace App.Scripts.DungeonScene.Editor
{
    [CustomEditor(typeof(AbstractDungeonGenerator), true)]
    public class RandomDungeonGeneratorEditor : UnityEditor.Editor
    {
        private AbstractDungeonGenerator _generator;

        private void Awake()
        {
            _generator = (AbstractDungeonGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Создание подземелья"))
            {
                _generator.GenerateDungeon();
            }
        }
    }
}
