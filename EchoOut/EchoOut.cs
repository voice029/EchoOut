namespace EchoOutLogging;

public class EchoOut<T> : EchoOut
{
    public EchoOut(){}
    public EchoOut(dynamic value)
    {
        Val = value;
    }
    public override dynamic trueSelf() => this;
        
    public static implicit operator T?(EchoOut<T> rhs)
    {
        rhs.AssignmentOpLog(rhs);
        return rhs.Val;
    }

    private void AssignmentOpLog(EchoOut<T> rhs)
    {
        if (string.IsNullOrWhiteSpace(rhs.Output))
        {
            rhs.Output = $"{Val}";
        }
        OutputLogger?.Invoke(rhs.Output);
    }

    private void OutputConversion(T rhs)
    {
        ValueToOutput?.Invoke(rhs);
    }

    public static implicit operator EchoOut<T>(T rhs)
    {
        var echoOut = new EchoOut<T>(){Val = rhs};
        echoOut.OutputConversion(rhs);
        return echoOut;
    }
        
    public static implicit operator string?(EchoOut<T> rhs)
    {
        return rhs.Val?.ToString();
    }
        
    public static implicit operator EchoOut<T>(string rhs)
    {
        return new EchoOut<T>(){Val = rhs};
    }
        
    /*
         * +x, -x, !x, ~x, ++, --, true, false
         *
         * x + y, x - y, x * y, x / y, x % y,
           x & y, x | y, x ^ y,
           x << y, x >> y, x >>> y
         *
         * x == y, x != y, x < y, x > y, x <= y, x >= y
         */

    private static dynamic doUnarySub(dynamic arg1, dynamic arg2) => -arg2;
    
    private static dynamic doUnaryAdd(dynamic arg1, dynamic arg2) => +arg2;

    private static dynamic doInvert(dynamic arg1, dynamic arg2) => ~arg2;

    private static dynamic doInc(dynamic arg1, dynamic arg2) => ++arg2;

    private static dynamic doDec(dynamic arg1, dynamic arg2) => --arg2;
    
    private static dynamic doNot(dynamic arg1, dynamic arg2) => !arg2;


    private static dynamic doBitwiseAnd(dynamic arg1, dynamic arg2) => arg1 & arg2;
    private static dynamic doBitwiseOr(dynamic arg1, dynamic arg2) => arg1 | arg2;
    private static dynamic doBitwiseBitShiftLeft(dynamic arg1, dynamic arg2) => arg1 << arg2;
    private static dynamic doBitwiseBitShiftRight(dynamic arg1, dynamic arg2) => arg1 >> arg2;
    private static dynamic doXor(dynamic arg1, dynamic arg2) => arg1 ^ arg2;
    // private static dynamic doBitwiseBitUnsignedShiftRight(dynamic arg1, dynamic arg2) => arg1 >>> arg2;
    private static dynamic doAdd(dynamic arg1, dynamic arg2) => arg1 + arg2;
    private static dynamic doSub(dynamic arg1, dynamic arg2) => arg1 - arg2;
    private static dynamic doMul(dynamic arg1, dynamic arg2) => arg1 * arg2;
    private static dynamic doDiv(dynamic arg1, dynamic arg2) => arg1 / arg2;
    private static dynamic doMod(dynamic arg1, dynamic arg2) => arg1 % arg2;
    private static dynamic doEq(dynamic arg1, dynamic arg2) => arg1 == arg2;
    private static dynamic doEqual(dynamic arg1, dynamic arg2) => arg1.Equals(arg2);
    private static dynamic doNotEq(dynamic arg1, dynamic arg2) => arg1 != arg2;
    private static dynamic doLessThan(dynamic arg1, dynamic arg2) => arg1 < arg2;
    private static dynamic doGreaterThan(dynamic arg1, dynamic arg2) => arg1 > arg2;
    private static dynamic doEqLessThan(dynamic arg1, dynamic arg2) => arg1 <= arg2;
    private static dynamic doEqGreaterThan(dynamic arg1, dynamic arg2) => arg1 >= arg2;

    private static dynamic MathOp(EchoOut? lhs, EchoOut rhs, string opSign, Func<dynamic, dynamic, dynamic> doOp)
    {
        dynamic c = doOp(lhs?.Val, rhs.Val);
        EchoOut o = EchoOutExt.echo(c);
        o.Counter = lhs?.Counter ?? 0 + rhs.Counter + 1;
        o.Output = rhs?.ConcatMathOp?.Invoke(lhs, rhs, c, o.Counter, opSign) ?? "VALUE";
        return o;
    }
        
