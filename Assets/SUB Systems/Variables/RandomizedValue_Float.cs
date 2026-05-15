using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SUBS.Core.Variables
{
    [Serializable]
    public class RandomizedValue_Float
    {
        [CustomValueDrawer("DrawRange")]
        [ShowIf("_viewType", RandomizedValueMode.Default)]
        [SerializeField, HorizontalGroup("1"), Range(0, 100)] private float _value;

        [CustomValueDrawer("DrawSlider")]
        [ShowIf("_viewType", RandomizedValueMode.RandomBetweenTwoValues)]
        [SerializeField, HorizontalGroup("1"), MinMaxSlider(0, 100)] private Vector2 _randomBetweenTwoValues = new Vector2(0.1f, 1f);

        [ShowIf("_viewType", RandomizedValueMode.RandomFromValues)]
        [SerializeField, HorizontalGroup("1")] private List<float> _randomFromValues;

        [SerializeField, HorizontalGroup("1"), HideLabel] private RandomizedValueMode _viewType = RandomizedValueMode.Default;

        [SerializeField, FoldoutGroup("Advanced")] private float _minValue_ForDefault = 0f;
        [SerializeField, FoldoutGroup("Advanced")] private float _maxValue_ForDefault = 100f;

        private float _selectedValue = 0;
        public float SelectedValue => _selectedValue;

        public float GetRandomValue()
        {
            UpdateSelectedValue();
            return _selectedValue;
        }

        public void SetValue(float value)
        {
            _viewType = RandomizedValueMode.Default;
            _value = value;
        }

        public void SetValue(Vector2 values)
        {
            _viewType = RandomizedValueMode.RandomBetweenTwoValues;
            _randomBetweenTwoValues = values;
        }

        public void SetValue(List<float> values)
        {
            _viewType = RandomizedValueMode.RandomFromValues;
            _randomFromValues = values;
        }

        private void UpdateSelectedValue()
        {
            switch (_viewType)
            {
                case RandomizedValueMode.Default:
                    _selectedValue = _value;
                    break;

                case RandomizedValueMode.RandomBetweenTwoValues:
                    _selectedValue = Random.Range(_randomBetweenTwoValues.x, _randomBetweenTwoValues.y);
                    break;

                case RandomizedValueMode.RandomFromValues:
                    _selectedValue = _selectedValue.GetRandomFloatValue(_randomFromValues.ToArray());
                    break;
            }
        }

        #region Advanced
#if UNITY_EDITOR
        private float DrawRange(float value, GUIContent content)
        {
            if (_maxValue_ForDefault < _minValue_ForDefault)
            {
                _maxValue_ForDefault = _minValue_ForDefault;
                Debug.Log("_maxValue < _minValue");
            }

            return EditorGUILayout.Slider(content, value, _minValue_ForDefault, _maxValue_ForDefault);
        }

        private Vector2 DrawSlider(Vector2 value, GUIContent content)
        {
            if (_maxValue_ForDefault < _minValue_ForDefault)
            {
                _maxValue_ForDefault = _minValue_ForDefault;
                Debug.Log("_maxValue < _minValue");
            }

            float min = value.x;
            float max = value.y;

            EditorGUILayout.MinMaxSlider(content, ref min, ref max, _minValue_ForDefault, _maxValue_ForDefault);

            return new Vector2(min, max);
        }
#endif
        #endregion Advanced
    }
}
