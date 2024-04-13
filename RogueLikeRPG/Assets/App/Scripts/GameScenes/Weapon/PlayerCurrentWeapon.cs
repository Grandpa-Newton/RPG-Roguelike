using App.Scripts.GameScenes.Player;
using App.Scripts.GameScenes.Player.Components;
using App.Scripts.GameScenes.Weapon;

public class PlayerCurrentWeapon 
{
   private static PlayerCurrentWeapon _instance;

   public static PlayerCurrentWeapon Instance => _instance ??= new PlayerCurrentWeapon();

   public WeaponItemSO CurrentPlayerWeapon { get; private set; }
   public CurrentWeaponsSO CurrentMeleeAndRangeWeaponsSO { get; private set; }

   private bool _isMeleeWeapon;
   
   public void Initialize( CurrentWeaponsSO currentWeaponsSO)
   {
      CurrentMeleeAndRangeWeaponsSO = currentWeaponsSO;
      
      PlayerWeaponSwitcher.Instance.OnPlayerSwapWeapon += SetCurrentPlayerWeapon;
   }

   private void SetCurrentPlayerWeapon(bool isMeleeWeapon)
   {
      _isMeleeWeapon = isMeleeWeapon;
      if (_isMeleeWeapon)
      {
         CurrentPlayerWeapon = CurrentMeleeAndRangeWeaponsSO.EquippedMeleeWeapon;
      }
      else
      {
         CurrentPlayerWeapon = CurrentMeleeAndRangeWeaponsSO.EquippedRangeWeapon;
      }
   }

   public void SetPlayerCurrentWeapon(WeaponItemSO currentPlayerWeapon)
   {
      CurrentPlayerWeapon = currentPlayerWeapon;
   }
   
}
