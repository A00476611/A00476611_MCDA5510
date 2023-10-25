using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetAssignment
{
    internal static class Validator
    {
        public static bool NoEmptyFields(Customer cus)
        {
            return
                IsNotNullOrEmpty(cus.firstName) &&
                IsNotNullOrEmpty(cus.lastName) &&
                IsNotNullOrEmpty(cus.email) &&
                IsNotNullOrEmpty(cus.phoneNumber) &&
                IsNotNullOrEmpty(cus.streetNum) &&
                IsNotNullOrEmpty(cus.city) &&
                IsNotNullOrEmpty(cus.street) &&
                IsNotNullOrEmpty(cus.country) &&
                IsNotNullOrEmpty(cus.postalCode) &&
                IsNotNullOrEmpty(cus.province);
        }

        public static bool NoUnwantedSpecialCharacter(Customer cus)
        {
            return
               NoSpecialCharacter(cus.firstName) &&
               NoSpecialCharacter(cus.lastName) &&
               NoSpecialCharacter(cus.city) &&
               NoSpecialCharacter(cus.street) &&
               NoSpecialCharacter(cus.country) &&
               NoSpecialCharacter(cus.postalCode) &&
               NoSpecialCharacter(cus.province);
        }

        private static bool NoSpecialCharacter(string value) 
        {
            return !Regex.IsMatch(value, @"[^a-zA-Z0-9\.\'\- ]+");
        }

        private static bool IsNotNullOrEmpty(string value)
        {
            return value != null && value.Length > 0;
        }


        public static bool IsValidEmail(string value) 
        {
            return Regex.IsMatch(value, @"[a-zA-Z0-9\-]+\@[a-zA-Z]+\.[a-zA-Z]+");
        }

        public static bool IsNumber(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
