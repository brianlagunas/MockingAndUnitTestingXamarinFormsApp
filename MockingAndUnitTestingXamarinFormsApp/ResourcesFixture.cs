using System.Threading.Tasks;
using Tests.Mocks;
using Xamarin.Forms;
using Xunit;
using System.Linq;

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
