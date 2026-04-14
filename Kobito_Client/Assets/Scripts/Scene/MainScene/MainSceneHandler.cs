using System;
using Cysharp.Threading.Tasks;
using KawausoForge.CommonComponent;
using KawausoForge.CommonComponent.Scene;
using R3;
using UnityEngine;

namespace KawausoForge.Scene.MainScene
{
    public class MainSceneHandler : AbstractSceneHandler
    {
        private CompositeDisposable disposables = new CompositeDisposable();

        private void Awake()
        {
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            var mainObjectsLayer = await UILayerHandler.Instance.ShowLayer<MainObjectsLayer>(UILayerHandler.LayerParentType.Back1);
            mainObjectsLayer.OnSelect.Subscribe(OnObjectSelected).AddTo(disposables);

            isInitialized = true;
        }

        private void OnObjectSelected(MainObjectsLayer.ObjectTypes objectType)
        {
            switch (objectType)
            {
                case MainObjectsLayer.ObjectTypes.Bottle:
                    break;
                case MainObjectsLayer.ObjectTypes.Album:
                    break;
            }
        }

        private void OnDestroy()
        {
            disposables.Dispose();
        }
    }
}

