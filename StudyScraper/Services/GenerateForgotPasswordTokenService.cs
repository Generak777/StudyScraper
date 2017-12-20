using System;
using System.Security.Cryptography;
using System.Text;

namespace StudyScraper.Services
{
    public class GenerateForgotPasswordTokenService : BaseService
    {
        private object _lock = new object();

        private RNGCryptoServiceProvider _crypto = new RNGCryptoServiceProvider();

        // parameterless constructor
        public GenerateForgotPasswordTokenService()
        {

        }

        public string GetRandomCouponValue(int ofLength)
        {
            lock (_lock)
            {
                var builder = new StringBuilder();

                for (int i = 1; i <= ofLength; i++)
                {
                    builder.Append(GetRandomCouponCharacter());
                }

                return builder.ToString();
            }
        }

        public string GetRandomValue(int ofLength)
        {
            lock (_lock)
            {
                var builder = new StringBuilder();

                for (int i = 1; i <= ofLength; i++)
                {
                    builder.Append(GetRandomCharacter());
                }

                return builder.ToString();
            }
        }

        private char GetRandomCouponCharacter()
        {
            var possibleAlphaNumericValues =
                new char[]{'A','B','C','D','E','F','G','H','J','K','L',
                'M','N','P','Q','R','S','T','U','V','W','X','Y',
                'Z','a','b','c','d','e','f','g','h','j','k','l',
                'm','n','p','q','r','s','t','u','v','w','x','y',
                'z','2','3','4','5','6','7','8','9'};

            return possibleAlphaNumericValues[GetRandomInteger(0, possibleAlphaNumericValues.Length - 1)];
        }

        private char GetRandomCharacter()
        {
            var possibleAlphaNumericValues =
                new char[]{'A','B','C','D','E','F','G','H','I','J','K','L',
                'M','N','O','P','Q','R','S','T','U','V','W','X','Y',
                'Z','a','b','c','d','e','f','g','h','i','j','k','l',
                'm','n','o','p','q','r','s','t','u','v','w','x','y',
                'z','0','1','2','3','4','5','6','7','8','9','-','_','!','~'};

            return possibleAlphaNumericValues[GetRandomInteger(0, possibleAlphaNumericValues.Length - 1)];
        }

        private int GetRandomInteger(int lowerBound, int upperBound)
        {
            uint scale = uint.MaxValue;

            // we never want the value to exceed the maximum for a uint, 
            // so loop this until something less than max is found.
            while (scale == uint.MaxValue)
            {
                byte[] fourBytes = new byte[4];
                _crypto.GetBytes(fourBytes); // Get four random bytes.
                scale = BitConverter.ToUInt32(fourBytes, 0); // Convert that into an uint.
            }

            var scaledPercentageOfMax = (scale / (double)uint.MaxValue); // get a value which is the percentage value where scale lies between a uint's min (0) and max value.
            var range = upperBound - lowerBound;
            var scaledRange = range * scaledPercentageOfMax; // scale the range based on the percentage value
            return (int)(lowerBound + scaledRange);
        }
    }
}
