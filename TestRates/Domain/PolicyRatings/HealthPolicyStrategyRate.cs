using TestRating.Logger;

namespace TestRating.Domain.PolicyRatings
{
    public class HealthPolicyStrategyRate : IPolicyStrategyRate
    {
        private readonly ILogger logger;
        public HealthPolicyStrategyRate(ILogger logger)
        {
            this.logger = logger;
        }
        public decimal? Rate(Policy policy)
        {
            logger.Info("Rating HEALTH policy...");
            logger.Info("Validating policy.");
            if (string.IsNullOrEmpty(policy.Gender))
            {
                logger.Info("Health policy must specify Gender");
                return null;
            }
            if (policy.Gender == "Male")
            {
                if (policy.Deductible < 500)
                {
                    return 1000m;
                }
                return 900m;
            }
            
            if (policy.Deductible < 800)
                return 1100m;
            
            return 1000m;
        }
    }
}
