public class CartesianTriangle : Triangle
{
    private readonly IVector _A;
    private readonly IVector _B;
    private readonly IVector _C;

    public override IVector A { get => _A; }
    public override IVector B { get => _B; }
    public override IVector C { get => _C; }

    public CartesianTriangle(IVector A, IVector B, IVector C)
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
