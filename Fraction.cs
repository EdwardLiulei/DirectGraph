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

        public ulong ActualNumerator { get { return _numerator + _integerPart * _denominator; } }

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

        public Fraction MutiplyBy(ulong mutiple)
        {
            ulong newNumera = _numerator * mutiple;
            Fraction c = new Fraction( newNumera, _denominator, _isPositive);
            c._integerPart = c._integerPart + _integerPart;

            return c;
        }

        public Fraction DivideBy(ulong divider)
        {
            ulong newDeno = _denominator * divider;
            Fraction c = new Fraction( _numerator, newDeno,_isPositive);
            c._integerPart = c._integerPart + _integerPart;

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
                result = result + _integerPart+",";
            result = result + _numerator + "/" + _denominator;
            return result;
        }


        private static Fraction AddTwoPositive(Fraction a,Fraction b)
        {
            ulong integerPater = a.IntegerPart + b.IntegerPart;
            ulong denominator = a.Denominator * b.Denominator;
            ulong numerator = a.Numerator * b.Denominator + b.Numerator* a.Denominator;
            Fraction c = new Fraction( numerator,denominator);
            c._integerPart = c.IntegerPart + integerPater;
            return c;
        }

        private static Fraction SubTwoPositive(Fraction a, Fraction b)
        {
            
            bool isPositive;
            ulong denominator = a.Denominator * b.Denominator;
            ulong numerator;
            if (a.ActualNumerator * b.Denominator > b.ActualNumerator * a.Denominator)
            {
                numerator = a.ActualNumerator*b.Denominator - b.ActualNumerator*a.Denominator;
                isPositive = true;
            }
            else
            {
                numerator = a.ActualNumerator * b.Denominator - b.ActualNumerator * a.Denominator;
                isPositive = false;
            }
            Fraction c = new Fraction(numerator,denominator);
            
            c._isPositive = isPositive;
            return c;
        }

        private ulong GetGreatestDivisor(ulong a, ulong b)
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

        private ulong GetIntegerPart(ref ulong denominator,ref ulong numerator)
        {
            ulong result = 0;
            if (numerator <= denominator)
                return 0;
            result = numerator / denominator;
            numerator = numerator % denominator;
            return result;
        }
        #endregion


    }
}
