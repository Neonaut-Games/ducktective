using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public abstract class Boss : Hostile
    {

        [Header("Interface Settings")]
        public Slider healthBar;
        public Animator healthBarAnimator;

        private static readonly int IsEnabled = Animator.StringToHash("isEnabled");

        protected override void AfterStart() => RefreshHUD();

        protected override void OnAggro() => healthBarAnimator.SetBool(IsEnabled, true);

        protected override void OnDeAggro() => healthBarAnimator.SetBool(IsEnabled, false);

        protected override void OnTakeDamage() => RefreshHUD();

        private void RefreshHUD()
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

    }
}
