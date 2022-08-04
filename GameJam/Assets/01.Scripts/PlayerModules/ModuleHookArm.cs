using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleHookArm : ModuleDefaultArm
{
    public LayerMask whatIsGround;
    public KeyCode hookKey = KeyCode.LeftShift;
    
    private HingeJoint2D _hj;
    private Rigidbody2D _hitPointRigid;
    private float _hookDist = 15f;
    private int _hookCount = 0;
    
    private void Start()
    {
        GameManager.Player.OnGroundCollision += () => 
        { 
            _hj.enabled = false;
            _hookCount = 0;
        };

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
            if(Input.GetKeyDown(hookKey))
            {
                Vector3 playerPos = GameManager.Player.transform.position;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                Vector3 hookShotDir = (mousePos - playerPos).normalized;

                RaycastHit2D hookHit = Physics2D.Raycast(playerPos, hookShotDir, _hookDist, whatIsGround);

                if (hookHit.collider != null)
                {
                    _hitPointRigid.position = hookHit.point;

                    _hj.enabled = true;
                }
            }
            
            if(Input.GetKeyUp(hookKey))
            {
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
