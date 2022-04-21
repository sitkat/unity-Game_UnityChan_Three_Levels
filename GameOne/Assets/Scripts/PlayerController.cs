using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody _Rigidbody;
    float MovemenetSpeed = 8.0f;
    float JumpForce = 1.0f;
    public float DistantToGround = 0.1f;
    bool OnGround = true;
    private AnimationManager _AnimationManager;
    public int Strenght = 10;
    public GameObject AttackPoint;
    public float AttackRange = 0.7f;
    public LayerMask AttackableLayer;
    public int AttackCountDownSeconds = 1;
    public bool EnableAttack = true;
    public Image DeathScreen;

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        _AnimationManager = GetComponent<AnimationManager>();
    }
    private void FixedUpdate()
    {
        GroundCheck();
        if (Input.GetKey(KeyCode.Space) && OnGround) Jump();
        if (Input.GetKey(KeyCode.Mouse0) && EnableAttack) Attack();
        _Rigidbody.MovePosition(CalculateMovement());
        if (!IsMoving()) SetRotation();
        else SetRotationOnMove();
        SetPlayerAnimation();
        NegativeY();
    }
    Vector3 CalculateMovement()
    {

        float HorizontalDirection = Input.GetAxis("Horizontal");
        float VerticalDirection = Input.GetAxis("Vertical");

        return _Rigidbody.transform.position + new Vector3(HorizontalDirection, 0, VerticalDirection) * Time.fixedDeltaTime * MovemenetSpeed;
    }

    private void Jump()
    {
        _Rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    private void GroundCheck()
    {
        OnGround = Physics.Raycast(transform.position, Vector3.down, DistantToGround);
    }

    private void SetRotation()
    {
        Plane PlayerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0;
        if (PlayerPlane.Raycast(ray, out hitdist))
        {
            Vector3 TargetPoint = ray.GetPoint(hitdist);
            Quaternion TargetRotation = Quaternion.LookRotation(TargetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, MovemenetSpeed * Time.deltaTime);
        }
    }

    private void SetRotationOnMove()
    {
        Quaternion TargetRotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, MovemenetSpeed * Time.deltaTime);
    }

    private bool IsMoving()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) != Vector3.zero;
    }

    private bool NegativeY()
    {
        float posY;
        posY = transform.position.y;
        if (posY < -2)
        {
            DeathScreen.gameObject.SetActive(true);
            return false;
        }
        else return true;
            
    }
    private void SetPlayerAnimation()
    {
        if (OnGround)
        {
            if (Input.GetKey(KeyCode.Mouse0)) _AnimationManager.SetAnimationAttack();
            else
            {
                if (IsMoving()) _AnimationManager.SetAnimationRun();
                else _AnimationManager.SetAnimationIdle();
            }
        }
        else _AnimationManager.SetAnimationJump();
    }
    private void Attack()
    {
        Collider[] HitedColliders = Physics.OverlapSphere(AttackPoint.transform.position, AttackRange, AttackableLayer);
        foreach (Collider HitedCollider in HitedColliders)
        {
            IAttackable attackable = HitedCollider.gameObject.GetComponent<IAttackable>();
            attackable.DealDamage(Strenght);
            Debug.Log(HitedCollider.name + " нанесено " + Strenght + " урона");
        }
        EnableAttack = false;
        StartCoroutine(AttackCountDown());
    }
    private IEnumerator AttackCountDown()
    {
        int Counter = AttackCountDownSeconds;
        while(Counter >0)
        {
            yield return new WaitForSeconds(1);
            Counter--;
        }
        EnableAttack = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * DistantToGround));
        Gizmos.DrawSphere(AttackPoint.transform.position, AttackRange);
    }
}
