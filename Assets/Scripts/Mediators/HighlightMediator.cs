using System;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMediator
{
	// TODOHIGH: not really a mediator, will be when more reps are present?

    private RepresentationFactory representationFactory;

	private List<GameObject> selectionMarkers = new List<GameObject>();

    public HighlightMediator(RepresentationFactory representationFactory)
    {
        this.representationFactory = representationFactory;
    }

	public Vector3 Target { get; set; }

	public void Highlight(Vector3 position)
	{
		selectionMarkers.Add(representationFactory.HighlightPoint(position));
	}

    public void ClearHighlights(){
        foreach (GameObject selectionMarker in selectionMarkers)
		{
			GameObject.Destroy(selectionMarker);
		}

		selectionMarkers.Clear();
    }
}