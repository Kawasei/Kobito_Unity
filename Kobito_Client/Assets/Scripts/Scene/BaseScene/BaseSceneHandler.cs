using System;
using CommonComponent.Scene;
using Cysharp.Threading.Tasks;
using KawausoForge.CommonComponent;
using UnityEngine;

namespace KawausoForge.Scene.BaseScene
{
    public class BaseSceneHandler : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            
            SceneHandler.Instance.SetFadeHandler(FadeHandler.Instance);
            SceneHandler.Instance.ChangeScene("MainScene").Forget();
        }
    }
}

