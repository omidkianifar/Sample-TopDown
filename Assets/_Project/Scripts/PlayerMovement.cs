using System.Collections.Generic;
using UnityEngine;
using VContainer;


namespace _Project.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float rotationSpeed = 700f;
        [SerializeField] BombController bombPrefab;
        [SerializeField] Transform bombSpawnPoint;

        private CharacterController _controller;
        private Animator _animator;
        private Vector3 _moveDirection;

        private List<BombController> _activeBombs = new();

        [Inject] private CollectableObjectInventory _inventory;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Collectable"))
            {
                return;
            }

            var collectableObject = other.GetComponent<CollectableObjectController>();

            if(collectableObject is null) 
            {
                Debug.LogError($"Collectable Object:{other.gameObject.name} has not Component:{typeof(CollectableObjectController)}");

                return;
            }

            Debug.Log(" collected!");

            _inventory.AddItem(collectableObject.Data);

            Destroy(other.gameObject);
        }

        private void Update()
        {
            HandleMoveInputs();

            if (Input.GetKeyDown(KeyCode.E))
            {
                ThrowBomb();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                DestroyAllBombs();
            }
        }

        private void FixedUpdate()
        {
            ProcessMove();
        }

        private void HandleMoveInputs()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            _moveDirection = (forward * moveZ + right * moveX).normalized;

            float speed = new Vector2(moveX, moveZ).magnitude;

            _animator.SetBool("IsMoving", speed > 0);
        }

        private void ProcessMove()
        {
            _controller.Move(_moveDirection * moveSpeed * Time.deltaTime);

            if (_moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void ThrowBomb()
        {
            var bomb = Instantiate(bombPrefab, bombSpawnPoint.position, bombSpawnPoint.rotation);

            _activeBombs.Add(bomb);

            bomb.Throw(transform.forward);
        }

        private void DestroyAllBombs()
        {
            foreach (var bomb in _activeBombs)
            {
                if (bomb != null)
                {
                    bomb.DestroyBomb();
                }
            }

            _activeBombs.Clear();
        }
    }
}