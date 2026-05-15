using System;
using UnityEngine;

namespace SUBS.Core
{
    [AddComponentMenu("SUBS/Core/Input/" + nameof(SimpleSwipeInput))]
    public class SimpleSwipeInput : MonoBehaviour
    {
        [Header("Swipe zone (0..1, top-based)")]
        [Range(0f, 1f)] public float left = 0f;
        [Range(0f, 1f)] public float right = 1f;
        [Range(0f, 1f)] public float top = 0f;
        [Range(0f, 1f)] public float bottom = 1f;

        [Header("Swipe settings")]
        public float minSwipeDistance = 200f;

        [Header("Debug")]
        public bool showZone = true;

        public Action OnSwipeLeft;
        public Action OnSwipeRight;

        private Vector2 _startPos;
        private bool _tracking;

        private Rect PixelRect
        {
            get
            {
                float x = left * Screen.width;
                float y = top * Screen.height;
                float width = (right - left) * Screen.width;
                float height = (bottom - top) * Screen.height;

                return new Rect(x, y, width, height);
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            HandleMouse();
#else
        HandleTouch();
#endif
        }

        private void HandleMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = ToGuiSpace(Input.mousePosition);

                if (PixelRect.Contains(pos))
                {
                    _startPos = pos;
                    _tracking = true;
                }
            }

            if (Input.GetMouseButtonUp(0) && _tracking)
            {
                DetectSwipe(ToGuiSpace(Input.mousePosition));
                _tracking = false;
            }
        }

        private void HandleTouch()
        {
            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);
            Vector2 pos = ToGuiSpace(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (PixelRect.Contains(pos))
                    {
                        _startPos = pos;
                        _tracking = true;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (_tracking)
                    {
                        DetectSwipe(pos);
                        _tracking = false;
                    }
                    break;
            }
        }

        private void DetectSwipe(Vector2 endPos)
        {
            Vector2 delta = endPos - _startPos;

            if (Mathf.Abs(delta.x) < minSwipeDistance) return;
            if (Mathf.Abs(delta.x) < Mathf.Abs(delta.y)) return;

            if (delta.x > 0)
                OnSwipeRight?.Invoke();
            else
                OnSwipeLeft?.Invoke();
        }

        private Vector2 ToGuiSpace(Vector2 screenPos)
        {
            return new Vector2(
                screenPos.x,
                Screen.height - screenPos.y
            );
        }

        private void OnGUI()
        {
            if (!showZone) return;

            Rect r = PixelRect;

            GUI.color = new Color(0f, 1f, 0f, 0.15f);
            GUI.DrawTexture(r, Texture2D.whiteTexture);

            GUI.color = Color.green;
            GUI.Box(r, GUIContent.none);

            GUI.color = Color.white;
        }
    }
}
