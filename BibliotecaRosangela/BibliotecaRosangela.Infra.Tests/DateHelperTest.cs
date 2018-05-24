using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaRosangela.Infra.Tests
{
    [TestFixture]
    public class DateHelperTest
    {
        [Test]
        public void DateHelper_CompareDateSmallerCurrent_ShouldBeOk()
        {
            //Cenário
            DateTime data = DateTime.Now;

            //Executa
            bool result = data.CompareDateSmallerCurrent();

            //Saída
            result.Should().Be(true);
        }

        [Test]
        public void DateHelper_CompareDateSmallerCurrent_OverFlow_ShouldBeFail()
        {
            //Cenário
            DateTime data = DateTime.Now.AddDays(15);

            //Executa
            bool result = data.CompareDateSmallerCurrent();

            //Saída
            result.Should().Be(false);
        }
    }
}
