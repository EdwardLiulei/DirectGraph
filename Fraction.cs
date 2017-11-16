using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractionCalculator
{
    public class Fraction
    {
        #region Field
        private ulong _integerPart;      //整数部分

        private ulong _denominator;     //分母

        private ulong _numerator;       //分子

        private bool _isPositive;       //是否为正数
        #endregion

        #region Properity
        public ulong Denominator { get { return _denominator; } }
        public ulong Numerator { get { return _numerator; } }

        private bool IsPositive { get { return _isPositive; } }

        private ulong IntegerPart { get { return _integerPart; } }
        #endregion

        #region Constructor
        public Fraction( long numerator, long denominator)
        {
            if (denominator == 0)
                throw new Exception("Denominator can not be Zero!");
            ulong deno, numer;
            int a=1, b=1;
            if (denominator < 0)
            {
                a = -1;
                deno = (ulong)(denominator * -1);
            }
            else
                deno = (ulong)denominator;

            if (numerator < 0)
            {
                b = -1;
                numer = (ulong)(numerator * -1);
            }
            else
            {
                numer =(ulong)numerator;
            }
            ulong divisor = GetGreatestDivisor(deno, numer);
            _denominator = deno / divisor;
            _numerator = numer / divisor;
            _integerPart = GetIntegerPart(ref _denominator, ref _denominator);
            if (a * b < 0)
                _isPositive = false;
            _isPositive = true;
        }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new Exception("Denominator can not be Zero!");
            ulong deno, numer;
            int a = 1, b = 1;
            if (denominator < 0)
            {
                a = -1;
                deno = (ulong)(denominator * -1);
            }
            else
                deno = (ulong)denominator;

            if (numerator < 0)
            {
                b = -1;
                numer = (ulong)(numerator * -1);
            }
            else
            {
                numer = (ulong)numerator;
            }
            ulong divisor = GetGreatestDivisor(deno, numer);
            _denominator = deno / divisor;
            _numerator = numer / divisor;
            _integerPart = GetIntegerPart(ref _denominator, ref _numerator);
            if (a * b < 0)
                _isPositive = false;
            else
                _isPositive = true;
        }

        public Fraction(ulong numerator, ulong denominator, bool isPositive = true)
        {
            if (denominator == 0)
                throw new Exception("Denominator can not be Zero!");
            ulong divisor = GetGreatestDivisor(denominator, numerator);
            _denominator = denominator / divisor;
            _numerator = numerator / divisor;
            _isPositive = isPositive;
            _integerPart = GetIntegerPart(ref _denominator,ref _numerator);
        }

        public Fraction( uint numerator, uint denominator, bool isPositive = true)
        {
            if (denominator == 0)
                throw new Exception("Denominator can not be Zero!");
            ulong deno = (ulong)denominator;
            ulong numer = (ulong)numerator;
            ulong divisor = GetGreatestDivisor(denominator, numerator);
            _denominator = denominator / divisor;
            _numerator = numerator / divisor;
            _isPositive = isPositive;
            _integerPart = GetIntegerPart(ref _denominator, ref _denominator);
        }
        public Fraction()
        {
            _numerator = 0;
            _denominator = 1;
            _isPositive = true;
            _integerPart = 0;
        }

        #endregion

        #region Memberfunction

        public static Fraction operator +(Fraction a, Fraction b)
        {
            if (a.IsPositive && b.IsPositive)
                return AddTwoPositive(a, b);
            if (a.IsPositive && !b.IsPositive)
                return SubTwoPositive(a, b);
            if (!a.IsPositive && b.IsPositive)
                return SubTwoPositive(b, a);
            Fraction c = AddTwoPositive(a, b);
            c._isPositive = false;
            return c;
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            if (a.IsPositive && b.IsPositive)
                return SubTwoPositive(a, b);
            if (a.IsPositive && !b.IsPositive)
                return AddTwoPositive(a, b);
            if (!a.IsPositive && b.IsPositive)
            {
                Fraction c = AddTwoPositive(a, b);
                c._isPositive = false;
            }

            if (!a.IsPositive && !b.IsPositive)
                return SubTwoPositive(b, a);
            return null;
        }

        public static bool operator ==(Fraction a, int b)
        {
            return CompareWithInteger(a, b);
        }

        public static bool operator !=(Fraction a, int b)
        {
            return !CompareWithInteger(a, b);
        }

        public static bool operator ==(int a, Fraction b)
        {
            return CompareWithInteger(b, a);
        }

        public static bool operator !=(int a, Fraction b)
        {
            return !CompareWithInteger(b, a);
        }

        public static bool operator ==(Fraction a,Fraction b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Fraction a, Fraction b)
        {
            return !a.Equals(b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            if (a.IsPositive && b.IsPositive)
                return CompareTwoPositive(a, b);
            if (a.IsPositive && !b.IsPositive)
                return true;
            if (!a.IsPositive && b.IsPositive)
                return false;
            else
                return !CompareTwoPositive(a, b);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            if (a.IsPositive && b.IsPositive)
                return !CompareTwoPositive(a, b);
            if (a.IsPositive && !b.IsPositive)
                return false;
            if (!a.IsPositive && b.IsPositive)
                return true;
            else
                return CompareTwoPositive(a, b);
        }

        public Fraction MutiplyBy(ulong mutiple)
        {           
            ulong newNumera =( _numerator + _denominator * _integerPart)* mutiple;
            Fraction c = new Fraction( newNumera, _denominator, _isPositive);

            return c;
        }

        public Fraction DivideBy(ulong divider)
        {
            ulong newDeno = _denominator * divider;
            ulong newNumera = _numerator + _denominator * _integerPart;
            Fraction c = new Fraction(newNumera, newDeno,_isPositive);

            return c;
        }

        public override bool Equals(object obj)
        {
            Fraction a = (Fraction)obj;
            if (a._numerator.Equals(_numerator) &&
                a._isPositive.Equals(_isPositive) &&
                a._integerPart.Equals(_integerPart) &&
                a._denominator.Equals(_denominator))
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string result = "";
            if (!_isPositive)
                result = "-";
            if (_integerPart != 0)
                result = result + _integerPart;
            if (_integerPart != 0 && _numerator !=0 )
                result = result + ",";
            if(_numerator!=0 )
                result = result + _numerator + "/" + _denominator;
            return result;
        }

        private static bool CompareWithInteger(Fraction a, int b)
        {
            bool isIntegerPositive = b > 0;
            ulong newInt;
            if (b > 0)
                newInt = (ulong)b;
            else
                newInt = (ulong)(b * -1);
            if (a.IsPositive == isIntegerPositive && newInt == a.IntegerPart && a.Numerator ==0)
                return true;
            else
                return false;
        }
        private static Fraction AddTwoPositive(Fraction a,Fraction b)
        {
            ulong integerPater = a.IntegerPart + b.IntegerPart;
            ulong multiple = GetLeastCommonMultiple( a.Denominator, b.Denominator);
            ulong numerator = a.Numerator * (multiple/ a.Denominator) + b.Numerator*(multiple/ b.Denominator);
            Fraction c = new Fraction( numerator, multiple);
            c._integerPart = c.IntegerPart + integerPater;
            return c;
        }

        private static Fraction SubTwoPositive(Fraction a, Fraction b)
        {
            
            bool isPositive;
            ulong multiple = GetLeastCommonMultiple(a.Denominator, b.Denominator);
            ulong numerator,newNumeratorA,newNumeratorB;
            newNumeratorA = a.IntegerPart * multiple + multiple / a.Denominator * a.Numerator;
            newNumeratorB = b.IntegerPart * multiple + multiple / b.Denominator * b.Numerator;

            if (newNumeratorA > newNumeratorB)
            {
                numerator = newNumeratorA - newNumeratorB;
                isPositive = true;
            }
            else
            {
                numerator =  newNumeratorB -newNumeratorA;
                isPositive = false;
            }
            Fraction c = new Fraction(numerator,multiple);
            
            c._isPositive = isPositive;
            return c;
        }

        private static bool CompareTwoPositive(Fraction a, Fraction b)
        {
            ulong multiple = GetLeastCommonMultiple(a.Denominator, b.Denominator);
            ulong numerator, newNumeratorA, newNumeratorB;
            newNumeratorA = a.IntegerPart * multiple + multiple / a.Denominator * a.Numerator;
            newNumeratorB = b.IntegerPart * multiple + multiple / b.Denominator * b.Numerator;

            if (newNumeratorA > newNumeratorB)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static ulong GetGreatestDivisor(ulong a, ulong b)
        {
            ulong result;
            if (a < b)
            {
                result = a;
                a = b;
                b = result;
            }
            while (b != 0)
            {
                result = a % b;
                a = b;
                b = result;
            }
            return a;
        }

        private static ulong GetLeastCommonMultiple(ulong a, ulong b)
        {
            ulong divisor = GetGreatestDivisor(a, b);
            return Math.Max(a, b) / divisor * Math.Min(a,b);
        }

        private ulong GetIntegerPart(ref ulong denominator,ref ulong numerator)
        {
            ulong result = 0;
            if (numerator < denominator)
                return 0;
            result = numerator / denominator;
            numerator = numerator % denominator;
            return result;
        }
        #endregion


    }
}
