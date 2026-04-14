using System;
using KawausoForge.CommonComponent;
using KawausoForge.Extension;
using R3;
using UnityEngine;

namespace KawausoForge.Scene.MainScene
{
    public class SelectableObject : MonoBehaviour
    {
        [SerializeField] private KawausoButton button;
        [SerializeField] private SpriteUpdater spriteUpdater;
        private CompositeDisposable disposable = new CompositeDisposable();
        
        private Subject<Unit> onSelect = new Subject<Unit>();
        private bool isPlaying = false;
        public Observable<Unit> OnSelect => onSelect;

        private void Awake()
        {
            spriteUpdater.OnFinished.Subscribe(x =>
            {
                onSelect.OnNext(Unit.Default);
                isPlaying = false;
            }).AddTo(disposable);
            button.OnClickObservable.Subscribe(_ =>
                {
                    if (isPlaying)
                    {
                        return;
                    }
                    isPlaying = true;
                    spriteUpdater.Play();
                }
            ).AddTo(disposable);
        }

        private void OnDestroy()
        {
            disposable?.Dispose();
        }
    }

}
