using System;
//using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
namespace reversePolishNotation
{
    public enum Symbols
    {
        none, number, plus, minus, multiplyVar, divide, exponent, Uminus, LP, RP
    }
    public struct ReversePolNoteToken
    {
        public string TokenVal;
        public Symbols TokenValType;
    }
    public class ReversePolishNotation
    {
        private Queue output;
        private Stack operators;
        private string stOriginalExpression;
        public string originalExpression
        {

            get { return stOriginalExpression; }

        }
        private string stTransExpression;
        public string TransExpression
        {
            get { return stTransExpression; }
        }
        private string stPostfixExpression;
        public string PostfixExpression
        {
            get { return stPostfixExpression; }
        }
        public ReversePolishNotation()
        {
            stTransExpression = string.Empty;
            stOriginalExpression = string.Empty;
            stPostfixExpression = string.Empty;
        }
        public void ParseString(string Expression)
        {
            output = new Queue();
            operators = new Stack();
            stOriginalExpression = Expression;
            string sBuffer = Expression.ToLower();
            sBuffer = Regex.Replace(sBuffer, @"(?<number>\d+(\.\d+)?)", " ${number} ");
            sBuffer = Regex.Replace(sBuffer, @"(?<operators>[+\-*/^()])", " ${operators} ");
            sBuffer = Regex.Replace(sBuffer, @"\s+", " ").Trim();
            sBuffer = Regex.Replace(sBuffer, "-", "MINUS");
            sBuffer = Regex.Replace(sBuffer, @"(?<number>(pi|e|[)]|(\d+(\.\d+)?)))\s+MINUS", "${number} -");
            sBuffer = Regex.Replace(sBuffer, "MINUS", "~");
            stTransExpression = sBuffer;
            string[] sParsed = sBuffer.Split(" ".ToCharArray());
            int i = 0;
            double tokenvalue;
            ReversePolNoteToken token, opstoken;
            for (i = 0; i < sParsed.Length; ++i)
            {
                token = new ReversePolNoteToken();
                token.TokenVal = sParsed[i];
                token.TokenValType = Symbols.none;

                try
                {
                    tokenvalue = double.Parse(sParsed[i]);
                    token.TokenValType = Symbols.number;
                    output.Enqueue(token);
                }
                catch
                {
                    switch (sParsed[i])
                    {
                        case "+":
                            token.TokenValType = Symbols.plus;
                            if (operators.Count > 0)
                            {
                                opstoken = (ReversePolNoteToken)operators.Peek();
                                while (IsOperatorToken(opstoken.TokenValType))
                                {
                                    output.Enqueue(operators.Pop());
                                    if (operators.Count > 0)
                                    {
                                        opstoken = (ReversePolNoteToken)operators.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            operators.Push(token);
                            break;
                        case "-":
                                    token.TokenValType = Symbols.minus;
                                    if (operators.Count > 0)
                                    {
                                        opstoken = (ReversePolNoteToken)operators.Peek();
                                        while (IsOperatorToken(opstoken.TokenValType))
                                        {
                                            output.Enqueue(operators.Pop());
                                            if (operators.Count > 0)
                                            {
                                                opstoken = (ReversePolNoteToken)operators.Peek();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                            operators.Push(token);
                            break;
                        case "*":
                            token.TokenValType = Symbols.multiplyVar;
                            if (operators.Count > 0)
                            {
                                opstoken = (ReversePolNoteToken)operators.Peek();
                                while (IsOperatorToken(opstoken.TokenValType))
                                {
                                    if (opstoken.TokenValType == Symbols.plus || opstoken.TokenValType == Symbols.minus)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        output.Enqueue(operators.Pop());
                                        if (operators.Count > 0)
                                        {
                                            opstoken = (ReversePolNoteToken)operators.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            operators.Push(token);
                            break;
                        case "/":
                            token.TokenValType = Symbols.divide;
                            if (operators.Count > 0)
                            {
                                opstoken = (ReversePolNoteToken)operators.Peek();
                                while (IsOperatorToken(opstoken.TokenValType))
                                {
                                    if (opstoken.TokenValType == Symbols.plus || opstoken.TokenValType == Symbols.minus)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        output.Enqueue(operators.Pop());
                                        if (operators.Count > 0)
                                        {
                                            opstoken = (ReversePolNoteToken)operators.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            operators.Push(token);
                            break;
                        case "^":
                            token.TokenValType = Symbols.exponent;
                            operators.Push(token);
                            break;
                        case "~":
                            token.TokenValType = Symbols.Uminus;
                            operators.Push(token);
                            break;
                        case "(":
                            token.TokenValType = Symbols.LP;
                            operators.Push(token);
                            break;
                        case ")":
                            token.TokenValType = Symbols.RP;
                            if (operators.Count > 0)
                            {
                                opstoken = (ReversePolNoteToken)operators.Peek();
                                while (opstoken.TokenValType != Symbols.LP)
                                {
                                    output.Enqueue(operators.Pop());
                                    if (operators.Count > 0)
                                    {
                                        opstoken = (ReversePolNoteToken)operators.Peek();
                                    }
                                    else
                                    {
                                        throw new Exception("unbalanced parenthesis!");
                                    }
                                }
                                operators.Pop();
                            }
                            break;
                            
                    }
                }
            }
            while (operators.Count != 0)
            {
                opstoken = (ReversePolNoteToken)operators.Pop();
                if (opstoken.TokenValType == Symbols.LP)
                {
                    throw new Exception("unbalanced parenthesis!");
                }
                else
                {
                    output.Enqueue(opstoken);
                }
            }
            stPostfixExpression = string.Empty;
            foreach (object obj in output)
            {
                opstoken = (ReversePolNoteToken)obj;
                stPostfixExpression += string.Format("{0}", opstoken.TokenVal);
            }
        }
        public double Evaluate(){
            Stack result = new Stack();
            double operOne = 0.0, operTwo = 0.0;
            ReversePolNoteToken token = new ReversePolNoteToken();
            foreach (object obj in output)
            {
                token = (ReversePolNoteToken)obj;
                switch (token.TokenValType)
                {
                    case Symbols.number:
                        result.Push(double.Parse(token.TokenVal));
                        break;
                    case Symbols.plus:
                        if (result.Count >= 2)
                        {
                            operTwo = (double)result.Pop();
                            operOne = (double)result.Pop();
                            result.Push(operOne + operTwo);
                        }
                        else
                        {
                            throw new Exception("Syntax error!");
                        }
                        break;
                    case Symbols.minus:
                        if (result.Count >= 2)
                        {
                            operTwo = (double)result.Pop();
                            operOne = (double)result.Pop();
                            result.Push(operOne - operTwo);
                        }
                        else
                        {
                            throw new Exception("Syntax error!");
                        }
                        break;

                    case Symbols.multiplyVar:
                        if (result.Count >= 2)
                        {
                            operTwo = (double)result.Pop();
                            operOne = (double)result.Pop();
                            result.Push(operOne * operTwo);
                        }
                        else
                        {
                            throw new Exception("Syntax error!");
                        }
                        break;
                    case Symbols.divide:
                        if (result.Count >= 2)
                        {
                            operOne = (double)result.Pop();
                            if (operOne == 0.0)
                            {
                                throw new Exception("Cannot Divide by Zero!");
                            }
                            operTwo = (double)result.Pop();
                            result.Push(operTwo / operOne);
                        }
                        else
                        {
                            throw new Exception("Syntax error!");
                        }
                        break;
                    case Symbols.exponent:
                        if (result.Count >= 2)
                        {
                            operTwo = (double)result.Pop();
                            operOne = (double)result.Pop();
                            result.Push(Math.Pow(operOne, operTwo));
                        }
                        else
                        {
                            throw new Exception("Syntax error!");
                        }
                        break;
                    case Symbols.Uminus:
                        if (result.Count >= 1)
                        {
                            operOne = (double)result.Pop();
                            result.Push(-operOne);
                        }
                        else
                        {
                            throw new Exception("syntax error!");
                        }
                        break;
                }
            }
            if (result.Count == 1)
            {
                return (double)result.Pop();
            }
            else
            {
                throw new Exception("Syntax Error!");
            }
        }
        private bool IsOperatorToken(Symbols k){
            bool result = false;
            switch (k)
            {
                case Symbols.plus:
                case Symbols.minus:
                case Symbols.multiplyVar:
                case Symbols.divide:
                case Symbols.exponent:
                case Symbols.Uminus:
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
        }
    }
