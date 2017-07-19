using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringEvaluation
{
    class Program
    {
        static void Main(string[] args)
        {

            /* TODO
             * Create enum of operators
             * allow multiple expression evaluation 1 after the other in case user wants to 
             * Create properties and use them instead
             * make the whole app thread safe
             * handle exceptions throughtout - try catch and central handling mechanism
             * simplify the app
             * create a class library that can be posted to nuget
             */


            // input 1 : '4+5'
            // input 2 : '4+5/2*3-1'

            Console.WriteLine("Please enter the expression you want to evaluate : ");
            var input = Console.ReadLine();
            Console.WriteLine(input);
            // create a recursive function, that ignores all operators other than * and / (precedent op)
            // on finding a precedent op, it will evaluate the operands around it and
            // replace the entire substring in the main string with this new evaluated value
            // it then goes back to find precedents,
            // if no more precedents are found till the end of string, now only + and - operators are left in the string
            // these are just calculated in a single continuous loop
            // final value is presented.


            var retValue = EvaluateStringRecursive(input);
            Console.WriteLine(retValue);
            Console.ReadKey();
        }

        private static int EvaluateStringRecursive(string input)
        {
            //var retValue = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '*' || input[i] == '/')
                {
                    // TODO:  currently not handled for multiple digit numbers - DONE
                    // TODO: Not handled for wrong input instead of integers

                    var leftOp = FindOperand(input, -1, i);
                    var rightOp = FindOperand(input, 1, i);
                    var substr = input.Substring(i - leftOp.ToString().Length, leftOp.ToString().Length + rightOp.ToString().Length + 1);
                    //var substr = input.Substring(input[i - leftOp.ToString().Length], leftOp.ToString().Length + rightOp.ToString().Length + 1);
                    var retValue = CarryOperation(leftOp, rightOp, input[i]);

                    var indexOfSubstr = input.IndexOf(substr);
                    var strBefore = input.Substring(0, indexOfSubstr);
                    var strAfter = input.Substring(indexOfSubstr + substr.Length);
                    input = strBefore + retValue + strAfter;
                    

                }
            }
            //if(input.Contains("*") || input.Contains("/") || input.Contains("+") || input.Contains("-"))
            //{
            //    EvaluateStringRecursive(input);
            //}
            return int.Parse(input);
        }

        public static int CarryOperation(int leftOp, int rightOp, char op)
        {
            var result = 0;
            if (op == '*')
            {
              result = leftOp * rightOp;
            }
            else if(op == '/')
            {
              result = leftOp / rightOp;
            }
            else if (op == '+')
            {
              result = leftOp + rightOp;
            }
            else if (op == '-')
            {
              result = leftOp - rightOp;
            }
            return result;
        }
        //public static string CalculateString(string inputSubstring, int lengthOfLeftOp, int lengthOfRightOp)
        //{
        //    string retValue = "";
        //    // find which operator is present

        //    return retValue;
        //}

        public static int FindOperand(string input, int direction, int currentOperatorPosition)
        {
            var opLength = 0;
            var operand = "";
            if (direction == -1) // leftOP
            {
                for (int i = currentOperatorPosition - 1; i > -1; i--)
                {
                    if (input[i] != '*' && input[i] != '/')
                    {
                        opLength++;
                    }
                    else
                    {
                        break;
                    }
                }

                operand = input.Substring(currentOperatorPosition - opLength, opLength);
            }
            else // rightOP
            {
                for (int i = currentOperatorPosition + 1; i < input.Length; i++)
                {
                    if (input[i] != '*' && input[i] != '/')
                    {
                        opLength++;
                    }
                    else
                    {
                        break;
                    }
                }
                operand = input.Substring(currentOperatorPosition + 1,opLength);
            }

            
            return int.Parse(operand);
        }
    }
}
