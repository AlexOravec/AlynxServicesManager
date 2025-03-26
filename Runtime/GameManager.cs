﻿using UnityGameplayFramework.Runtime;

namespace AlynxServicesManagerForUnity.Runtime
{
    public abstract class GameManager : Object, IGameService
    {
        protected override void Initialize()
        {
            base.Initialize();

            ServiceLocator.Register(this);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            ServiceLocator.Unregister(this);
        }
    }
}