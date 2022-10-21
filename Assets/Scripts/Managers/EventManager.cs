using System;

namespace Dev.MikeQ.SpaceShooter.Events {
    public class EventManager
    {
        //Although adding dependecy, this scipt will hopefull make the logic easier to write and read.

        public static Action EnemyDied;
        public static Action EnemyBorn;
        public static Action<UnityEngine.Vector3> EnemyDiedToLaser; //Supplies chance to spawn powerup

        public static Action EnemyLaserShot;
        public static Action PlayerLaserShot;
        public static Action DryFireShot;

        public static Action PlayerTookDamage;
        public static Action PlayerDied;

        public static Action AsteroidExploded;

        public static Action StartNextRound;
        public static Action GameIsReady;

        public static Action UpdateSoundEffectVolume;
        public static Action UpdateMusicVolume;

        public static Action<int> AddToScore;
    }


}
