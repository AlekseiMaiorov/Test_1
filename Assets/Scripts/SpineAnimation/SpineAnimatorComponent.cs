using System.Linq;
using Scripts.Enum;
using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

namespace Scripts.SpineAnimation
{
    public class SpineAnimatorComponent : MonoBehaviour
    {
        public AnimationReferenceAsset CurrentAsset => _currentAsset;
        
        [SerializeField]
        private AnimationAssets[] _animationAssets;
        [SerializeField]
        private SkeletonAnimation _skeletonAnimation;
        
        private bool _currentAnimationLoop;
        private TrackEntry _currentTrackEntry;
        private AnimationReferenceAsset _currentAsset;

        public AnimationReferenceAsset GetReferenceAsset(WeaponType weaponType, CharacterStates state)
        {
            var asset = _animationAssets.Where(assets => assets.WeaponType == weaponType)
                                        .SelectMany(assets => assets.Animations)
                                        .FirstOrDefault(animationAsset => animationAsset.CharacterState == state)
                                       ?.AssetReference;
            return asset;
        }

        public void ResetPose()
        {
            _skeletonAnimation.Skeleton.SetToSetupPose();
            _skeletonAnimation.AnimationState.ClearTracks();
            _currentAsset = null;
            _currentAnimationLoop = false;
        }

        public void SetAnimation(
            WeaponType weapon,
            CharacterStates state,
            bool loop,
            AnimationState.TrackEntryDelegate endAnimationCallback = null,
            float timeScale = 1)
        {
            SetAnimation(GetReferenceAsset(weapon, state), loop, endAnimationCallback, timeScale);
        }

        public void SetAnimation(
            AnimationReferenceAsset animation,
            bool loop,
            AnimationState.TrackEntryDelegate endAnimationCallback = null,
            float timeScale = 1,
            AnimationReferenceAsset nextAnimation = null)
        {
            if (animation == null)
            {
                Debug.LogWarning("Отсутствует ссылка на анимацию для текущего состояния.");
                return;
            }

            if (_currentAsset != null)
            {
                if (_currentAsset == animation)
                {
                    return;
                }
            }

            TrackEntry trackEntry = _skeletonAnimation.state.SetAnimation(0, animation, loop);

            if (nextAnimation != null)
            {
                trackEntry = _skeletonAnimation.state.AddAnimation(0, nextAnimation, _currentAnimationLoop, 0);
                _currentAsset = nextAnimation;
            }
            else
            {
                _currentAsset = animation;
                _currentAnimationLoop = loop;
            }

            if (trackEntry == null)
            {
                return;
            }

            trackEntry.TimeScale = timeScale;

            if (endAnimationCallback == null)
            {
                return;
            }

            trackEntry.Complete
                += endAnimationCallback;
        }

        public void SetAnimationShot(
            WeaponType weapon,
            CharacterStates state,
            bool loop,
            AnimationState.TrackEntryDelegate endAnimationCallback = null,
            float timeScale = 1)
        {
            SetAnimation(GetReferenceAsset(weapon, state), loop, endAnimationCallback, timeScale, _currentAsset);
        }
    }
}