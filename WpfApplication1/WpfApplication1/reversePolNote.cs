using System;
//using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
namespace ReversePolishNotation{
    public enum Symbols
    {
        none, number, plus, minus, multiplyVar, divide, exponent, Uminus, LP, RP
    }
    public struct ReversPolNoteToken
    {
        public string TokenVal;
        public Symbols TokenValType;
    }
    public class ReversePolishNotation
    {
        private Queue output;
        private Stack operators;
        private string strongOriginalExpression;
        public string originalExpression
        {

            get { return strongOriginalExpression; }

        }
    }
