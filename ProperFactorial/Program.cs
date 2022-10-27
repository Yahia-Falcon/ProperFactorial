using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ProperFactorial {
 internal class Program {
  static void Main(string[] args) {
   Write("Please enter a number: ");

   uint N = uint.Parse(ReadLine());

   while (true) { 
    Write("Do you want the factorial approximately[1, fast] or exactly[2, slow]? ");
    char choice = ReadLine()[0];

    if (choice == '1') {
     WriteLine($"{N}! = {ApproximateFactorial(N)}");
     break;
    } else if (choice == '2') {
     WriteLine($"{N}! = {ExactFactorial(N)}");
     break;
    } else WriteLine("Please enter a valid choice.");
   }

   Write("Use Ctrl+C to Exit.");
   while (true); 
  }

  static double ApproximateFactorial(uint N) {
   double prod = 1;

   for (int i = 2; i <= N; i++) prod *= i;

   return prod;
  }

  static string ExactFactorial(uint N) {
   ulong[] digits = {1};

   if (N <= 1) return "1";

   for (int i = 0; i < N * Math.Log10(N); i++) digits = digits.Append(0U).ToArray();

   for (uint i = 2; i <= N; i++) {
    digits = digits.Select(d => d * i).ToArray();
    ulong[] digitsGreaterThan9 = digits.Where(d => d > 9).ToArray();

    while (digitsGreaterThan9.Length > 0) {
     int index = Array.FindIndex(digits, d => d > 9);

     digits[index + 1] += (uint)Math.Floor((double)(digits[index] / 10));
     digits[index] -= (uint)Math.Floor((double)(digits[index] / 10)) * 10;
     digitsGreaterThan9 = digits.Where(d => d > 9).ToArray();
    }
   }

   int lastNonzeroDigitPosition = Array.FindLastIndex(digits, d => d > 0) + 1;

   string numStr = string.Join("", digits.Take(lastNonzeroDigitPosition));

   for (int i = 1; i <= numStr.Length; i++) {
    if (i % 4 == 0) numStr = numStr.Insert(i - 1, ",");
   }

   return Reverse(numStr.Last() == ',' ? numStr.Remove(numStr.Length - 1) : numStr);
  }

  public static string Reverse(string s) {
   char[] charArray = s.ToCharArray();
   Array.Reverse(charArray);

   return new string(charArray);
  }
 }
}
