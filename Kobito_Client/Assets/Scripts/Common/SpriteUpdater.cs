using R3;
using UnityEngine;
using UnityEngine.UI;

namespace KawausoForge.CommonComponent
{
    public class SpriteUpdater : MonoBehaviour
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float updateInterval = 0.5f;

        [SerializeField] private bool isPingPong = true;
        [SerializeField] private bool isAuto;

        private readonly Subject<Unit> onFinished = new Subject<Unit>();
        public Observable<Unit> OnFinished => onFinished;

        private float timer;
        private int currentIndex;
        private int direction = 1;
        private bool isPlaying;

        private void Awake()
        {
            if (targetImage == null)
            {
                targetImage = GetComponent<Image>();
            }

            if (isAuto)
            {
                Play();
            }
            else
            {
                ApplySprite(0);
            }
        }

        public void Play()
        {
            if (targetImage == null || sprites == null || sprites.Length == 0)
            {
                return;
            }

            timer = 0f;
            currentIndex = 0;
            direction = 1;
            isPlaying = true;

            ApplySprite(currentIndex);

            if (sprites.Length == 1)
            {
                Finish();
            }
        }

        public void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            if (sprites == null || sprites.Length == 0 || targetImage == null)
            {
                return;
            }

            if (sprites.Length == 1)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer < updateInterval)
            {
                return;
            }

            timer -= updateInterval;
            AdvanceSprite();
        }

        private void AdvanceSprite()
        {
            if (!isPingPong)
            {
                currentIndex++;

                if (currentIndex >= sprites.Length)
                {
                    if (isAuto)
                    {
                        currentIndex = 0;
                        ApplySprite(currentIndex);
                        return;
                    }
                    currentIndex = sprites.Length - 1;
                    ApplySprite(currentIndex);
                    Finish();
                    return;
                }

                ApplySprite(currentIndex);
                return;
            }

            currentIndex += direction;

            if (direction > 0 && currentIndex >= sprites.Length)
            {
                currentIndex = sprites.Length - 2;

                if (currentIndex < 0)
                {
                    currentIndex = 0;
                    ApplySprite(currentIndex);
                    if (!isAuto) Finish();
                    return;
                }

                direction = -1;
                ApplySprite(currentIndex);
                return;
            }

            if (direction < 0 && currentIndex < 0)
            {
                currentIndex = 1;
                if (currentIndex >= sprites.Length) currentIndex = 0;
                ApplySprite(currentIndex);
                if (!isAuto)
                {
                    currentIndex = 0;
                    ApplySprite(currentIndex);
                    Finish();
                    return;
                }
                direction = 1;
                return;
            }

            ApplySprite(currentIndex);
        }

        private void ApplySprite(int index)
        {
            if (targetImage == null || sprites == null || sprites.Length == 0)
            {
                return;
            }

            if (index < 0 || index >= sprites.Length)
            {
                return;
            }

            targetImage.sprite = sprites[index];
        }

        private void Finish()
        {
            isPlaying = false;
            onFinished.OnNext(Unit.Default);
        }
    }
}