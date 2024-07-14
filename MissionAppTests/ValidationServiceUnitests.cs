using MissionApp.Validations;
namespace MissionAppTests
{
    [TestClass]
    public class ValidationServiceUnitests
    {
        [TestMethod]
        public void TestInvalidDate()
        {
            Assert.IsTrue(MissionCreationValidator.IsValidDate("Dec 17, 1995, 9:45:17 PM"));
            Assert.IsFalse(MissionCreationValidator.IsValidDate("Dec 127, 1995, 9:45:17 PM"));
            Assert.IsFalse(MissionCreationValidator.IsValidDate("Dec 17 1995, 9:45:17 PM"));
            Assert.IsFalse(MissionCreationValidator.IsValidDate(""));
            Assert.IsFalse(MissionCreationValidator.IsValidDate("Dec 17, 1995, 9:45:17:20 PM"));
        }
    }
}