using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayUIHandler : MonoBehaviour
{
    #region Variables
    [SerializeField] BallsGenerator ballsGenerator;
    [SerializeField] TMP_Text ballsCount;
    #endregion

    #region UIMethods
    void Update()
    {
        ballsCount.text = $"Balls generated: {ballsGenerator.numberToSpawn}";
    }
    #endregion
}