    public static dynamic operator +(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "+", doAdd);
        return o;
    }
        
    public static dynamic operator +(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "+", doAdd);
        return o;
    }
        
        
    public static dynamic operator -(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "-", doSub);
        return o;
    }
        
    public static dynamic operator -(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "-", doSub);
        return o;
    }
        
    public static dynamic operator *(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "*", doMul);
        return o;
    }
    public static dynamic operator %(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "%", doMod);
        return o;
    }
    public static dynamic operator %(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "%", doMod);
        return o;
    }
        
    public static dynamic operator *(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "*", doMul);
        return o;
    }
        
    public static dynamic operator /(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "/", doDiv);
        return o;
    }
    public static dynamic operator /(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "/", doDiv);
        return o;
    }
        
    public static dynamic operator <(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "<", doLessThan);
        return o;
    }
    public static dynamic operator <(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "<", doLessThan);
        return o;
    }

    public static dynamic operator >(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, ">", doGreaterThan);
        return o;
    }
    public static dynamic operator >(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, ">", doGreaterThan);
        return o;
    }
        
    public static dynamic operator <=(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, "<=", doEqLessThan);
        return o;
    }
        
    public static dynamic operator <=(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, "<=", doLessThan);
        return o;
    }
        
    public static dynamic operator >=(EchoOut<T> lhs, EchoOut rhs)
    {
        var o = MathOp(lhs, rhs, ">=", doEqGreaterThan);
        return o;
    }
        
    public static dynamic operator >=(EchoOut lhs, EchoOut<T> rhs)
    {
        var o = MathOp(lhs, rhs, ">=", doGreaterThan);
        return o;
    }
        
    public static dynamic operator ==(EchoOut lhs, EchoOut<T> rhs)
    {
        var operationResult = MathOp(lhs, rhs, "==", doEq);
        return operationResult;
    }
        
    public static dynamic operator ==(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "==", doEq);
        return operationResult;
    }

    public static dynamic operator &(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "&", doBitwiseAnd);
        return operationResult;
    }
    
    public static dynamic operator &(EchoOut lhs, EchoOut<T> rhs)
    {
        var operationResult = MathOp(lhs, rhs, "&", doBitwiseAnd);
        return operationResult;
    }

    public static dynamic operator |(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "|", doBitwiseOr);
        return operationResult;
    }
    
    public static dynamic operator |(EchoOut lhs, EchoOut<T>  rhs)
    {
        var operationResult = MathOp(lhs, rhs, "|", doBitwiseOr);
        return operationResult;
    }
    
    public static dynamic operator ^(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "^", doXor);
        return operationResult;
    }
    
    public static dynamic operator ^(EchoOut lhs, EchoOut<T>  rhs)
    {
        var operationResult = MathOp(lhs, rhs, "^", doXor);
        return operationResult;
    }
    
    public static dynamic operator <<(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "<<", doBitwiseBitShiftLeft);
        return operationResult;
    }
    
    public static dynamic operator >>(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, ">>", doBitwiseBitShiftRight);
        return operationResult;
    }

    public static dynamic operator +(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "+", doUnaryAdd);
        return operationResult;
    }
    
    public static dynamic operator !(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "!", doNot);
        return operationResult;
    }

    public static dynamic operator -(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "-", doUnarySub);
        return operationResult;
    }

    public static EchoOut<T> operator ++(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "++", doInc);
        return operationResult;
    }
    
    public static EchoOut<T> operator --(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "--", doDec);
        return operationResult;
    }

    public static dynamic operator ~(EchoOut<T> rhs)
    {
        var operationResult = MathOp(null, rhs, "~", doDec);
        return operationResult;
    }

    public override bool Equals(object? obj)
    {
        dynamic result = false;
        switch (obj)
        {
            case null:
                Output = ConcatMathOp(this, null, false, ++Counter, "Equals");
                break;
            case EchoOut other:
                result = MathOp(this, other, "Equals", doEqual);
                break;
            default:
                result = obj.Equals(Val);
                Output = ConcatMathOp(this, new EchoOut<dynamic>(obj), result, ++Counter, "Equals");
                break;
        }

        return new EchoOut<bool>(result){Output = Output};
    }

    public override int GetHashCode()
    {
        // TODO make a custom logger for this
        return Val?.GetHashCode();
    }

    public static dynamic operator !=(EchoOut lhs, EchoOut<T> rhs)
    {
        var operationResult = MathOp(lhs, rhs, "!=", doNotEq);
        return operationResult;
    }
        
    public static dynamic operator !=(EchoOut<T> lhs, EchoOut rhs)
    {
        var operationResult = MathOp(lhs, rhs, "!=", doNotEq);
        return operationResult;
    }

    public static dynamic operator |(EchoOutTitle str, EchoOut<T> rhs)
    {
        rhs.Output = str.LhsConcatTitle(str, rhs);
        return rhs;
    }
        
    public static dynamic operator |(EchoOut<T> lhs, EchoOutTitle rhs)
    {
        lhs.Output = rhs.RhsConcatTitle(lhs, rhs);
        return rhs;
    }
}

public class EchoOut
{
    public EchoOut()
    {
        OutputLogger = EchoOutFactory.OutputLogger;
        ConcatMathOp = EchoOutFactory.ConcatMathOp;
        ValueToOutput = EchoOutFactory.ValueToOutput;
    }
    
    public string? Output;
    public dynamic? Val;
    public string? Id;
    public int Counter;

    public EchoOutputLogger OutputLogger;
    public ConcatMathOp ConcatMathOp;
    public ValueToOutput ValueToOutput;

    public virtual dynamic trueSelf() => this;
}