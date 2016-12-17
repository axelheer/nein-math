using Xunit;

namespace NeinMath.Tests
{
    public class BugReports
    {
        [Fact]
        public void Report1()
        {
            var test = new IntegerFunctionsTest();
            test.ModPow(1, 0, 1, 0);
        }

        [Fact]
        public void Report2()
        {
            var test = new IntegerTest();
            test.Divide(Integer.Parse("726838724295606890549323807888004534353641360687318060281374407091401971917968075239446976125146459786673673375810617994082224464461825"),
                        Integer.Parse("18446744073709551615"),
                        Integer.Parse("39402006196394479214415027136064523887590553065872314977974568911611062449297181921535205606241531953485997729120255"));
        }
    }
}
