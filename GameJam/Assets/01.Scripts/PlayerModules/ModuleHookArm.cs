using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHookArm : ModuleDefaultArm
{
    public GameObject rope;
    public LayerMask whatIsGround;
    public KeyCode hookKey = KeyCode.LeftShift;
    
    private HingeJoint2D _hj;
    private Rigidbody2D _hitPointRigid;
    RaycastHit2D hookHit;
    private float _hookDist = 15f;
    private int _hookCount = 0;
    
    private void Start()
    {
        // 언제나 하드코딩을 실생활에서 사용 할수 있도록 하자.
        GameManager.Player.OnGroundCollision += () => 
        { 
            _hj.enabled = false;
            _hookCount = 0;
            rope.SetActive(false);
        };

        rope = Instantiate(rope);
        GameObject tempObj = new GameObject("hitPoint");
        tempObj.transform.parent = transform;

        _hitPointRigid = tempObj.AddComponent<Rigidbody2D>();
        _hitPointRigid.gravityScale = 0;
        _hitPointRigid.constraints = RigidbodyConstraints2D.FreezePosition;

        _hj = GameManager.Player.GetComponent<HingeJoint2D>();

        if(_hj == null)
        {
            GameManager.Player.gameObject.AddComponent<HingeJoint2D>();
            Debug.LogWarning("Player에게 HingeJoint2d를 넣어주세요");
        }

        _hj.enabled = false;
        _hj.connectedBody = _hitPointRigid;
    }
    public override void ArmMoving()
    {
        if (!GameManager.Player.IsGround && _hookCount == 0)
        {
            // 언제나 하드코딩을 실생활에서 사용 할수 있도록 하자.
            Vector3 playerPos = GameManager.Player.transform.position;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Vector3 hookShotDir = (mousePos - playerPos).normalized;

            if (Input.GetKeyDown(hookKey))
            {
                hookHit = Physics2D.Raycast(playerPos, hookShotDir, _hookDist, whatIsGround);

                if (hookHit.collider != null)
                {
                    _hitPointRigid.position = hookHit.point;
                    rope.transform.localScale = new Vector3(0.1f, Vector3.Distance(playerPos, hookHit.point), 1f);
                    _hj.enabled = true;
                }
            }

            if (Input.GetKey(hookKey))
            {
                rope.SetActive(true);
                rope.transform.position = Vector3.Lerp(playerPos, hookHit.point, 0.5f);
                // 언제나 하드코딩을 실생활에서 사용 할수 있도록 하자.
                rope.transform.rotation = Quaternion.LookRotation(((Vector2)playerPos - hookHit.point).normalized) * Quaternion.Euler(90, 0, 0);
                rope.transform.localRotation *= Quaternion.Euler(0, -90, 0);
            }

            if (Input.GetKeyUp(hookKey))
            {
                rope.SetActive(false);
                _hj.enabled = false;
                _hookCount++;
            }
        }
        else
        {
            _hj.enabled = false;
        }
    }
}
