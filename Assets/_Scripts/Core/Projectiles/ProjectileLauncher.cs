using Core.UI;
using UnityEngine;
using Zenject;

namespace Core.Projectiles
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _spawnPosition;

        [SerializeField] private Vector3 _maxProjectileLaunchForce;
        [SerializeField] private Vector3 _minProjectileLaunchForce;

        private Projectile _projectileInstance;
        private Vector3 _directionToLaunch;

        private void Awake()
        {
            _projectileInstance = Instantiate(_projectilePrefab, _spawnPosition.position, Quaternion.identity);
            _projectileInstance.Landed += Launch;
        }

        private void Start() => GhostLaunch();

        public void GhostLaunch()
        {
            ResetProjectile();
            SelectDirectionToLaunch();
            _projectileInstance.GhostLaunch(_directionToLaunch);
        }

        private void Launch()
        {
            _projectileInstance.Landed -= Launch;
            _projectileInstance.Landed += Level.RestartLevel;

            ResetProjectile();
            _projectileInstance.VisibleLaunch(_directionToLaunch);
        }

        private void SelectDirectionToLaunch()
        {
            _directionToLaunch = new Vector3(
                Random.Range(_minProjectileLaunchForce.x, _maxProjectileLaunchForce.x),
                Random.Range(_minProjectileLaunchForce.y, _maxProjectileLaunchForce.y),
                Random.Range(_minProjectileLaunchForce.z, _maxProjectileLaunchForce.z));
        }

        private void ResetProjectile()
        {
            _projectileInstance.Rigidbody.velocity = Vector3.zero;
            _projectileInstance.transform.position = _spawnPosition.position;
        }
    }
}