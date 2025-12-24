namespace CFBROrders.Web.Models
{
    public enum SettingType
    {
        Boolean,
        Select
    }

    public class SettingOption
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public SettingType Type { get; set; }
        public bool BoolValue { get; set; }
        public string? SelectedValue { get; set; }
        public List<string>? Options { get; set; }

        public SettingOption(string name, string label)
        {
            Name = name;
            Label = label;
            Type = SettingType.Boolean;
            BoolValue = false;
        }

        public SettingOption(string name, string label, List<string> options, string defaultValue)
        {
            Name = name;
            Label = label;
            Type = SettingType.Select;
            Options = options;
            SelectedValue = defaultValue;
        }
    }
}
