using App.Scripts.GameScenes.Weapon.Bullet;
using App.Scripts.GameScenes.Weapon.MeleeWeapon;
using App.Scripts.GameScenes.Weapon.RangeWeapon;

namespace App.Scripts.GameScenes.Player.Components
{
   public class PlayerCombat
   {
      private static PlayerCombat _instance;
      public static PlayerCombat Instance => _instance ??= new PlayerCombat();

      private BulletFactory _bulletFactory;
      private bool _isRolling;
   
      public void Initialize(BulletFactory bulletFactory)
      {
         _bulletFactory = bulletFactory;
         PlayerAnimator.Instance.OnPlayerRolling += GetPlayerRollState;
         PlayerController.Instance.OnPlayerHandleCombat += HandleCombat;
      }
      private void GetPlayerRollState(bool isRolling)
      {
         _isRolling = isRolling;
      }
   
      private void HandleCombat()
      {
         if (_isRolling) return;

         if (PlayerCurrentWeapon.Instance.CurrentPlayerWeapon as MeleeWeaponSO)
         {
            MeleeWeapon.Instance.Attack();
         }

         if (PlayerCurrentWeapon.Instance.CurrentPlayerWeapon as RangeWeaponSO)
         {
            RangeWeapon.Instance.Shoot(_bulletFactory);
         }
      }
      public void Dispose()
      {
         PlayerAnimator.Instance.OnPlayerRolling -= GetPlayerRollState;
         PlayerController.Instance.OnPlayerHandleCombat -= HandleCombat;
      
      }
   }
}
