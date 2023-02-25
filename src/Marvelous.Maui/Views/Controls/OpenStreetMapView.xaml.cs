namespace Marvelous.Maui.Views.Controls;

public partial class OpenStreetMapView : ContentView
{
    public static readonly BindableProperty LatLngParametersProperty = BindableProperty.Create(nameof(LatLngParameters), typeof(IDictionary<string, object>), typeof(OpenStreetMapView), propertyChanged: OnLatLngParametersChanged);

    public virtual IDictionary<string, object> LatLngParameters
    {
        get => (IDictionary<string, object>)GetValue(LatLngParametersProperty);
        set => SetValue(LatLngParametersProperty, value);
    }
    

    public OpenStreetMapView()
	{
		InitializeComponent();
	}


    private static void OnLatLngParametersChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = bindable as OpenStreetMapView;
        view.rootComponent.Parameters = newValue as IDictionary<string, object>;
    }
}