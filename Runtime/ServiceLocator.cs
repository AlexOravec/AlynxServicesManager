using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AlynxServicesManagerForUnity.Runtime
{
    public static class ServiceLocator
    {
        // Registered IGameServices
        private static List<IGameService> _services;

        // Create default services
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateDefaultServices()
        {
            // Create GameInstance from prefab
            var gameInstancePrefab = Resources.Load<GameObject>("GameInstance");

            // Check if prefab exists
            if (gameInstancePrefab == null)
            {
                Debug.LogWarning("ServiceLocator: GameInstance prefab not found");
                return;
            }

            // Check if scene is valid
            if (SceneManager.GetActiveScene().name == null)
            {
                Debug.LogWarning("ServiceLocator: Active scene is not valid");
                return;
            }

            var gameObject = Object.Instantiate(gameInstancePrefab);
            gameObject.name = "GameInstance";
        }

        // Register IGameService
        public static void Register(IGameService service)
        {
            _services ??= new List<IGameService>();

            // Check if service is already registered
            if (_services.Contains(service))
            {
                Debug.LogWarning("ServiceLocator: Service already registered with name " + service);
                return;
            }

            _services.Add(service);
        }

        // Unregister IGameService
        public static void Unregister(IGameService service)
        {
            if (_services == null)
            {
                Debug.LogWarning("ServiceLocator: No services registered");
                return;
            }

            // Check if service is registered
            if (!_services.Contains(service))
            {
                Debug.LogWarning("ServiceLocator: Service " + service + " not registered");
                return;
            }

            _services.Remove(service);
        }

        // Get IGameService
        public static T GetService<T>() where T : IGameService
        {
            if (_services == null)
            {
                Debug.LogWarning("ServiceLocator: No services registered");
                return default;
            }

            // Check if service is registered
            foreach (var service in _services)
                if (service is T gameService)
                    return gameService;

            return default;
        }

        // Try to get IGameService
        public static bool TryGetService<T>(out T service) where T : IGameService
        {
            if (_services == null)
            {
                Debug.LogWarning("ServiceLocator: No services registered");
                service = default;
                return false;
            }

            // Check if service is registered
            foreach (var gameService in _services)
                if (gameService is T gameService1)
                {
                    service = gameService1;
                    return true;
                }

            Debug.LogWarning("ServiceLocator: Service not registered");

            service = default;
            return false;
        }
    }
}