using UnityEngine;

namespace Interactions
{
    public class Door : InputInteraction
    {
        private bool isOpen;
        private Animator animator;
        [SerializeField] private float openTime;
        private float currentTime;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isOpen)
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0f)
                {
                    isOpen = false;
                }
            }
            else
            {
                currentTime = openTime;
            }

            animator.SetBool("IsOpen", isOpen);
        }

        public override void OnInteract()
        {
            base.OnInteract();

            if (!isOpen)
            {
                isOpen = true;
            }
        }
    }
}
