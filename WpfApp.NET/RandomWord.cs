using System;

namespace Pract_3
{
    static class RandomWord
    {
        public static string createRandomWord(int size)
        {
            string resultString = "";
            
            string chars = "QWERTYUIPASDFGHJKLZXCVBNM123456789";

            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                int randomNumber = random.Next(chars.Length);

                resultString += chars[randomNumber];
            }

            return resultString;
        }
    }
}
