using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobsApplicants.Tests
{
    public class MockAutoFixture
    {
        public static Fixture GetFixture()
        {
            var fixture = new Fixture();

            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

            return fixture;
        }
    }
}
