using UnityEngine;
public class StudentCollisionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "ActiveObstacleObject":
                other.gameObject.tag = "PassiveObstacleObject";
                CineMachineManager.instance.ShakeCamera();
                TeacherManager.instance.distance += 1f;
                StudentAnimatorController.instance.StudentStumbleAnimationActive();
                break;
            case "Teacher":
                gameObject.GetComponent<StudentMovementController>().stopPlayerMovement = true;
                CineMachineManager.instance.SwitchCamera2To1();
                StudentAnimatorController.instance.StudentAngryAnimation(true);
                TeacherManager.instance.TeacherRunAnimation(false);
                TeacherManager.instance.TeacherLookBackRotation(180f);
                CanvasController.instance.GameEndPanelSetActive(true);
                CanvasController.instance.GameEndPanelScoreText();
                break;
        }
    }
}
