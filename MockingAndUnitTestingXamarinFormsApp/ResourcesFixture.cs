using Tests.Mocks;
using Xamarin.Forms;
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

		[Fact]
		public void TestDynamicResource()
		{
			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			var layout = new StackLayout
			{
				Children = {
					label
				}
			};

			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);

			layout.Resources = new ResourceDictionary {
				{ "foo", "FOO" }
			};
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void SetResourceTriggerSetValue()
		{
			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			label.Resources = new ResourceDictionary {
				{"foo", "FOO"}
			};
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void SetResourceOnParentTriggerSetValue()
		{
			var label = new Label();
			var layout = new StackLayout { Children = { label } };
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			layout.Resources = new ResourceDictionary {
				{"foo", "FOO"}
			};
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void SettingResourceTriggersValueChanged()
		{
			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			label.Resources = new ResourceDictionary();
			label.Resources.Add("foo", "FOO");
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void AddingAResourceDictionaryTriggersValueChangedForExistingValues()
		{
			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			var rd = new ResourceDictionary { { "foo", "FOO" } };
			label.Resources = rd;
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void ValueChangedTriggeredOnSubscribeIfKeyAlreadyExists()
		{
			var label = new Label();
			label.Resources = new ResourceDictionary { { "foo", "FOO" } };
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void RemoveDynamicResourceStopsUpdating()
		{
			var label = new Label();
			label.Resources = new ResourceDictionary { { "foo", "FOO" } };
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal("FOO", label.Text);
			label.RemoveDynamicResource(Label.TextProperty);
			label.Resources["foo"] = "BAR";
			Assert.Equal("FOO", label.Text);
		}

		[Fact]
		public void ReparentResubscribe()
		{
			var layout0 = new ContentView { Resources = new ResourceDictionary { { "foo", "FOO" } } };
			var layout1 = new ContentView { Resources = new ResourceDictionary { { "foo", "BAR" } } };

			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			Assert.Equal(Label.TextProperty.DefaultValue, label.Text);

			layout0.Content = label;
			Assert.Equal("FOO", label.Text);

			layout0.Content = null;
			layout1.Content = label;
			Assert.Equal("BAR", label.Text);
		}

		[Fact]
		public void ClearedResourcesDoesNotClearValues()
		{
			var layout0 = new ContentView { Resources = new ResourceDictionary { { "foo", "FOO" } } };
			var label = new Label();
			label.SetDynamicResource(Label.TextProperty, "foo");
			layout0.Content = label;

			Assert.Equal("FOO", label.Text);

			layout0.Resources.Clear();
			Assert.Equal("FOO", label.Text);
		}
	}
}
