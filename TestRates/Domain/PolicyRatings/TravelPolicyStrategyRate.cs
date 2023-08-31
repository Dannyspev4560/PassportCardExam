
using TestRating.Logger;

namespace TestRating.Domain.PolicyRatings
{
    public class TravelPolicyStrategyRate : IPolicyStrategyRate
    {
        private const int MAX_TRAVEL_POLICY_DAYS = 180;
        private const decimal RATING_MULTIPLIER = 2m;
        private const decimal ITALY_RATING_MULTIPLIER = 3m;

        private const string ITALY = "Italy";

        private readonly ILogger logger;
        public TravelPolicyStrategyRate(ILogger logger)
        {
            this.logger = logger;
        }
        public decimal? Rate(Policy policy)
        {
            logger.Info("Rating TRAVEL policy...");
            logger.Info("Validating policy.");
            if (policy.Days <= 0)
            {
                logger.Info("Travel policy must specify Days.");
                return null;
            }
            if (policy.Days > MAX_TRAVEL_POLICY_DAYS)
            {
                logger.Info("Travel policy cannot be more then "+MAX_TRAVEL_POLICY_DAYS + " Days.");
                return null;
            }
            if (string.IsNullOrEmpty(policy.Country))
            {
                logger.Info("Travel policy must specify country.");
                return null;
            }
            decimal rating = policy.Days * RATING_MULTIPLIER;
            if (policy.Country == ITALY)
            {
                rating *= ITALY_RATING_MULTIPLIER;
                return rating;
            }

            return rating;

        }
    }
}
