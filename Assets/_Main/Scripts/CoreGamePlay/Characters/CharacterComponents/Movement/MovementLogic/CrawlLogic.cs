using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class CrawlLogic : MovementLogic
    {
        [SerializeField, Range(0f, 1f)] private float _zigzagAmplitude = 0.35f;
        [SerializeField, Range(0.1f, 20f)] private float _zigzagFrequency = 4f;

        public override MovementType MovementType => MovementType.Crawl;
        public override CharacterTrait ProvidedTraits => CharacterTrait.CanCrawl;

        private float _zigzagPhase;

        public override void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info)
        {
            Vector3 planarDir = MovementVelocityUtility.GetPlanarDirection(info.MoveDirectionWorld);

            if (planarDir == Vector3.zero)
            {
                MovementVelocityUtility.ApplyPlanarVelocity(body, deltaTime, in info, Vector3.zero);
                return;
            }

            _zigzagPhase += deltaTime * _zigzagFrequency * (Mathf.PI * 2f);
            Vector3 right = Vector3.Cross(Vector3.up, planarDir);
            float offset = Mathf.Sin(_zigzagPhase) * _zigzagAmplitude;
            Vector3 crawlDir = (planarDir + right * offset).normalized;
            MovementVelocityUtility.ApplyPlanarVelocity(body, deltaTime, in info, crawlDir);
        }
    }
}
