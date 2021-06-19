using UnityEngine;

public class TeacherManager : MonoBehaviour
{
    private Animator _animator;
    private bool _run;
    
    public static TeacherManager instance;
    public GameObject studentPlayer;
    [HideInInspector]
    public float distance;
    

    private void Awake()
    {
        instance = this;
        _animator = gameObject.GetComponent<Animator>();
        distance = -7f;
    }

    private void FixedUpdate()
    {
        TeacherMovement();
    }

    private void TeacherMovement()
    {
        if (_run)
        {
            var maxX = 0f;
            var studentPos = studentPlayer.transform.position;
            transform.position = Vector3.Lerp(transform.position, (studentPos + (Vector3.forward * distance)),
                4f * Time.deltaTime);
        }
        if (transform.position.z >= studentPlayer.transform.position.z)
            distance = 1f;
    }

    public void TeacherRun()
    {
        _run = true;
        TeacherRunAnimation(true);
    }

    public void TeacherRunAnimation(bool value)
    {
        _animator.SetBool("teacherRun", value);
    }

    public void TeacherLookBackRotation(float y)
    {
        transform.rotation = Quaternion.Euler(0f, y, 0f);
    }

    public void ResetTeacher()
    {
        TeacherLookBackRotation(0f);
        distance = -7f;
        _run = false;
        TeacherRunAnimation(false);
        transform.position = new Vector3(0f,0.23f,-3.38f);
    }
}
