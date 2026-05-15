using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SUBS.Core.Variables
{
    [Serializable]
    public class RandomizedValue_Int
    {
        [CustomValueDrawer("DrawSlider")]
        [ShowIf("_viewType", RandomizedValueMode.Default)]
        [SerializeField, HorizontalGroup("1"), Range(0, 100)] private int _value;

        [ShowIf("_viewType", RandomizedValueMode.RandomBetweenTwoValues)]
        [SerializeField, HorizontalGroup("1"), MinMaxSlider(0, 100)] private Vector2Int _randomBetweenTwoValues = new Vector2Int(1, 10);

        [ShowIf("_viewType", RandomizedValueMode.RandomFromValues)]
        [SerializeField, HorizontalGroup("1")] private List<int> _randomFromValues;

        [SerializeField, HorizontalGroup("1"), HideLabel] private RandomizedValueMode _viewType = RandomizedValueMode.Default;

        [SerializeField, FoldoutGroup("Advanced")] private int _minValue_ForDefault = 0;
        [SerializeField, FoldoutGroup("Advanced")] private int _maxValue_ForDefault = 100;

        private int _selectedValue = 0;
        public int SelectedValue => _selectedValue;

        public int GetRandomValue()
        {
            UpdateSelectedValue();
            return _selectedValue;
        }

        public void SetValue(int value)
        {
            _viewType = RandomizedValueMode.Default;
            _value = value;
        }

        public void SetValue(Vector2Int values)
        {
            _viewType = RandomizedValueMode.RandomBetweenTwoValues;
            _randomBetweenTwoValues = values;
        }

        public void SetValue(List<int> values)
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
                    _selectedValue = _selectedValue.GetRandomIntValue(_randomFromValues.ToArray());
                    break;
            }
        }

        #region Advanced
#if UNITY_EDITOR
        private int DrawSlider(int value, GUIContent content)
        {
            if (_maxValue_ForDefault < _minValue_ForDefault)
            {
                _maxValue_ForDefault = _minValue_ForDefault;
                Debug.Log("_maxValue < _minValue");
            }
            else if (_minValue_ForDefault > _maxValue_ForDefault)
            {
                _minValue_ForDefault = _maxValue_ForDefault;
                Debug.Log("_minValue > _maxValue");
            }

            return EditorGUILayout.IntSlider(content, value, _minValue_ForDefault, _maxValue_ForDefault);
        }
#endif
        #endregion Advanced
    }
}
