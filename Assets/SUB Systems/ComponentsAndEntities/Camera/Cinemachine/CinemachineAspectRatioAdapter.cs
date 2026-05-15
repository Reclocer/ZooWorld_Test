using UnityEngine;
using Unity.Cinemachine;

namespace SUBS.Core
{
    [ExecuteAlways]
    [SaveDuringPlay]
    [DisallowMultipleComponent]
    public class CinemachineAspectRatioAdapter : CinemachineExtension
    {
        public enum AdaptMode
        {
            MoveForward,
            ChangeFieldOfView
        }

        [Header("Mode")]
        [SerializeField] private AdaptMode mode = AdaptMode.ChangeFieldOfView;

        [Header("Reference")]
        [Tooltip("Aspect ratio at which camera keeps original values")]
        [SerializeField] private float referenceAspect = 16f / 9f;

        [Tooltip("Camera stops adapting outside this range")]
        [SerializeField] private float minAspect = 9f / 21f;

        [Tooltip("Camera stops adapting outside this range")]
        [SerializeField] private float maxAspect = 21f / 9f;

        [Header("FOV Settings")]
        [SerializeField] private float referenceFOV = 60f;

        [Tooltip("How much FOV changes relative to aspect difference")]
        [SerializeField] private float fovMultiplier = 20f;

        [Header("Forward Offset Settings")]
        [SerializeField] private float referenceForwardOffset = 0f;

        [Tooltip("How much camera moves forward/back relative to aspect difference")]
        [SerializeField] private float forwardMultiplier = 5f;

        [Header("Smoothing")]
        [SerializeField] private bool smooth = true;

        [SerializeField] private float smoothSpeed = 5f;

        private float _currentFov;
        private float _currentOffset;

        protected override void OnEnable()
        {
            base.OnEnable();

            _currentFov = referenceFOV;
            _currentOffset = referenceForwardOffset;
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime)
        {
            if (stage != CinemachineCore.Stage.Body)
                return;

            float aspect = (float)Screen.width / Screen.height;
            aspect = Mathf.Clamp(aspect, minAspect, maxAspect);
            float t = aspect - referenceAspect;

            if (mode == AdaptMode.ChangeFieldOfView)
            {
                float targetFov = referenceFOV - (t * fovMultiplier);

                if (smooth && deltaTime >= 0)
                {
                    _currentFov = Mathf.Lerp(
                        _currentFov,
                        targetFov,
                        deltaTime * smoothSpeed);
                }
                else
                {
                    _currentFov = targetFov;
                }

                LensSettings lens = state.Lens;
                lens.FieldOfView = _currentFov;
                state.Lens = lens;
            }
            else
            {
                float targetOffset = referenceForwardOffset + (t * forwardMultiplier);

                if (smooth && deltaTime >= 0)
                {
                    _currentOffset = Mathf.Lerp(
                        _currentOffset,
                        targetOffset,
                        deltaTime * smoothSpeed);
                }
                else
                {
                    _currentOffset = targetOffset;
                }

                state.RawPosition += state.RawOrientation * Vector3.forward * _currentOffset;
            }
        }
    }
}