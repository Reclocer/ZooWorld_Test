using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SUBS.Core;
using Zenject;

namespace SUBS.Core.ResourceManagement
{
    [Serializable]
    public class ResourceLoader : IInitializable, IHaveAsyncAction
    {
        [SerializeField] private float _delay = 0.1f;
        private const string ARTS_FOR_LOAD_PATH = "Sprites/ArtsForAsyncLoad/";

        [Space]
        public List<ArtForImageItem> Items;

        public void Initialize()
        {
            this.DoAfterSeconds(0.2f, nameof(ResourceLoader), () =>
            {
            //StartCoroutine(AsyncLoad());
        });
        }

        private IEnumerator AsyncLoad()
        {
            foreach (ArtForImageItem artForImage in Items)
            {
                ResourceRequest request = Resources.LoadAsync<Sprite>($"{ARTS_FOR_LOAD_PATH}{artForImage.SpriteName}");
                artForImage.Image.sprite = request.asset as Sprite;
                yield return new WaitForSeconds(_delay);
            }
        }
    }

    [Serializable]
    public class ArtForImageItem
    {
        //public int ID;
        public Image Image;
        public string SpriteName;
        public Sprite Sprite;
    }
}