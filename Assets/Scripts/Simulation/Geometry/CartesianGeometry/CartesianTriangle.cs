using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianTriangle : ITriangle
{
    public IPoint a => new CartesianPoint(m_a);
    public IPoint b => new CartesianPoint(m_b);
    public IPoint c => new CartesianPoint(m_c);

    public CartesianTriangle(IPoint a, IPoint b, IPoint c)
    {
        m_a = new CartesianPoint(a);
        m_b = new CartesianPoint(b);
        m_c = new CartesianPoint(c);
    }

    public CartesianTriangle(CartesianPoint a, CartesianPoint b, CartesianPoint c)
    {
        m_a = a;
        m_b = b;
        m_c = c;
    }

    private readonly CartesianPoint m_a;
    private readonly CartesianPoint m_b;
    private readonly CartesianPoint m_c;
}
