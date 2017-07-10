using Tests.Mocks;
using Xunit;

namespace Tests
{
	public class ResourcesFixture
    {
		public ResourcesFixture()
		{
			Tests.Xamarin.Forms.Mocks.MockForms.Init();
		}

		[Fact]
		public void ApplicationIsNotNull()
		{
			var app = new ApplicationMock();
			Assert.NotNull(app);
		}
	}
}
