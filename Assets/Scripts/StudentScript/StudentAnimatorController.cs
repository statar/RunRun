using UnityEngine;

public class StudentAnimatorController : MonoBehaviour
{
    public static StudentAnimatorController instance;
    
    private Animator _animator;
    private static readonly int StudentRun = Animator.StringToHash("StudentRun");
    private static readonly int StudentAngry = Animator.StringToHash("StudentAngry");
    private static readonly int StudentStumble = Animator.StringToHash("StudentStumble");

    private void Awake()
    {
        instance = this;
        _animator = gameObject.GetComponent<Animator>();
    }
    
    public void StudentRunAnimation(bool setActive)
    {
        _animator.SetBool(StudentRun, setActive);
    }
    public void StudentAngryAnimation(bool setActive)
    {
        _animator.SetBool(StudentAngry, setActive);
        if(setActive) return;
        StudentRunAnimation(false);
    }
    public void StudentStumbleAnimationActive()
    {
        _animator.SetBool(StudentStumble,true);
    } 
    public void StudentStumbleAnimationPassive()
    {
        //using by animator event system 
        _animator.SetBool(StudentStumble,false);
    }
}
