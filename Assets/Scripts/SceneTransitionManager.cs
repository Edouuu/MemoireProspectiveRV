using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private static int previousScene;

    private void Awake()
    {
        // Empêcher la duplication du GameObject lors du changement de scène
        if (FindObjectsOfType<SceneTransitionManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public static void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Téléporter le joueur à la position appropriée en fonction de la scène précédente
        switch (previousScene)
        {
            case 0:
                TeleportPlayerToPosition(new Vector3(-1f, 1f, 0f)); // Exemple de position pour la scène 1
                break;
            case 1:
                TeleportPlayerToPosition(new Vector3(1f, 2f, 3f)); // Exemple de position pour la scène 2
                break;
            // Ajouter d'autres cas pour chaque scène avec leurs positions respectives
            default:
                TeleportPlayerToPosition(Vector3.zero); // Position par défaut si la scène précédente n'est pas spécifiée
                break;
        }

        previousScene = scene.buildIndex;
    }

    private void TeleportPlayerToPosition(Vector3 position)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadScene(1);
        }
    }
}
