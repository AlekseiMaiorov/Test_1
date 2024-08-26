using System;
using Scripts.Enum;
using Spine.Unity;

namespace Scripts.SpineAnimation
{
    [Serializable]
    public class AnimationAsset
    {
        public AnimationReferenceAsset AssetReference;
        public CharacterStates CharacterState;
    }

    [Serializable]
    public class AnimationAssets
    {
        public WeaponType WeaponType;
        public AnimationAsset[] Animations;
    }
}