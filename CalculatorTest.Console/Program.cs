using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BvgCalculatorEngine.Contracts;
using BvgCalculatorEngine.Contracts.Calculators;
using BvgCalculatorEngine.Implementation;

namespace CalculatorTest.Console
{
    public class BvgTestData
    {
        public BvgCalculationInput Input { get; set; }
        public BvgCalculationResult Result { get; set; }
    }

    class Program
    {
        static void Main()
        {

            var resultList = new List<BvgTestData>();

            var plan = new BvgPlan();

            var engine = new BvgCalculator(new BvgConstants());

            // Testcase 1
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Mann,
                    Altersguthaben = 40000m,
                    DateOfBirth = new DateTime(1974, 8, 15),
                    Lohn = 100000m,
                };

                CalculateHelper(input, plan, engine, resultList);

            }

            // Testcase 2
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Frau,
                    Altersguthaben = 45600m,
                    DateOfBirth = new DateTime(1965, 6, 6),
                    Lohn = 70000m,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 3
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Mann,
                    Altersguthaben = 25000m,
                    DateOfBirth = new DateTime(1963, 1, 24),
                    Lohn = 28200m,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 4
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Frau,
                    Altersguthaben = 44000m,
                    DateOfBirth = new DateTime(1965, 12, 12),
                    Lohn = 28200m,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 5
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Mann,
                    Altersguthaben = 50000m,
                    DateOfBirth = new DateTime(1966, 7, 25),
                    Lohn = 200000m,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 6
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Frau,
                    Altersguthaben = 29000,
                    DateOfBirth = new DateTime(1972, 10, 18),
                    Lohn = 250000,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 7
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Mann,
                    Altersguthaben = 30000,
                    DateOfBirth = new DateTime(1951, 7, 21),
                    Lohn = 70000,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 8
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Frau,
                    Altersguthaben = 21400,
                    DateOfBirth = new DateTime(1952, 10, 5),
                    Lohn = 75000,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 9
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Frau,
                    Altersguthaben = 0,
                    DateOfBirth = new DateTime(1994, 8, 31),
                    Lohn = 71000,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            // Testcase 10
            {
                BvgCalculationInput input = new BvgCalculationInput()
                {
                    Geschlecht = Geschlecht.Mann,
                    Altersguthaben = 0,
                    DateOfBirth = new DateTime(1995, 5, 13),
                    Lohn = 66000,
                };

                CalculateHelper(input, plan, engine, resultList);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<BvgTestData>));
            serializer.Serialize(System.Console.Out,resultList);
        }

        private static void CalculateHelper(BvgCalculationInput input, BvgPlan plan, IBvgCalculator engine, List<BvgTestData> resultList)
        {
            var result =
                Task.Factory.StartNew(s => engine.CalculateAsync(plan, input), engine, CancellationToken.None,
                    TaskCreationOptions.None, TaskScheduler.Default)
                    .Unwrap().GetAwaiter().GetResult();


            resultList.Add(new BvgTestData()
            {
                Input = input,
                Result = result,
            });

        }
    }
}
