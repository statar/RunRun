using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject studentPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GameDataScript.LoadLevelDataFromJson();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void ResetLevel()
    {
        GameDataScript.SaveLevelDataAsJson();
        studentPlayer.GetComponent<StudentMovementController>().ResetMovement();
        StudentAnimatorController.instance.StudentAngryAnimation(false);
        TeacherManager.instance.ResetTeacher();
    }

    public void StartGame()
    {
        CineMachineManager.instance.SwitchCamera1To2();
        StudentAnimatorController.instance.StudentRunAnimation(true);
        TeacherManager.instance.TeacherRun();

    }
}