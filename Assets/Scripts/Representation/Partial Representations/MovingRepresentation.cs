using System;
using UnityEngine;

public class MovingRepresentation : PartialRepresentation<IBoardPosition>
{
    public Vector3 prevPosition = Vector3.zero;
    public Vector3 nextPosition = Vector3.zero;

    public Vector3 prevDirection = Vector3.zero;
    public Vector3 nextDirection = Vector3.zero;

    private Animator animator;

    protected override void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    public override void SetPrevRepresentedState(IBoardPosition prevState)
    {
        // prevPosition = prevState.BoardPosition.Cartesians;
    }

    public override void SetNextRepresentedState(IBoardPosition nextState)
    {
        // nextPosition = nextState.BoardPosition.Cartesians;
    }

    public override void Sync()
    {
        this.transform.position = Vector3.Lerp(
                prevPosition,
                nextPosition,
                RepresentationManager.Instance.RepresentationStepElapsedFraction);

        this.transform.LookAt(
            Vector3.Lerp(
                this.transform.position + prevDirection,
                this.transform.position + nextDirection,
                RepresentationManager.Instance.RepresentationStepElapsedFraction));

        if (animator != null)
        {
            animator.SetFloat("RepSpeed", RepresentationManager.Instance.RepresentationSpeed);
            if (nextPosition != prevPosition) animator.SetFloat("BF", 1.0f);

            // animator.SetFloat("LR",
            //     Vector3.SignedAngle(
            //         lastDirection,
            //         nextDirection,
            //         Vector3.up));
        }
    }

}