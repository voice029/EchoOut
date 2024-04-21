using System;

namespace VoiceOut.EchoOutClasses
{
    public class EchoOut<T> : EchoOut
    {
        public EchoOut(){}

        public EchoOut(T value)
        {
            Val = value;
        }

        public override dynamic trueSelf() => this;

        public static implicit operator T(EchoOut<T> rhs)
        {
            rhs.AssignmentOpLog(rhs);
            return (T)rhs.Val;
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
            var echoOut = new EchoOut<T>() { Val = rhs };
            echoOut.OutputConversion(rhs);
            return echoOut;
        }

        public static implicit operator string(EchoOut<T> rhs)
        {
            object obj = rhs.Val;
            return obj?.ToString() ?? "";
        }

        public static implicit operator EchoOut<T>(string rhs)
        {
            return new EchoOut<T>() { Val = rhs };
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
        private static dynamic doEqual(dynamic arg1, dynamic arg2)
        {
            bool isEqual = (arg1).Equals(arg2);
            return isEqual;
        }

        private static dynamic doNotEq(dynamic arg1, dynamic arg2) => arg1 != arg2;
        private static dynamic doLessThan(dynamic arg1, dynamic arg2) => arg1 < arg2;
        private static dynamic doGreaterThan(dynamic arg1, dynamic arg2) => arg1 > arg2;
        private static dynamic doEqLessThan(dynamic arg1, dynamic arg2) => arg1 <= arg2;
        private static dynamic doEqGreaterThan(dynamic arg1, dynamic arg2) => arg1 >= arg2;

        private static dynamic MathOp<T1, T2>(T1 lhs, T2 rhs, string opSign, Func<dynamic, dynamic, dynamic> doOp) 
            where T1: EchoOut, new() 
            where T2: EchoOut, new()
        {
            dynamic c = doOp(lhs?.Val, rhs.Val);
            EchoOut o = new T1();
            o.Val = c;
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

        public static dynamic operator |(EchoOut lhs, EchoOut<T> rhs)
        {
            var operationResult = MathOp(lhs, rhs, "|", doBitwiseOr);
            return operationResult;
        }

        public static dynamic operator ^(EchoOut<T> lhs, EchoOut rhs)
        {
            var operationResult = MathOp(lhs, rhs, "^", doXor);
            return operationResult;
        }

        public static dynamic operator ^(EchoOut lhs, EchoOut<T> rhs)
        {
            var operationResult = MathOp(lhs, rhs, "^", doXor);
            return operationResult;
        }
        
#if NET11_0
        public static dynamic operator <<(EchoOut<T> lhs, EchoOut rhs)
        {
            var operationResult = MathOp(lhs, rhs, "<<", doBitwiseBitShiftLeft);
            return operationResult;
        }
        
        public static dynamic operator >> (EchoOut<T> lhs, EchoOut rhs)
        {
            var operationResult = MathOp(lhs, rhs, ">>", doBitwiseBitShiftRight);
            return operationResult;
        }
#else 
        // TODO figure out how to handle these cases
        public static dynamic operator <<(EchoOut<T> lhs, int rhs)
        {
            var operationResult = MathOp(lhs, new EchoOut(rhs), "<<", doBitwiseBitShiftLeft);
            return operationResult;
        }
        
        public static dynamic operator >> (EchoOut<T> lhs, int rhs)
        {
            var operationResult = MathOp(lhs, new EchoOut(rhs), ">>", doBitwiseBitShiftRight);
            return operationResult;
        }        
#endif
        public static dynamic operator +(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "+", doUnaryAdd);
            return operationResult;
        }

        public static dynamic operator !(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "!", doNot);
            return operationResult;
        }

        public static dynamic operator -(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "-", doUnarySub);
            return operationResult;
        }

