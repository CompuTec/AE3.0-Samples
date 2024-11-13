namespace CT.VehOne;

public class Info : AEPlugin
{
    public Info()
    {
        PluginCode = "CT.VehOne";
        PluginName = "CT.VehOne Name";
        //Register the configuration to the plugin 
        //this configuration is managaed in adminstration Panel on the company level    
        SetConfiguration<VehicleWebPluginConfig>();
    }
}