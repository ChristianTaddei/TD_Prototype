public class CartesianTriangle : Triangle
{
    private readonly Vector _A;
    private readonly Vector _B;
    private readonly Vector _C;

    public override Vector A { get => _A; }
    public override Vector B { get => _B; }
    public override Vector C { get => _C; }

    public CartesianTriangle(Vector A, Vector B, Vector C)
    {
        _A = A;
        _B = B;
        _C = C;
    }

    public CartesianTriangle(Triangle t)
    {
        _A = t.A;
        _B = t.B;
        _C = t.C;
    }
}
