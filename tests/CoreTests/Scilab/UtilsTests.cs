using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using LabSharp.Scilab;

namespace LabSharp.Tests.Scilab
{
    public class UtilsTests
    {
        [Fact]
        public void VariableNameToStringBase()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utils.VariableNameToString(null);
            });
            Assert.Equal(string.Empty, Utils.VariableNameToString(new int[0]));

            var sample = new int[] { 0x25E303E0, 0x1D23DD12, 0x162402EF, 0x251B1CF6, 0x27DA2112, 0x28282828 };
            Assert.Equal("W4T!iZ_tH3_mAtr#ix?     ", Utils.VariableNameToString(sample));

            var sample2 = new int[] { 0x25E303E0, 0x1D23DD12, 0x162402EF, 0x251B1CF6, 0x27DA2112 };
            Assert.Equal(Utils.VariableNameToString(sample).TrimEnd(), Utils.VariableNameToString(sample2).TrimEnd());
        }

        [Fact]
        public void StringToVariableNameBase()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Utils.StringToVariableName(null);
            });

            var varName = Utils.StringToVariableName(string.Empty);
            Assert.NotNull(varName);
            Assert.Equal(0, varName.Length);

            var sample = new int[] { 0x25E303E0, 0x1D23DD12, 0x162402EF, 0x251B1CF6, 0x27DA2112, 0x28282828 };
            Assert.Equal(sample, Utils.StringToVariableName("W4T!iZ_tH3_mAtr#ix?     "));

            var sample2 = new int[] { 0x25E303E0, 0x1D23DD12, 0x162402EF, 0x251B1CF6, 0x27DA2112 };
            Assert.Equal(sample2, Utils.StringToVariableName("W4T!iZ_tH3_mAtr#ix? "));
        }
    }
}
