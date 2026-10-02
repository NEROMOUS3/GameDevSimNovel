namespace Source.Scripts.Services.Settings.OptionsValue
{
    public class BoolOptionValue : IOptionValue
    {
        public bool Value { get; set; }
        
        public static bool operator true(BoolOptionValue option) => option.Value;

        public static bool operator false(BoolOptionValue option) => !option.Value;
    }
}