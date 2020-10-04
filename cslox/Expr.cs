namespace cslox
{

public abstract class Expr
{
}

public class Binary
{
	Expr left;
	Token oper;
	Expr right;

	public Binary(Expr left, Token oper, Expr right)
	{
		this.left = left;
		this.oper = oper;
		this.right = right;
	}
}
public class Grouping
{
	Expr expr;

	public Grouping(Expr expr)
	{
		this.expr = expr;
	}
}
public class Literal
{
	object value;

	public Literal(object value)
	{
		this.value = value;
	}
}
public class Unary
{
	Token oper;
	Expr right;

	public Unary(Token oper, Expr right)
	{
		this.oper = oper;
		this.right = right;
	}
}

}
