﻿using UnityEngine;
using Framework.DI;

namespace Modules.RootComposition.PokemonGoScriptableObjs
{
    public class DependentBehaviour : MonoBehaviour
    {
        [Inject] M1Behaviour m1;
        [Inject] IM2Behaviour m2;
        [Inject(Id = "M22")] IM2Behaviour m22;
        [Inject(Id = "M23")] IM2Behaviour m23;

        void Start()
        {
            m1.Log();
            m2.Log();
            m22.Log();
            m23.Log();
        }
    }
}
