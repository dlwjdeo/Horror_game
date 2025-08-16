using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class Player : MonoBehaviour
{
    [Header("모듈")]
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private PlayerInventory playerInventory;

    //캐시
    private float moveX, moveY;
    private bool jumpDown, interactDown, dropDown,stair;
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerMover = GetComponent<PlayerMover>();
        playerInteraction = GetComponent<PlayerInteraction>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        ReadInput();
        DispatchInstantActions();
    }

    private void FixedUpdate()
    {
        playerMover.TickMove(moveX, moveY);
        playerMover.TryJump();
    }

    private void ReadInput()    //인풋 캐시 저장
    {
        var input = playerInputManager ?? PlayerInputManager.Instance;
        if (input == null) return;

        moveX = input.MoveX();
        moveY = input.MoveY();
        interactDown = input.InteractPressedThisFrame();
        jumpDown = input.JumpPressedThisFrame();
        dropDown = input.DropPressedThisFrame();
        stair = input.StairPressedThisFrame();
    }

    private void DispatchInstantActions()   //저장된 인풋캐시로 실행
    {
        if (interactDown)
           playerInteraction.TryInteract();
        if (jumpDown)
            playerMover.RequestJump();  //물리 동작이기 때문에 리퀘스트 Update -> 실제 동작은 fixedUpdate
        if (dropDown)
            playerInventory.DropItem(transform.position);
        if (stair && !playerMover.IsOnStair)
            playerMover.EnterStair();
    }
}
