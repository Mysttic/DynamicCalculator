using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicCalculator;

namespace DynamicCalculatorTests
{
    /// <summary>
    /// Ze względu na nieprawidłowe przekazywanie parametrów w postaci String.Format wymagane było standardowe definiowanie równania
    /// </summary>
    [TestClass]
    public class DynamicCalculatorTests
    {
        private double Calc(string equation)
        {
            double result;
            string sresult = new DynaCode(equation).Execute().ToString();
            double.TryParse(sresult, out result);
            return result;
        }

        private decimal Calc(string equation, decimal v = 0)
        {
            decimal result;
            string sresult = new DynaCode(equation).Execute().ToString();
            decimal.TryParse(sresult, out result);
            return result;
        }

        private string Calc(string equation, string v = "")
        {
            string sresult = new DynaCode(equation).Execute().ToString();
            return sresult.Trim();
        }

        [TestMethod]
        public void Dodawanie()
        {
            Assert.AreEqual(5, Calc("2+3"));
            Assert.AreEqual(8, Calc("2+3+3"));
        }

        [TestMethod]
        public void Odejmowanie()
        {
            Assert.AreEqual(5, Calc("12-7"));
            Assert.AreEqual(-5, Calc("12-7-10"));
        }

        [TestMethod]
        public void Mnozenie()
        {
            Assert.AreEqual(25, Calc("5*5"));
            Assert.AreEqual(0, Calc("9999999*0"));
            Assert.AreEqual(8, Calc("2*2*2"));
        }

        [TestMethod]
        public void Dzielenie()
        {
            Assert.AreEqual(5, Calc("25/5"));
            Assert.AreEqual("Równanie jest nieprawidłowe", Calc("2/0", string.Empty));
            Assert.AreEqual(2, Calc("5/2.5"));
        }
                
        [TestMethod]
        public void Modulo()
        {
            Assert.AreEqual(1, Calc("5%4"));
            Assert.AreEqual(2.8m, Calc("5.9m%3.1m", 0m)); 
        }

        [TestMethod]
        public void OperacjeWNawiasach()
        {
            Assert.AreEqual(3.6, Calc("9.0/(5.0/2.0)"));
            Assert.AreEqual(8, Calc("(2+2)*2"));
        }

        [TestMethod]
        public void OperacjeZParsowaniemInt()
        {

        }

        [TestMethod]
        public void Potegowanie()
        {

        }

        [TestMethod]
        public void SprawdzaniePierwszenstwaOperacjiMatemetycznych1()
        {

        }

        [TestMethod]
        public void SprawdzaniePierwszenstwaOperacjiMatematycznych2()
        {

        }
    }
}