        public static EchoOut<T> operator ++(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "++", doInc);
            return operationResult;
        }

        public static EchoOut<T> operator --(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "--", doDec);
            return operationResult;
        }

        public static dynamic operator ~(EchoOut<T> rhs)
        {
            var operationResult = MathOp((EchoOut<T>)null, rhs, "~", doDec);
            return operationResult;
        }

        public override bool Equals(object obj)
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

            return new EchoOut<bool>(result) { Output = Output };
        }

        public override int GetHashCode() 
        {
            // TODO make a custom logger for this
            return Val?.GetHashCode() ?? 0;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compare"></param>
        /// <param name="toFormattedStringIfTrue"></param>
        /// <param name="toFormattedStringIfFalse"></param>
        /// <returns></returns>
        public EchoOut<T> IfEq(T compare, object toFormattedStringIfTrue = null,
            object toFormattedStringIfFalse = null)
        {
            if (Val?.Equals(null) == true && compare?.Equals(null) == true ||
                !Val?.Equals(null) == true && Val.Equals(compare))
            {
                lastConditional = true;
                if (toFormattedStringIfTrue?.ToString() != null)
                {
                    Output += String.Format(toFormattedStringIfTrue?.ToString(), Val, compare);
                }
            }
            else
            {
                lastConditional = false;

                if (toFormattedStringIfFalse?.ToString() != null)
                {
                    Output += String.Format(toFormattedStringIfTrue?.ToString(), Val, compare);
                }
            }

            return this;
        }

        public EchoOut<T> True()
        {
            //TODO get this working right
            if (!lastConditional.HasValue)
                throw new Exception();

            conditionalState = true;
            return this;
        }

        public EchoOut<T> False()
        {
            //TODO get this working right

            if (!lastConditional.HasValue)
                throw new Exception();

            conditionalState = false;
            return this;
        }

        public EchoOut<T> If(Func<T, bool> func, object toFormattedStringIfTrue,
            object toFormattedStringIfFalse = null)
        {
            if (func(Val))
            {
                Output += String.Format(toFormattedStringIfTrue?.ToString(), Val);
                lastConditional = true;
            }
            else
            {
                Output += String.Format(toFormattedStringIfFalse?.ToString(), Val);
                lastConditional = false;
            }

            return this;
        }
    }

    public struct EchoOutputMethods
    {
        public EchoOutputLogger OutputLogger { get; set; }
        public ConcatMathOp ConcatMathOp { get; set; }
        public ValueToOutput ValueToOutput { get; set; }
        public LhsConcatTitle LeftHandSide { get; set; }
        public RhsConcatTitle RightHandSide { get; set; }
    }
    
    public class EchoOut
    {
        protected EchoOutputMethods _outputMethods;
        public EchoOut()
        {
            _outputMethods.OutputLogger = EchoOutFactory.OutputLogger;
            _outputMethods.ConcatMathOp = EchoOutFactory.ConcatMathOp;
            _outputMethods.ValueToOutput = EchoOutFactory.ValueToOutput;
            _outputMethods.LeftHandSide = EchoOutFactory.LhsConcatTitle;
            _outputMethods.RightHandSide = EchoOutFactory.RhsConcatTitle;
        }

        public EchoOut(dynamic val)
        {
            Val = val;
        }

        public string Output;
        public dynamic Val;
        public string Id;
        public int Counter;

        // TODO create an interface that adapts to these callbacks
        public virtual EchoOutputLogger OutputLogger
        {
            get => _outputMethods.OutputLogger;
            set => _outputMethods.OutputLogger = value;
        }

        public virtual ConcatMathOp ConcatMathOp
        {
            get => _outputMethods.ConcatMathOp;
            set => _outputMethods.ConcatMathOp = value;
        }

        public virtual ValueToOutput ValueToOutput
        {
            get => _outputMethods.ValueToOutput;
            set => _outputMethods.ValueToOutput = value;
        }

        public virtual LhsConcatTitle LeftHandSide
        {
            get => _outputMethods.LeftHandSide;
            set => _outputMethods.LeftHandSide = value;
        }

        public virtual RhsConcatTitle RightHandSide
        {
            get => _outputMethods.RightHandSide;
            set => _outputMethods.RightHandSide = value;
        }

        public bool? lastConditional;
        public bool? conditionalState;

        public virtual dynamic trueSelf() => this;

    }
}