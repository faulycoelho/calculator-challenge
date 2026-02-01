namespace CalculatorChallenge.Engine.Configuration
{
    public sealed class ConfigurationRegistry
    {
        public static readonly Dictionary<string, ConfigurationDefinition> Options
            = new Dictionary<string, ConfigurationDefinition>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "--upper-bound", new ConfigurationDefinition
                    {
                        Name = "--upper-bound",
                        Description = "Ignore values greater than this",
                        DefaultValue = "1000",
                        Handler = (value, opt)=>
                        {
                            if (!int.TryParse(value, out var ub) || ub < 0)
                                return false;

                            opt.UpperBound = ub;
                            return true;
                        }
                    }
                },
                {
                    "--alt-delimiter", new ConfigurationDefinition
                    {
                        Name = "--alt-delimiter",
                        Description = "Alternate delimiter",
                        DefaultValue = "\\n",
                        Handler = (value, opt)=>
                        {
                            if (string.IsNullOrWhiteSpace(value))
                                return false;

                            opt.AlternativeDelimiter = value;
                            return true;
                        }
                    }
                },
                {
                    "--deny-negatives", new ConfigurationDefinition
                    {
                        Name = "--deny-negatives",
                        Description = "Deny negative numbers",
                        DefaultValue = "true",
                        Handler = (value, opt) =>
                        {
                            if (!bool.TryParse(value, out var deny))
                                return false;

                            opt.DenyNegatives = deny;
                            return true;
                        }
                    }
                }
            };
    }
}
