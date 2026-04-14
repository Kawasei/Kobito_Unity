using System;
using KawausoForge.CommonComponent;
using R3;
using UnityEngine;

namespace KawausoForge.Scene.MainScene
{
    
    [UIPrefabPathAttribute("Layer/MainScene/MainObjectsLayer")]
    public class MainObjectsLayer : AbstractUILayer
    {
        public enum ObjectTypes
        {
            Bottle,
            Album
        }

        [SerializeField] private SelectableObject bottleObject;
        [SerializeField] private SelectableObject albumObject;
        
        private Subject<ObjectTypes> onSelect = new Subject<ObjectTypes>();
        private CompositeDisposable disposables = new CompositeDisposable();
        public Observable<ObjectTypes> OnSelect => onSelect;

        public override TransitionType TransitionIn => TransitionType.None;
        public override TransitionType TransitionOut => TransitionType.None;

        private void Awake()
        {
            bottleObject.OnSelect.Subscribe(_ =>
            {
                onSelect.OnNext(ObjectTypes.Bottle);
            }).AddTo(disposables);
            albumObject.OnSelect.Subscribe(_ =>
            {
                onSelect.OnNext(ObjectTypes.Album);
            }).AddTo(disposables);
        }
        
        private void OnDestroy()
        {
            disposables.Dispose();
        }
    }
}

